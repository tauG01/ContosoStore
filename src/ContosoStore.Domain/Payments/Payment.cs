using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace ContosoStore.Payments;

public class Payment : AuditedAggregateRoot<Guid>
{
    public Guid CustomerId { get; set; }
    public string Reference { get; set; }

    public DateTime PaymentDate { get; set; }

    public string Naration { get; set; }
}
