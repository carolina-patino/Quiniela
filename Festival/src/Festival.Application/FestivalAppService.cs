using Festival.Localization;
using Volo.Abp.Application.Services;

namespace Festival;

/* Inherit your application services from this class.
 */
public abstract class FestivalAppService : ApplicationService
{
    protected FestivalAppService()
    {
        LocalizationResource = typeof(FestivalResource);
    }
}
