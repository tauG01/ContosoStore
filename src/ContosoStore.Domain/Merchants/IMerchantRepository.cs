using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace ContosoStore.Merchants;

public interface IMerchantRepository : IRepository<Merchant, Guid>
{
    Task<Merchant> FindByEmailAsync(string email);

    Task<Merchant> FindByNameAsync(string businessName);

    Task<List<Merchant>> GetListAsync(
        int skipCount,
        int maxResultCount,
        string sorting,
        string filter = null
    );
}
