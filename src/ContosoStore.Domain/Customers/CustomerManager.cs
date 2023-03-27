using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace ContosoStore.Customers;

public class CustomerManager : DomainService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerManager(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<Customer> CreateAsync(
        Guid merchantId,
        [NotNull] string name,
        [NotNull] string email)
    {
        Check.NotNullOrWhiteSpace(email, nameof(email));

        var existingCustomer = await _customerRepository.FindByEmailAsync(email);
        if (existingCustomer != null)
        {
            throw new CustomerAlreadyExistsException(email);
        }

        return new Customer(
            GuidGenerator.Create(),
            merchantId,
            name,
            email
        );
    }

    public async Task ChangeNameAsync(
        [NotNull] Customer customer,
        [NotNull] string newName)
    {
        Check.NotNull(customer, nameof(customer));
        Check.NotNullOrWhiteSpace(newName, nameof(newName));

        var existingCustomer = await _customerRepository.FindByNameAsync(newName);
        if (existingCustomer != null && existingCustomer.Id != customer.Id)
        {
            throw new CustomerAlreadyExistsException(newName);
        }
        customer.ChangeName(newName);
    }
}
