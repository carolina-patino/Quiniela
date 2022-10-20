using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Festival.EntityFrameworkCore;
using Festival.Equipos;
using Volo.Abp.Identity;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Festival.Apuestas
{
    public class EfCoreApuestaRepository
          : EfCoreRepository<FestivalDbContext, Apuesta, Guid>,
              IApuestaRepository
    {

        public EfCoreApuestaRepository(
            IDbContextProvider<FestivalDbContext> dbContextProvider
        )
            : base(dbContextProvider)
        { }

        public async Task<List<Apuesta>> GetApuestas(Guid? id)
        {
            var dbContext = await GetDbContextAsync();
            var apuestas = dbContext.Apuestas
                .Include(p => p.Predicciones)
                    .ThenInclude(prediccion => prediccion.Partido)
                .Where(p => p.CreatorId == id)
                .Where(x=> x.Tenant == CurrentTenant.Id)
                .ToList();

            return apuestas;
        }

        public async Task<Apuesta> GetApuesta(Guid id)
        {
            var dbContext = await GetDbContextAsync();
            var apuesta = dbContext.Apuestas
                .Include(p => p.Predicciones)
                    .ThenInclude(prediccion => prediccion.Partido)
                .Where(p => p.Id == id)
                .Where(x => x.Tenant == CurrentTenant.Id)
                .FirstOrDefault();
            return apuesta;
        }

        public async Task<IQueryable<ApuestaWithNavigationProperties>> GetRanking()
        {
            return from apuesta in (await GetDbContextAsync()).Apuestas
                   .Include(x => x.Predicciones)
                   .Where(x=> x.Tenant == CurrentTenant.Id)
                   join usuario in (await GetDbContextAsync()).Users
                   on apuesta.CreatorId equals usuario.Id into apuestas
                   from apuestaResultado in apuestas.DefaultIfEmpty()

                   select new ApuestaWithNavigationProperties
                   {
                       Id = apuesta.Id,
                       Tenant = apuesta.Tenant,
                       Alias = apuesta.Alias,
                       EstaPagada = apuesta.EstaPagada,
                       Predicciones = apuesta.Predicciones,
                       NombreUsuario = apuestaResultado.Name,
                       CorreoUsuario = apuestaResultado.Email
                   };
                    
                   

        }

        public async Task<int> GetTotalApuestas()
        {
            var dbContext = await GetDbContextAsync();
            var total = dbContext.Apuestas.Where(x => x.Tenant == CurrentTenant.Id).ToList();
            return total.Count();
        }

        


    }
}
