using System;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace ContosoStore.Payments;

public class PaymentAppService :
    CrudAppService<
        Payment, //The Payment entity
        PaymentDto, //Used to show payments
        Guid, //Primary key of the payment entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdatePaymentDto>, //Used to create/update a payment
    IPaymentAppService //implement the IPaymentAppService
{
    public PaymentAppService(IRepository<Payment, Guid> repository)
        : base(repository)
    {

    }
}
