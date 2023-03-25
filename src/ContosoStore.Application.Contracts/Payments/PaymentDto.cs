using System;
using Volo.Abp.Application.Dtos;

namespace ContosoStore.Payments;
public class PaymentDto : AuditedEntityDto<Guid>
{
    public Guid CustomerId { get; set; }
    public string CustomerName { get; set; }
    public string Reference { get; set; }
    public DateTime PaymentDate { get; set; }
    public string Naration { get; set; }
}
