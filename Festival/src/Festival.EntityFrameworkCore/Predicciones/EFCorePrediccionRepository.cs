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

namespace Festival.Predicciones
{
    public class EfCorePrediccionRepository
              : EfCoreRepository<FestivalDbContext, Prediccion, Guid>,
                  IPrediccionRepository
    {

        public EfCorePrediccionRepository(
            IDbContextProvider<FestivalDbContext> dbContextProvider
        )
            : base(dbContextProvider)
        { }

        public async Task<List<Prediccion>> GetPredicciones(Guid PartidoId)
        {
            var dbContext = await GetDbContextAsync();
            var predicciones = dbContext.Prediccion
                .Where(x => x.Tenant == CurrentTenant.Id)
                .Where(x => x.Partido.Id == PartidoId)
                .Include(x => x.Partido)
                .ToList();
            return predicciones;
        }
    }
}
