using Festival.Localization;
using Volo.Abp.AspNetCore.Components;

namespace Festival.Blazor;

public abstract class FestivalComponentBase : AbpComponentBase
{
    protected FestivalComponentBase()
    {
        LocalizationResource = typeof(FestivalResource);
    }
}
