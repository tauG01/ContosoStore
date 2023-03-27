using System;
using Volo.Abp.Application.Dtos;

namespace ContosoStore.Customers;

public class CustomerDto : EntityDto<Guid>
{
    public Guid MerchantId { get; set; }
    public string MerchantName { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}
