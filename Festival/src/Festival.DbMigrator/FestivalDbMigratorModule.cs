using Festival.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace Festival.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(FestivalEntityFrameworkCoreModule),
    typeof(FestivalApplicationContractsModule)
)]
public class FestivalDbMigratorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpBackgroundJobOptions>(options =>
        {
            options.IsJobExecutionEnabled = false;
        });
    }
}
