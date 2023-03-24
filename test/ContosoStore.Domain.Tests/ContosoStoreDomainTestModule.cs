using ContosoStore.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace ContosoStore;

[DependsOn(
    typeof(ContosoStoreEntityFrameworkCoreTestModule)
    )]
public class ContosoStoreDomainTestModule : AbpModule
{

}
