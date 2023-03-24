using System;
using System.Linq;
using System.Threading.Tasks;
using Shouldly;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Validation;
using Xunit;

namespace ContosoStore.Payments;

public class PaymentAppService_Tests : ContosoStoreApplicationTestBase
{
    private readonly IPaymentAppService _contosoAppService;

    public PaymentAppService_Tests()
    {
        _contosoAppService = GetRequiredService<IPaymentAppService>();
    }

    //test to get available valid lit of payments
    [Fact]
    public async Task Should_Get_List_Of_Payments()
    {
        //Act
        var result = await _contosoAppService.GetListAsync(
            new PagedAndSortedResultRequestDto()
        );

        //Assert
        result.TotalCount.ShouldBeGreaterThan(0);
        result.Items.ShouldContain(b => b.Reference == "20032023801");
    }

    //test to add valid payment
    [Fact]
    public async Task Should_Create_A_Valid_Payment()
    {
        //Act
        var result = await _contosoAppService.CreateAsync(
            new CreateUpdatePaymentDto
            {
                Reference = "TestRef3r3uc3",
                PaymentDate = DateTime.Now,
                Naration = "Test naration"
            }
        );

        //Assert
        result.Id.ShouldNotBe(Guid.Empty);
        result.Reference.ShouldBe("TestRef3r3uc3");
    }

    //test to try create invalid payment and fail

    [Fact]
    public async Task Should_Not_Create_A_Payment_Without_Reference()
    {
        var exception = await Assert.ThrowsAsync<AbpValidationException>(async () =>
        {
            await _contosoAppService.CreateAsync(
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
