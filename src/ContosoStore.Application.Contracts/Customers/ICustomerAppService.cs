using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace ContosoStore.Customers;

public interface ICustomerAppService : IApplicationService
{
    Task<CustomerDto> GetAsync(Guid id);

    Task<PagedResultDto<CustomerDto>> GetListAsync(GetCustomerListDto input);

    Task<CustomerDto> CreateAsync(CreateCustomerDto input);

    Task UpdateAsync(Guid id, UpdateCustomerDto input);

    Task DeleteAsync(Guid id);
}
