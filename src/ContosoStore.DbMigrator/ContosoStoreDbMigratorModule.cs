using ContosoStore.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace ContosoStore.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(ContosoStoreEntityFrameworkCoreModule),
    typeof(ContosoStoreApplicationContractsModule)
    )]
public class ContosoStoreDbMigratorModule : AbpModule
{

}
