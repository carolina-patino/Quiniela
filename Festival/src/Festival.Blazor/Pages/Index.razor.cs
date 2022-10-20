using Blazorise;
using Blazorise.DataGrid;
using Festival.Apuestas;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Festival.Predicciones;
using Volo.Abp.BlazoriseUI.Components;
using Festival.Partidos;
using System;
using Festival.Permissions;
using Microsoft.AspNetCore.Authorization;

namespace Festival.Blazor.Pages;


public partial class Index
{
    private readonly IApuestaAppService _apuestaAppService;
    private readonly IPartidoAppService _partidoAppService;
    private IReadOnlyList<ApuestaDto> apuestas { get; set; }

    private IReadOnlyList<PartidoDto> partidos { get; set; }

    private Modal ConsultarApuestaRef;
    private ApuestaDto detalleApuesta = new ApuestaDto();

    private PremioDto premio = new PremioDto();
    private DataGridEntityActionsColumn<ApuestaDto> EntityActionsColumn { get; set; }

    public Index(IApuestaAppService apuestaAppService,
        IPartidoAppService partidoAppService)
    {
        _apuestaAppService = apuestaAppService;
        _partidoAppService = partidoAppService;
    }

    private bool PuedeConsultarRanking;
    private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<ApuestaDto> e)
    {
        await GetRanking();
    }

    private async Task GetRanking()
    {
        apuestas = await _apuestaAppService.GetRanking();
    }

    private async Task GetPartidosDelDia(DataGridReadDataEventArgs<PartidoDto> e)
    {
        partidos = await _partidoAppService.GetPartidosDelDia();
    }


    private async Task OpenConsultarApuestaModal(ApuestaDto input)
    {
        detalleApuesta = await _apuestaAppService.GetApuesta(input.Id);
        ConsultarApuestaRef.Show();
    }

    private void CloseConsultarApuestaModal()
    {
        ConsultarApuestaRef.Hide();
    }

    public string ObtenerBandera(string siglas)
    {
        return "https://countryflagsapi.com/png/" + siglas;
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

    protected override async Task OnInitializedAsync()
    {
        await SetPermissionsAsync();
        premio = await _apuestaAppService.GetTotalPremio();
    }

    private async Task SetPermissionsAsync()
    {
        PuedeConsultarRanking = await AuthorizationService
            .IsGrantedAsync(FestivalPermissions.Partidos.Create);
    }


}
