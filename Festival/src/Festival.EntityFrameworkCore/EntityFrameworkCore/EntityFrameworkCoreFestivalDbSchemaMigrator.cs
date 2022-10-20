using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Festival.Data;
using Volo.Abp.DependencyInjection;

namespace Festival.EntityFrameworkCore;

public class EntityFrameworkCoreFestivalDbSchemaMigrator
    : IFestivalDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreFestivalDbSchemaMigrator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the FestivalDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<FestivalDbContext>()
            .Database
            .MigrateAsync();
    }
}
