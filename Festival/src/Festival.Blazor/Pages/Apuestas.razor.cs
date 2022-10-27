using System.Collections.Generic;
using System.Threading.Tasks;
using Festival.Apuestas;
using Blazorise.DataGrid;
using Microsoft.AspNetCore.Components;
using Volo.Abp.Application.Dtos;
using Blazorise;
using Festival.Partidos;
using System;
using Festival.Predicciones;
using Volo.Abp.AspNetCore.Components.Notifications;
using Volo.Abp.BlazoriseUI.Components;
using Volo.Abp.Users;
using Microsoft.AspNetCore.Authorization;
using Festival.Permissions;

namespace Festival.Blazor.Pages
{
    public partial class Apuestas
    {
        private readonly IApuestaAppService _apuestaAppService;
        private readonly IPartidoAppService _partidoAppService;
        private readonly IUiNotificationService _uiNotificationService;
        public Apuestas(
            IApuestaAppService apuestaAppService,
            IPartidoAppService partidoAppService,
            IUiNotificationService uiNotificationService
            )
        {
            _apuestaAppService = apuestaAppService;
            _partidoAppService = partidoAppService;
            _uiNotificationService = uiNotificationService;
        }

        //Data grid
        private IReadOnlyList<ApuestaDto> listaApuestas { get; set; }
        private int TotalCount { get; set; }
        private string filtro { get; set; } = null;

        private Validations CreateValidationsRef;

        //Nueva apuesta
        private Modal modalCreateRef;
        private CreateUpdateApuestaDto NuevaApuesta { get; set; } = new CreateUpdateApuestaDto();
        IEnumerable<PartidoDto> partidos = Array.Empty<PartidoDto>();
        private List<CreateUpdatePrediccionDto> predicciones = new List<CreateUpdatePrediccionDto>();
        private DataGridEntityActionsColumn<ApuestaDto> EntityActionsColumn { get; set; }

        private bool PuedeEditarApuesta;

        private bool PuedeCrearApuesta;

        //Consultar detalle

        private Modal ConsultarApuestaRef;
        private ApuestaDto detalleApuesta = new ApuestaDto();

        private Modal EditarApuestaRef;
        private ApuestaDto editarApuesta = new ApuestaDto();

        private async Task GetApuestas()
        {
            var result = await _apuestaAppService.GetApuestas();
            listaApuestas = result.Items;
            TotalCount = (int)result.TotalCount;
        }

        private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<ApuestaDto> e)
        {
            await GetApuestas();
        }

        protected override async Task OnInitializedAsync()
        {
            await SetPermissionsAsync();
        }
        private async Task<Task> ShowCreateModal()
        {
            Console.WriteLine("entro");
            partidos = (await _partidoAppService.GetPartidosAsync()).Items;
            predicciones = new List<CreateUpdatePrediccionDto>();
            foreach (var item in partidos)
            {
                CreateUpdatePrediccionDto predict = new CreateUpdatePrediccionDto();
                predict.PartidoId = item.Id;
                predict.EquipoA = item.EquipoA;
                predict.EquipoB = item.EquipoB;
                predict.SiglasEquipoA = item.SiglasEquipoA; 
                predict.SiglasEquipoB = item.SiglasEquipoB;
                predict.Tenant = item.Tenant;
                predicciones.Add(predict);
            }
            return modalCreateRef.Show();
        }
        private Task HideCreateModal()
        {
            return modalCreateRef.Hide();
        }

        private async Task AgregarAsync()
        {
            if (await CreateValidationsRef.ValidateAll())
            {
                ApuestaDto apuesta = await _apuestaAppService.CreateApuesta(NuevaApuesta);
                foreach (var item in predicciones)
                {
                    item.ApuestaId = apuesta.Id;
                }
                await _apuestaAppService.AgregarPredicciones(predicciones);
                await _uiNotificationService.Success("La apuesta ha sido agregado exitosamente");
                await GetApuestas();
                await modalCreateRef.Hide();
            }
        }

        private async Task OpenConsultarApuestaModal(ApuestaDto input)
        {
            detalleApuesta = await _apuestaAppService.GetApuesta(input.Id);
            await ConsultarApuestaRef.Show();
        }

        private void CloseConsultarApuestaModal()
        {
            ConsultarApuestaRef.Hide();
        }

        public string ObtenerBandera(string siglas)
        {
            return "https://countryflagsapi.com/png/" + siglas;
        }

        public async Task OpenEditarApuestaModal(ApuestaDto input)
        {
            editarApuesta = input;
            await EditarApuestaRef.Show();
        }

        private void CloseEditarApuestaModal()
        {
            EditarApuestaRef.Hide();
        }

        private async Task EditarApuesta()
        {
            await _apuestaAppService.EditarApuesta(editarApuesta);
            CloseEditarApuestaModal();
        }

        private async Task SetPermissionsAsync()
        {
            PuedeEditarApuesta = await AuthorizationService
                .IsGrantedAsync(FestivalPermissions.Apuestas.Edit);
            PuedeCrearApuesta = await AuthorizationService
                .IsGrantedAsync(FestivalPermissions.Apuestas.Create);
        }

        public string GetResultado(PartidoDto partido)
        {
            if (partido.FechaPartido < DateTime.Now)
            {
                return partido.ResultadoEquipoA + "-" + partido.ResultadoEquipoB;
            }
            else
            {
                return "No han jugado";
            }
        }
    }
}