using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Festival.Equipos
{
    public interface IEquipoAppService :
         ICrudAppService< //Defines CRUD methods
             EquipoDto, //Used to show books
             Guid, //Primary key of the book entity
             PagedAndSortedResultRequestDto, //Used for paging/sorting
             CreateUpdateEquipoDto> //Used to create/update a book
    {
        Task<List<EquipoDto>> GetEquiposAsync();
    }
}
