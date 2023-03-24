using Volo.Abp.Settings;

namespace ContosoStore.Settings;

public class ContosoStoreSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(ContosoStoreSettings.MySetting1));
    }
}
