using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Festival.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Festival.Equipos
{
    public class EquipoAppService :
        CrudAppService<
            Equipo, //The Book entity
            EquipoDto, //Used to show books
            Guid, //Primary key of the book entity
            PagedAndSortedResultRequestDto, //Used for paging/sorting
            CreateUpdateEquipoDto>, //Used to create/update a book
        IEquipoAppService //implement the IBookAppService
    {
        public EquipoAppService(IRepository<Equipo, Guid> repository)
            : base(repository)
        {

        }

        public async Task<List<EquipoDto>> GetEquiposAsync()
        {
            var equipos= await Repository.GetListAsync();

            return new List<EquipoDto>(
                ObjectMapper.Map<List<Equipo>, List<EquipoDto>>(equipos)
            );
        }
    }
}