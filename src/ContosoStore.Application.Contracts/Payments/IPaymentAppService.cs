using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace ContosoStore.Payments;
public interface IPaymentAppService :
    ICrudAppService< //Defines CRUD methods
        PaymentDto, //Used to show payments
        Guid, //Primary key of the payment entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdatePaymentDto> //Used to create/update a payment
{

}
