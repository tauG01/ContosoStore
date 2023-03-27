using System;
using Volo.Abp.Application.Dtos;

namespace ContosoStore.Customers;

public class MerchantLookupDto : EntityDto<Guid>
{
    public string BusinessName { get; set; }
}
