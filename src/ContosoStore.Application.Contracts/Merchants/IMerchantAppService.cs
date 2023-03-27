using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace ContosoStore.Merchants;

public interface IMerchantAppService : IApplicationService
{
    Task<MerchantDto> GetAsync(Guid id);

    Task<PagedResultDto<MerchantDto>> GetListAsync(GetMerchantListDto input);

    Task<MerchantDto> CreateAsync(CreateMerchantDto input);

    Task UpdateAsync(Guid id, UpdateMerchantDto input);

    Task DeleteAsync(Guid id);
}
