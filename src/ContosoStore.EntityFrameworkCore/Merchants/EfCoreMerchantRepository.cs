using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using ContosoStore.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace ContosoStore.Merchants;

public class EfCoreMerchantRepository : EfCoreRepository<ContosoStoreDbContext, Merchant, Guid>, IMerchantRepository
{
    public EfCoreMerchantRepository(IDbContextProvider<ContosoStoreDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public async Task<Merchant> FindByNameAsync(string businessName)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet.FirstOrDefaultAsync(merchant => merchant.BusinessName == businessName);
    }

    public async Task<Merchant> FindByEmailAsync(string email)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet.FirstOrDefaultAsync(merchant => merchant.Email == email);
    }

    public async Task<List<Merchant>> GetListAsync(
        int skipCount,
        int maxResultCount,
        string sorting,
        string filter = null)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet
            .WhereIf(
                !filter.IsNullOrWhiteSpace(),
                customer => customer.BusinessName.Contains(filter)
                )
            .OrderBy(sorting)
            .Skip(skipCount)
            .Take(maxResultCount)
            .ToListAsync();
    }
}
