using Festival.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Festival.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class FestivalController : AbpControllerBase
{
    protected FestivalController()
    {
        LocalizationResource = typeof(FestivalResource);
    }
}
