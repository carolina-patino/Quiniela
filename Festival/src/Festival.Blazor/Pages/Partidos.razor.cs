using System;
using System.Threading.Tasks;
using Festival.Partidos;
using Festival.Equipos;
using Festival.Apuestas;
using Blazorise;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using Volo.Abp.AspNetCore.Components.Notifications;
using Volo.Abp.Application.Dtos;
using System.Linq;
using Blazorise.DataGrid;
using Volo.Abp.BlazoriseUI.Components;
using Festival.Permissions;
using Microsoft.AspNetCore.Authorization;
using Festival.Predicciones;

namespace Festival.Blazor.Pages
{
    public partial class Partidos
    {
        private CreateUpdatePartidoDto NuevoPartido { get; set; } = new CreateUpdatePartidoDto();

        private IReadOnlyList<PartidoDto> partidos { get; set; }

        private Validations EditValidationsRef;
        private Validations CreateValidationsRef;

        private Modal modalCreateRef;
        private Modal modalCargarResultadoRef;

        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;

        private readonly IPartidoAppService _partidoAppService;
        private readonly IEquipoAppService _equipoAppService;
        private readonly IApuestaAppService _apuestaAppService;
        private readonly IPrediccionAppService _prediccionAppService;

        private int totalCount;


        IEnumerable<EquipoDto> listaEquipos = Array.Empty<EquipoDto>();
        IEnumerable<EquipoDto> listaEquiposB = Array.Empty<EquipoDto>();

        private GetEquipoListDto filtro = new GetEquipoListDto();

        private PartidoDto partido= new PartidoDto();

        private readonly IUiNotificationService _uiNotificationService;

        private DataGridEntityActionsColumn<PartidoDto> EntityActionsColumn { get; set; }

        private bool PuedeCrearPartido;
        private bool PuedeCargarResultados;

        public Partidos(
            IPartidoAppService partidoAppService,
            NavigationManager navigationManager,
            IEquipoAppService equipoAppService,
            IUiNotificationService uiNotificationService, 
            IPrediccionAppService prediccionAppService)
        {
            _partidoAppService = partidoAppService;
            _equipoAppService = equipoAppService;
            _uiNotificationService = uiNotificationService;
            _prediccionAppService = prediccionAppService;
        }
        protected override async Task OnInitializedAsync()
        {
            await SetPermissionsAsync();
            await GetEquipos();

        }
        private async Task SetPermissionsAsync()
        {
            PuedeCrearPartido = await AuthorizationService
                .IsGrantedAsync(FestivalPermissions.Partidos.Create);
            PuedeCargarResultados = await AuthorizationService
                .IsGrantedAsync(FestivalPermissions.Partidos.CargarResultados);
        }
        private async Task GetPartidos()
        {
            var result = await _partidoAppService.GetPartidosAsync();
            partidos = result.Items;
            totalCount=(int)result.TotalCount;
        }
        private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<PartidoDto> e)
        {
            await GetPartidos();
        }

        private async Task GetEquipos()
        {
            listaEquipos = await _equipoAppService.GetEquiposAsync();
        }

        private void ActualizarListaB (ChangeEventArgs e)
        {
            Guid equipoSeleccionado = new Guid((string)e.Value);
            listaEquiposB = listaEquipos.Where(x => x.Id != equipoSeleccionado);
            NuevoPartido.EquipoAId = Guid.Parse(e.Value.ToString());
        }


        private async Task AgregarAsync()
        {
            if (await CreateValidationsRef.ValidateAll())
            {
                await _partidoAppService.CreateAsync(NuevoPartido);
                await _uiNotificationService.Success("El partido ha sido agregado exitosamente");
                await GetPartidos();
                await HideCreateModal();
            }
            else
            {
                await _uiNotificationService.Success("El partido ha sido agregado exitosamente");
            }
        }

        private Task ShowCreateModal()
        {
            return modalCreateRef.Show();
        }

        private Task HideCreateModal()
        {
            return modalCreateRef.Hide();
        }

        private Task OpenCargarResultadoModal(PartidoDto input)
        {
            partido = input;
            return modalCargarResultadoRef.Show();
        }

        private Task HideCargarResultadoModal()
        {
            return modalCargarResultadoRef.Hide();
        }

        public string ObtenerBandera(string siglas)
        {
            return "https://countryflagsapi.com/png/" + siglas;
        }

        public string GetResultado(Guid Id)
        {
            PartidoDto partidoDto = partidos.Where(p => p.Id == Id).First();
            if (partidoDto.FechaPartido < DateTime.Now)
            {
                return partidoDto.ResultadoEquipoA + "-" + partidoDto.ResultadoEquipoB;
            }
            else
            {
                return "No han jugado";
            }
        
        }

        private async Task CargarResultado()
        {
            if (await EditValidationsRef.ValidateAll())
            {
                await _partidoAppService.CargarResultados(partido);
                await _uiNotificationService.Success("El resultado ha sido cargado exitosamente");
                await GetPartidos();
                await HideCargarResultadoModal();
                await _prediccionAppService.CalcularPuntos(partido);
            }
            else
            {
                await _uiNotificationService.Error("Sólo puede cargar resultados mayores a 0");
            }
                

        }



    }
}
