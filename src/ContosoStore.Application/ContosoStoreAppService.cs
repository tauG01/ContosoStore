using System;
using System.Collections.Generic;
using System.Text;
using ContosoStore.Localization;
using Volo.Abp.Application.Services;

namespace ContosoStore;

/* Inherit your application services from this class.
 */
public abstract class ContosoStoreAppService : ApplicationService
{
    protected ContosoStoreAppService()
    {
        LocalizationResource = typeof(ContosoStoreResource);
    }
}
