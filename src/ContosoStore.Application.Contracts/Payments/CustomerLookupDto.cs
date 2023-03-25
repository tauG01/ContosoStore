using System;
using Volo.Abp.Application.Dtos;

namespace ContosoStore.Payments;

public class CustomerLookupDto : EntityDto<Guid>
{
    public string Name { get; set; }
}