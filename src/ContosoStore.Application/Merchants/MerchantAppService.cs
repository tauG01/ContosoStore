using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoStore.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace ContosoStore.Merchants;

//[Authorize(ContosoStorePermissions.Customers.Default)]
public class MerchantAppService : ContosoStoreAppService, IMerchantAppService
{
    private readonly IMerchantRepository _merchantRepository;
    private readonly MerchantManager _merchantManager;

    public MerchantAppService(
        IMerchantRepository merchantRepository,
        MerchantManager merchantManager)
    {
        _merchantRepository = merchantRepository;
        _merchantManager = merchantManager;
    }

    //...SERVICE METHODS WILL COME HERE...
    public async Task<MerchantDto> GetAsync(Guid id)
    {
        var merchant = await _merchantRepository.GetAsync(id);
        return ObjectMapper.Map<Merchant, MerchantDto>(merchant);
    }

    public async Task<PagedResultDto<MerchantDto>> GetListAsync(GetMerchantListDto input)
    {
        if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = nameof(Merchant.BusinessName);
        }

        var merchants = await _merchantRepository.GetListAsync(
            input.SkipCount,
            input.MaxResultCount,
            input.Sorting,
            input.Filter
        );

        var totalCount = input.Filter == null
            ? await _merchantRepository.CountAsync()
            : await _merchantRepository.CountAsync(merchant => merchant.BusinessName.Contains(input.Filter));

        return new PagedResultDto<MerchantDto>(
            totalCount,
            ObjectMapper.Map<List<Merchant>, List<MerchantDto>>(merchants)
        );
    }

    public async Task<MerchantDto> CreateAsync(CreateMerchantDto input)
    {

        var merchant = await _merchantManager.CreateAsync(
            input.BusinessName,
            input.PhoneNumber,
            input.Email

        );

        await _merchantRepository.InsertAsync(merchant);

        return ObjectMapper.Map<Merchant, MerchantDto>(merchant);
    }

    public async Task UpdateAsync(Guid id, UpdateMerchantDto input)
    {

        var merchant = await _merchantRepository.GetAsync(id);

        if (merchant.BusinessName != input.BusinessName)
        {
            await _merchantManager.ChangeBusinessNameAsync(merchant, merchant.BusinessName);
        }

        merchant.PhoneNumber = input.PhoneNumber;
        merchant.Email = input.Email;

        await _merchantRepository.UpdateAsync(merchant);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _merchantRepository.DeleteAsync(id);
    }
}

