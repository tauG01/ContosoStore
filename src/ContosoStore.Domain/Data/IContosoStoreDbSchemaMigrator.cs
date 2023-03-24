using System.Threading.Tasks;

namespace ContosoStore.Data;

public interface IContosoStoreDbSchemaMigrator
{
    Task MigrateAsync();
}
