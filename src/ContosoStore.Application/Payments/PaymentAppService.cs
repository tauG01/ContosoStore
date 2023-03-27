
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using ContosoStore.Customers;
using ContosoStore.Payments;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace Acme.BookStore.Books;

//[Authorize(ContosoStorePermissions.Payments.Default)]
public class PaymentAppService :
    CrudAppService<
        Payment, //The Payment entity
        PaymentDto, //Used to show payments
        Guid, //Primary key of the payment entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdatePaymentDto>, //Used to create/update a payments
    IPaymentAppService //implement the IPaymentAppService
{
    private readonly ICustomerRepository _customerRepository;

    public PaymentAppService(IRepository<Payment, Guid> repository, ICustomerRepository customerRepository) : base(repository)
    {
        _customerRepository = customerRepository;
    }

    public override async Task<PaymentDto> GetAsync(Guid id)
    {
        //Get the IQueryable<Payment> from the repository
        var queryable = await Repository.GetQueryableAsync();

        //Prepare a query to join payments and customers
        var query = from payment in queryable
                    join customer in await _customerRepository.GetQueryableAsync() on payment.CustomerId equals customer.Id
                    where payment.Id == id
                    select new { payment, customer };

        //Execute the query and get the payment with author
        var queryResult = await AsyncExecuter.FirstOrDefaultAsync(query);
        if (queryResult == null)
        {
            throw new EntityNotFoundException(typeof(Payment), id);
        }

        var paymentDto = ObjectMapper.Map<Payment, PaymentDto>(queryResult.payment);
        paymentDto.CustomerName = queryResult.customer.Name;
        return paymentDto;

    }

    public override async Task<PagedResultDto<PaymentDto>> GetListAsync(PagedAndSortedResultRequestDto input)
    {
        //Get the IQueryable<Payment> from the repository
        var queryable = await Repository.GetQueryableAsync();

        //Prepare a query to join payments and customers
        var query = from payment in queryable
                    join customer in await _customerRepository.GetQueryableAsync() on payment.CustomerId equals customer.Id
                    select new { payment, customer };

        //Paging
        query = query
            .OrderBy(NormalizeSorting(input.Sorting))
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount);

        //Execute the query and get a list
        var queryResult = await AsyncExecuter.ToListAsync(query);

        //Convert the query result to a list of PaymentDto objects
        var paymentDtos = queryResult.Select(x =>
        {
            var paymentDto = ObjectMapper.Map<Payment, PaymentDto>(x.payment);
            paymentDto.CustomerName = x.customer.Name;
            return paymentDto;
        }).ToList();

        //Get the total count with another query
        var totalCount = await Repository.GetCountAsync();

        return new PagedResultDto<PaymentDto>(
            totalCount,
            paymentDtos
        );
    }

    public async Task<ListResultDto<CustomerLookupDto>> GetCustomerLookupAsync()
    {
        var customers = await _customerRepository.GetListAsync();

        return new ListResultDto<CustomerLookupDto>(
            ObjectMapper.Map<List<Customer>, List<CustomerLookupDto>>(customers)
        );
    }

    private static string NormalizeSorting(string sorting)
    {
        if (sorting.IsNullOrEmpty())
        {
            return $"payment.{nameof(Payment.Reference)}";
        }

        if (sorting.Contains("customerName", StringComparison.OrdinalIgnoreCase))
        {
            return sorting.Replace(
                "customerName",
                "customer.Name",
                StringComparison.OrdinalIgnoreCase
            );
        }

        return $"payment.{sorting}";
    }
}

