using Volo.Abp.Application.Dtos;

namespace ContosoStore.Customers;

public class GetCustomerListDto : PagedAndSortedResultRequestDto
{
    public string Filter { get; set; }
}
