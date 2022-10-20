using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Festival.Data;

/* This is used if database provider does't define
 * IFestivalDbSchemaMigrator implementation.
 */
public class NullFestivalDbSchemaMigrator : IFestivalDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
