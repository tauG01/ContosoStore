using System;
using Volo.Abp.Application.Dtos;

namespace ContosoStore.Customers;

public class CustomerDto : EntityDto<Guid>
{
    public string Name { get; set; }
    public string Email { get; set; }
}
