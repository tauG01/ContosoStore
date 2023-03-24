using AutoMapper;
using AutoMapper.Internal.Mappers;
using ContosoStore.Customers;
using ContosoStore.Payments;

namespace ContosoStore;

public class ContosoStoreApplicationAutoMapperProfile : Profile
{
    public ContosoStoreApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<Payment, PaymentDto>();
        CreateMap<CreateUpdatePaymentDto, Payment>();
        CreateMap<Customer, CustomerDto>();
    }
}