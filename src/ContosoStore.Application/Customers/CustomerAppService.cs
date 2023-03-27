using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ContosoStore.Merchants;
using ContosoStore.Payments;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using System.Linq;

namespace ContosoStore.Customers;

public class CustomerAppService : ContosoStoreAppService, ICustomerAppService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly CustomerManager _customerManager;
    private readonly IMerchantRepository _merchantRepository;

    public CustomerAppService(ICustomerRepository customerRepository, CustomerManager customerManager, IMerchantRepository merchantRepository)
    {
        _customerRepository = customerRepository;
        _customerManager = customerManager;
        _merchantRepository = merchantRepository;
    }

    //...SERVICE METHODS WILL COME HERE...
    //public async Task<CustomerDto> GetAsync(Guid id)
    //{
    //    var customer = await _customerRepository.GetAsync(id);
    //    return ObjectMapper.Map<Customer, CustomerDto>(customer);
    //}

    public async Task<CustomerDto> GetAsync(Guid id)
    {
        var customer = await _customerRepository.GetAsync(id);
        var merchant = await _merchantRepository.GetAsync(customer.MerchantId);

        var customerDto = ObjectMapper.Map<Customer, CustomerDto>(customer);
        customerDto.MerchantName = merchant.BusinessName;

        return customerDto;
    }

    public async Task<PagedResultDto<CustomerDto>> GetListAsync(GetCustomerListDto input)
    {
        if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = nameof(Customer.Name);
        }

        var customers = await _customerRepository.GetListAsync(
            input.SkipCount,
            input.MaxResultCount,
            input.Sorting,
            input.Filter
        );

        var totalCount = input.Filter == null
            ? await _customerRepository.CountAsync()
            : await _customerRepository.CountAsync(customer => customer.Name.Contains(input.Filter));

        var customerDtos = new List<CustomerDto>();

        foreach (var customer in customers)
        {
            var merchant = await _merchantRepository.GetAsync(customer.MerchantId);

            var customerDto = ObjectMapper.Map<Customer, CustomerDto>(customer);
            customerDto.MerchantName = merchant.BusinessName;

            customerDtos.Add(customerDto);
        }

        return new PagedResultDto<CustomerDto>(
            totalCount,
            customerDtos
        );
    }
    //public async Task<PagedResultDto<CustomerDto>> GetListAsync(GetCustomerListDto input)
    //{
    //    if (input.Sorting.IsNullOrWhiteSpace())
    //    {
    //        input.Sorting = nameof(Customer.Name);
    //    }

    //    var queryable = await _customerRepository.GetQueryableAsync();

    //    var query = from customer in queryable
    //                join merchant in await _merchantRepository.GetQueryableAsync() on customer.MerchantId equals merchant.Id
    //                select new { customer, merchant };

    //    query = query.OrderBy(x => x.customer.Name, StringComparer.OrdinalIgnoreCase);

    //    var queryResult = await AsyncExecuter.ToListAsync(query);

    //    var customerDtos = queryResult.Select(x =>
    //    {
    //        var customerDto = ObjectMapper.Map<Customer, CustomerDto>(x.customer);
    //        customerDto.MerchantName = x.merchant.BusinessName;
    //        return customerDto;
    //    }).ToList();

    //    var totalCount = await _customerRepository.GetCountAsync();

    //    return new PagedResultDto<CustomerDto>(
    //        totalCount,
    //        customerDtos
    //    );
    //}


    public async Task<CustomerDto> CreateAsync(CreateCustomerDto input)
    {
       
        var customer = await _customerManager.CreateAsync(
            input.MerchantId,
            input.Name,
            input.Email
            
        );

        await _customerRepository.InsertAsync(customer);

        return ObjectMapper.Map<Customer, CustomerDto>(customer);
    }

    public async Task UpdateAsync(Guid id, UpdateCustomerDto input)
    {
        
        var customer = await _customerRepository.GetAsync(id);

        if (customer.Name != input.Name)
        {
            await _customerManager.ChangeNameAsync(customer, input.Name);
        }

        await _customerRepository.UpdateAsync(customer);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _customerRepository.DeleteAsync(id);
    }

    public async Task<ListResultDto<MerchantLookupDto>> GetMerchantLookupAsync()
    {
        var merchants = await _merchantRepository.GetListAsync();

        return new ListResultDto<MerchantLookupDto>(
            ObjectMapper.Map<List<Merchant>, List<MerchantLookupDto>>(merchants)
        );
    }
}

