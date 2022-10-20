using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Festival.Equipos
{
    public interface IEquipoRepository : IRepository<Equipo, Guid>
    {
        Task<Equipo> FindByNameAsync(string name);

        Task<List<Equipo>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null
        );
    }
}