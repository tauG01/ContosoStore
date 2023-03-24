using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace ContosoStore.Blazor;

[Dependency(ReplaceServices = true)]
public class ContosoStoreBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "ContosoStore";
}
