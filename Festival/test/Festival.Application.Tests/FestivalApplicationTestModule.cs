using Volo.Abp.Modularity;

namespace Festival;

[DependsOn(
    typeof(FestivalApplicationModule),
    typeof(FestivalDomainTestModule)
    )]
public class FestivalApplicationTestModule : AbpModule
{

}
