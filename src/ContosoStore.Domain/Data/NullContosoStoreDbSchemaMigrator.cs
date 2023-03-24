using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace ContosoStore.Data;

/* This is used if database provider does't define
 * IContosoStoreDbSchemaMigrator implementation.
 */
public class NullContosoStoreDbSchemaMigrator : IContosoStoreDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
