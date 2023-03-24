using Volo.Abp.Modularity;

namespace ContosoStore;

[DependsOn(
    typeof(ContosoStoreApplicationModule),
    typeof(ContosoStoreDomainTestModule)
    )]
public class ContosoStoreApplicationTestModule : AbpModule
{

}
