using ContosoStore.Localization;
using Volo.Abp.AspNetCore.Components;

namespace ContosoStore.Blazor;

public abstract class ContosoStoreComponentBase : AbpComponentBase
{
    protected ContosoStoreComponentBase()
    {
        LocalizationResource = typeof(ContosoStoreResource);
    }
}
