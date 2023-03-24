using ContosoStore.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace ContosoStore.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class ContosoStoreController : AbpControllerBase
{
    protected ContosoStoreController()
    {
        LocalizationResource = typeof(ContosoStoreResource);
    }
}
