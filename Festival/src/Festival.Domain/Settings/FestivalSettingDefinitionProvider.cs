using Volo.Abp.Settings;

namespace Festival.Settings;

public class FestivalSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(FestivalSettings.MySetting1));
    }
}
