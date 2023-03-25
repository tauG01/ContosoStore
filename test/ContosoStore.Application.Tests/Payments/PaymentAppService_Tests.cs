
using System;
using System.Linq;
using System.Threading.Tasks;
using ContosoStore.Customers;
using Shouldly;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Validation;
using Xunit;

namespace ContosoStore.Payments;

public class PaymentAppService_Tests : ContosoStoreApplicationTestBase
{
    private readonly IPaymentAppService _paymentAppService;
    private readonly ICustomerAppService _customerAppService;
    public PaymentAppService_Tests()
    {
        _paymentAppService = GetRequiredService<IPaymentAppService>();
        _customerAppService = GetRequiredService<ICustomerAppService>();
    }

    //test to get available valid list of payments
    [Fact]
    public async Task Should_Get_List_Of_Payments()
    {
        //Act
        var result = await _paymentAppService.GetListAsync(
            new PagedAndSortedResultRequestDto()
        );

        //Assert
        result.TotalCount.ShouldBeGreaterThan(0);
        result.Items.ShouldContain(b => b.Reference == "20032023800" &&
                                    b.CustomerName == "Taurai Gombera");

    }

    //test to add valid payment
    [Fact]
    public async Task Should_Create_A_Valid_Payment()
    {
        var customers = await _customerAppService.GetListAsync(new GetCustomerListDto());
        var firstCustomer = customers.Items[0];


        //Act
        var result = await _paymentAppService.CreateAsync(
            new CreateUpdatePaymentDto
            {
                CustomerId = firstCustomer.Id,
                Reference = "N3w-Test-Ref3r3uc3",
                PaymentDate = DateTime.Now,
                Naration = "This is a test order"
            }
        );

        //Assert
        result.Id.ShouldNotBe(Guid.Empty);
        result.Reference.ShouldBe("N3w-Test-Ref3r3uc3");
    }

    //test to try create invalid payment and fail

    [Fact]
    public async Task Should_Not_Create_A_Payment_Without_Reference()
    {
        var exception = await Assert.ThrowsAsync<AbpValidationException>(async () =>
        {
            await _paymentAppService.CreateAsync(
                new CreateUpdatePaymentDto
                {
                    Reference = "",
                    PaymentDate = DateTime.Now,
                    Naration = "Test naration"
                }
            );
        });

        exception.ValidationErrors
            .ShouldContain(err => err.MemberNames.Any(mem => mem == "Reference"));
    }

}
