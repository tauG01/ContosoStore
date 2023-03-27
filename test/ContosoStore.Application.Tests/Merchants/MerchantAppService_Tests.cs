using System;
using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace ContosoStore.Merchants;

public class MerchantAppService_Tests : ContosoStoreApplicationTestBase
{
    private readonly IMerchantAppService _merchantAppService;

    public MerchantAppService_Tests()
    {
        _merchantAppService = GetRequiredService<IMerchantAppService>();
    }

    [Fact]
    public async Task Should_Get_All_Merchants_Without_Any_Filter()
    {
        var result = await _merchantAppService.GetListAsync(new GetMerchantListDto());

        result.TotalCount.ShouldBeGreaterThanOrEqualTo(2);
        result.Items.ShouldContain(merchant => merchant.BusinessName == "KFC");
        result.Items.ShouldContain(merchant => merchant.BusinessName == "Kips");
    }

    [Fact]
    public async Task Should_Get_Filtered_Merchants()
    {
        var result = await _merchantAppService.GetListAsync(
            new GetMerchantListDto { Filter = "KF" });

        result.TotalCount.ShouldBeGreaterThanOrEqualTo(1);
        result.Items.ShouldContain(merchant => merchant.BusinessName == "KFC");
        result.Items.ShouldNotContain(merchant => merchant.BusinessName == "Kips");
    }

    [Fact]
    public async Task Should_Create_A_New_Merchant()
    {
        var merchant = await _merchantAppService.CreateAsync(
            new CreateMerchantDto
            {
                BusinessName = "Game Stores",
                PhoneNumber= "+265888212121",
                Email = "service@gamestores.com",
            }
        );

        merchant.Id.ShouldNotBe(Guid.Empty);
        merchant.BusinessName.ShouldBe("Game Stores");
    }

    [Fact]
    public async Task Should_Not_Allow_To_Create_Duplicate_Merchant()
    {
        await Assert.ThrowsAsync<MerchantAlreadyExistsException>(async () =>
        {
            await _merchantAppService.CreateAsync(
                new CreateMerchantDto
                {
                    BusinessName = "KFC",
                    PhoneNumber= "+265888701110",
                    Email = "service@kfc.com",
                }
            );
        });
    }
}
