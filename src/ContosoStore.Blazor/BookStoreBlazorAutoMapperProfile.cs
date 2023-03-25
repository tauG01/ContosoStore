using ContosoStore.Payments;
using ContosoStore.Customers;
using AutoMapper;

namespace ContosoStore.Blazor;

public class ContosoStoreBlazorAutoMapperProfile : Profile
{
    public ContosoStoreBlazorAutoMapperProfile()
    {
        CreateMap<PaymentDto, CreateUpdatePaymentDto>();
        CreateMap<CustomerDto, UpdateCustomerDto>();

    }
}
