using Festival.Predicciones;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Festival.Apuestas
{
    public interface IApuestaAppService : IApplicationService
    {
        Task<PagedResultDto<ApuestaDto>> GetApuestas();

        Task<List<CreateUpdatePrediccionDto>> AgregarPredicciones(List<CreateUpdatePrediccionDto> predicciones);

        Task<ApuestaDto> CreateApuesta(CreateUpdateApuestaDto input);

        Task<ApuestaDto> GetApuesta(Guid id);

        Task<PagedResultDto<ApuestaDto>> GetRanking();

        Task EditarApuesta(ApuestaDto input);

        Task<PremioDto> GetTotalPremio();

        Task DeleteApuesta(Guid ApuestaId);
    }
}

