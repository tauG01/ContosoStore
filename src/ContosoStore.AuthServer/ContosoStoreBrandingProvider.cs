using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace ContosoStore;

[Dependency(ReplaceServices = true)]
public class ContosoStoreBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "ContosoStore";
}
