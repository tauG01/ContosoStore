using ContosoStore.Payments;
using ContosoStore.Customers;
using ContosoStore.Merchants;
using AutoMapper;

namespace ContosoStore.Blazor;

public class ContosoStoreBlazorAutoMapperProfile : Profile
{
    public ContosoStoreBlazorAutoMapperProfile()
    {
        CreateMap<PaymentDto, CreateUpdatePaymentDto>();
        CreateMap<CustomerDto, UpdateCustomerDto>();
        CreateMap<MerchantDto, UpdateMerchantDto>();
    }
}
