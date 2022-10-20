using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace Festival.Apuestas
{
    public interface IApuestaRepository : IRepository<Apuesta, Guid>
    {
        Task<List<Apuesta>> GetApuestas(Guid? userId);
        Task<Apuesta> GetApuesta(Guid id);

        Task<IQueryable<ApuestaWithNavigationProperties>> GetRanking();

        Task<int> GetTotalApuestas();

    }
}

