
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using ContosoStore.Payments;
using ContosoStore.Customers;

namespace ContosoStore;
public class ContosoStoreDataSeederContributor : IDataSeedContributor, ITransientDependency
{
    private readonly IRepository<Payment, Guid> _paymentRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly CustomerManager _customerManager;

    public ContosoStoreDataSeederContributor(IRepository<Payment, Guid> paymentRepository, 
        ICustomerRepository customerRepository, CustomerManager customerManager)
    {
        _paymentRepository = paymentRepository;
        _customerRepository = customerRepository;
        _customerManager = customerManager;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        if (await _paymentRepository.GetCountAsync() > 0)
        {
            return;
        }
        // SEED DATA FOR CUSTOMERS

        var customer1 = await _customerRepository.InsertAsync(
           await _customerManager.CreateAsync(
               "Taurai Gombera",
                "tauraigombera@gmail.com"
           )
       );

        var customer2 = await _customerRepository.InsertAsync(
           await _customerManager.CreateAsync(
               "Tiyamike Gombera",
                "tiyamikegombera@gmail.com"
           )
       );

        // SEED DATA FOR PAYMENTS

        await _paymentRepository.InsertAsync(
           new Payment
           {
               CustomerId = customer1.Id, // SET THE CUSTOMER
               Reference = "20032023800",
               PaymentDate = new DateTime(2023, 3, 8),
               Naration = "Paying for burger"
           },
           autoSave: true
       );

        await _paymentRepository.InsertAsync(
          new Payment
          {
              CustomerId = customer2.Id, // SET THE CUSTOMER
              Reference = "20032023801",
              PaymentDate = new DateTime(2023, 3, 9),
              Naration = "KFC Bucket for 1"
          },
          autoSave: true
      );
    }
}

