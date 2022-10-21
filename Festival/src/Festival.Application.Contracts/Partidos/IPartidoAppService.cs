using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Festival.Partidos
{
    public interface IPartidoAppService : ICrudAppService< //Defines CRUD methods
             PartidoDto, //Used to show books
             Guid, //Primary key of the book entity
             PagedAndSortedResultRequestDto, //Used for paging/sorting
             CreateUpdatePartidoDto> //Used to create/update a book
    {

        Task<PagedResultDto<PartidoDto>> GetPartidosAsync();
        Task CargarResultados(PartidoDto input);

        Task<List<PartidoDto>> GetPartidosDelDia();
    }
}

