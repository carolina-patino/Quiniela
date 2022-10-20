using Volo.Abp.Application.Dtos;

namespace Festival.Equipos
{
    public class GetEquipoListDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}