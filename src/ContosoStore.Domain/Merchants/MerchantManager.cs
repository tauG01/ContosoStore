using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace ContosoStore.Merchants;

public class MerchantManager : DomainService
{
    private readonly IMerchantRepository _merchantRepository;

    public MerchantManager(IMerchantRepository merchantRepository)
    {
        _merchantRepository = merchantRepository;
    }

    public async Task<Merchant> CreateAsync(
        [NotNull] string businessName,
        [NotNull] string phoneNumber,
        [NotNull] string email)
    {
        Check.NotNullOrWhiteSpace(email, nameof(email));

        var existingCustomer = await _merchantRepository.FindByEmailAsync(email);
        if (existingCustomer != null)
        {
            throw new MerchantAlreadyExistsException(email);
        }

        return new Merchant(
            GuidGenerator.Create(),
            businessName,
            phoneNumber,
            email
        );
    }

    public async Task ChangeBusinessNameAsync(
        [NotNull] Merchant merchant,
        [NotNull] string newName)
    {
        Check.NotNull(merchant, nameof(merchant));
        Check.NotNullOrWhiteSpace(newName, nameof(newName));

        var existingMerchant = await _merchantRepository.FindByNameAsync(newName);
        if (existingMerchant != null && existingMerchant.Id != merchant.Id)
        {
            throw new MerchantAlreadyExistsException(newName);
        }
        merchant.ChangeName(newName);
    }
}
