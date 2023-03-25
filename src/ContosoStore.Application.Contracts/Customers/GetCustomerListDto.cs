using System;
using Volo.Abp.Application.Dtos;

namespace ContosoStore.Customers;

[Serializable]
public class GetCustomerListDto : PagedAndSortedResultRequestDto
{
    public string Filter { get; set; }
}
