using System;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace ContosoStore.Customers;

public class Customer : FullAuditedAggregateRoot<Guid>
{
    public Guid MerchantId { get; set; }
    public string Name { get; private set; }
    public string Email { get; private set; }

    private Customer()
    {
        /* This constructor is for deserialization / ORM purpose */
    }

    internal Customer(
        Guid id,
        [NotNull] Guid merchantId,
        [NotNull] string name,
        [NotNull] string email)
        : base(id)
    {
        Email = email;
        MerchantId = merchantId;
        SetName(name);
    }

    internal Customer ChangeName([NotNull] string name)
    {
        SetName(name);
        return this;
    }

    private void SetName([NotNull] string name)
    {
        Name = Check.NotNullOrWhiteSpace(
            name,
            nameof(name),
            maxLength: CustomerConsts.MaxNameLength
        );
    }
}