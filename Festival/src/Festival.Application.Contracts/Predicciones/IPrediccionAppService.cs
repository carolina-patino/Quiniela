using Festival.Partidos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Festival.Predicciones
{
    public interface IPrediccionAppService :
           ICrudAppService< //Defines CRUD methods
           PrediccionDto, //Used to show books
           Guid, //Primary key of the book entity
           PagedAndSortedResultRequestDto, //Used for paging/sorting
           CreateUpdatePrediccionDto> //Used to create/update a book
    {
        Task CalcularPuntos(PartidoDto Partido);
    }
}
