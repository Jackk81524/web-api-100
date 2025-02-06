

using Alba;
using SoftwareCatalog.Api.Vendors;

namespace SoftwareCatalog.Tests.Vendors;
public class AddingAVendor
{
    [Fact]
    public async Task CanAddAVendor()
    {
        var host = await AlbaHost.For<Program>();

        var requestModel = new VendorCreateModel
        {
            Name = "Jetbrains",
            Link = "https://jetbrains.com"
        };

        var postResponse = await host.Scenario(api =>
        {
            api.Post.Json(requestModel).ToUrl("/vendors");
            api.StatusCodeShouldBe(201);
        });

        var postBody = postResponse.ReadAsJson<VendorDetailsResponseModel>();

        Assert.NotNull(postBody);

        var getResponse = await host.Scenario(api =>
        {
            api.Get.Url($"/vendors/{postBody.Id}");
        });

        var getBody = getResponse.ReadAsJson<VendorDetailsResponseModel>();
        Assert.NotNull(getBody);

        var ts = postBody.CreatedOn - getBody.CreatedOn;

        Assert.Equal(postBody, getBody);

    }

    [Fact]
    public async Task InputsAreValidated()
    {
        var host = await AlbaHost.For<Program>();



        var postResponse = await host.Scenario(api =>
        {
            api.Post.Json(new { }).ToUrl("/vendors");
            api.StatusCodeShouldBe(400);
        });
    }
}
