using System;
using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace ContosoStore.Customers;

public class CustomerAppService_Tests : ContosoStoreApplicationTestBase
{
    private readonly ICustomerAppService _customerAppService;

    public CustomerAppService_Tests()
    {
        _customerAppService = GetRequiredService<ICustomerAppService>();
    }

    [Fact]
    public async Task Should_Get_All_Customers_Without_Any_Filter()
    {
        var result = await _customerAppService.GetListAsync(new GetCustomerListDto());

        result.TotalCount.ShouldBeGreaterThanOrEqualTo(2);
        result.Items.ShouldContain(customer => customer.Name == "Taurai Gombera");
        result.Items.ShouldContain(customer => customer.Name == "Tiyamike Gombera");
    }

    [Fact]
    public async Task Should_Get_Filtered_Customers()
    {
        var result = await _customerAppService.GetListAsync(
            new GetCustomerListDto { Filter = "Taurai" });

        result.TotalCount.ShouldBeGreaterThanOrEqualTo(1);
        result.Items.ShouldContain(customer => customer.Name == "Taurai Gombera");
        result.Items.ShouldNotContain(customer => customer.Name == "Tiyamike Chagoma");
    }

    [Fact]
    public async Task Should_Create_A_New_Customer()
    {
        var customer = await _customerAppService.CreateAsync(
            new CreateCustomerDto
            {
                Name = "Lovemore Gombera",
                Email = "lovemoregomera@gmail.com", 
            }
        );

        customer.Id.ShouldNotBe(Guid.Empty);
        customer.Name.ShouldBe("Lovemore Gombera");
    }

    [Fact]
    public async Task Should_Not_Allow_To_Create_Duplicate_Customer()
    {
        await Assert.ThrowsAsync<CustomerAlreadyExistsException>(async () =>
        {
            await _customerAppService.CreateAsync(
                new CreateCustomerDto
                {
                    Name = "Taurai Gombera",
                    Email = "tauraigombera@gmail.com",
                }
            );
        });
    }
}
