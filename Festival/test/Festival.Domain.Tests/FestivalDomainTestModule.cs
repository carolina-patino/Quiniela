using Festival.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Festival;

[DependsOn(
    typeof(FestivalEntityFrameworkCoreTestModule)
    )]
public class FestivalDomainTestModule : AbpModule
{

}
