using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Festival.EntityFrameworkCore;
using Festival.Equipos;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Festival.Partidos
{
    public class EfCorePartidoRepository
          : EfCoreRepository<FestivalDbContext, Partido, Guid>,
              IPartidoRepository
    {

        public EfCorePartidoRepository(
            IDbContextProvider<FestivalDbContext> dbContextProvider
        )
            : base(dbContextProvider)
        { }

        public async Task<List<Partido>> GetPartidos()
        {
            var dbContext = await GetDbContextAsync();
            var partidos = dbContext.Partidos
                .Where(x=> x.Tenant == CurrentTenant.Id)
                .ToList();
            return partidos;
        }

        public async Task<List<Partido>> GetPartidosDelDia()
        {
            var dbContext = await GetDbContextAsync();
            var partidos = dbContext.Partidos
                .Where(x => x.Tenant == CurrentTenant.Id)
                .Where(x => x.FechaPartido.Date == DateTime.Today)
                .ToList();
            return partidos;

        }


    }
}
