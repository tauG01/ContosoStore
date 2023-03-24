using ContosoStore.Payments;
using AutoMapper;

namespace ContosoStore.Blazor;

public class ContosoStoreBlazorAutoMapperProfile : Profile
{
    public ContosoStoreBlazorAutoMapperProfile()
    {
        CreateMap<PaymentDto, CreateUpdatePaymentDto>();
    }
}
