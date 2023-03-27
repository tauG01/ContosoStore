using System;
using Volo.Abp.Application.Dtos;

namespace ContosoStore.Merchants;

public class MerchantDto : EntityDto<Guid>
{
    public string BusinessName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
}
