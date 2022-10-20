using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Festival.Blazor;

[Dependency(ReplaceServices = true)]
public class FestivalBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Festival";
}
