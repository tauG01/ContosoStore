using System;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace ContosoStore.Merchants;

public class Merchant : FullAuditedAggregateRoot<Guid>
{
    public string BusinessName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }

    private Merchant()
    {
        /* This constructor is for deserialization / ORM purpose */
    }

    internal Merchant(
      Guid id,
      [NotNull] string businessName,
      [NotNull] string phoneNumber,
      [NotNull] string email)
      : base(id)
    {
        Email = email;
        PhoneNumber = phoneNumber;
        SetName(businessName);
    }

    internal Merchant ChangeName([NotNull] string businessName)
    {
        SetName(businessName);
        return this;
    }

    private void SetName([NotNull] string businessName)
    {
        BusinessName = Check.NotNullOrWhiteSpace(
            businessName,
            nameof(businessName),
            maxLength: MerchantConsts.MaxBusinessNameLength
        );
    }
}
