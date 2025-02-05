using Alba;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Time.Testing;
using SoftwareCatalog.Api.Vendors;

namespace SoftwareCatalog.Tests.Vendors;
public class AddVendor
{
    [Theory]
    [InlineData("Amazon", "https://amazon.com")]
    [InlineData("Amazon", "")]
    [InlineData("Google", "https://google.com")]
    [InlineData("Google", "")]
    [InlineData("Google", "https://")]
    [InlineData("Google", "https://123")]
    [InlineData("Google", "https://test")]
    public async Task CanAddAVendor(string name, string url)
    {
        var Host = await AlbaHost.For<Program>();

        var itemToPost = new VendorItemRequestModel
        {
            Name = name,
            Url = url
        };

        var resource = $"/vendors";
        var response = await Host.Scenario(api =>
        {
            api.Post.Json(itemToPost).ToUrl(resource);
            api.StatusCodeShouldBe(201);
        });

        var responseFromThePost = response.ReadAsJson<VendorItemResponseDetailsModel>();
        Assert.NotNull(responseFromThePost);

        Assert.Equal(responseFromThePost.Name, itemToPost.Name);
        Assert.Equal(responseFromThePost.Url, itemToPost.Url);

        var getResponse = await Host.Scenario(api =>
        {
            api.Get.Url($"/vendors/{responseFromThePost.Id}");
        });

        var responseFromGet = getResponse.ReadAsJson<VendorItemResponseDetailsModel>();
        Assert.NotNull(responseFromGet);

        Assert.Equal(responseFromThePost.Name, responseFromGet.Name);
        Assert.Equal(responseFromThePost.Url, responseFromGet.Url);
        Assert.Equal(responseFromThePost.Id, responseFromGet.Id);
    }

    [Theory]
    [InlineData("Amazon", "https://amazon.com")]
    [InlineData("Amazon", "")]
    [InlineData("Google", "https://google.com")]
    [InlineData("Google", "")]
    [InlineData("Google", "https://")]
    [InlineData("Google", "https://123")]
    [InlineData("Google", "https://test")]
    public async Task CanAddAVendorFakeDate(string name, string url)
    {
        var Host = await AlbaHost.For<Program>(config =>
        {
            var fakeDate = new DateTimeOffset(new DateTime(2001, 12, 08, 11, 59, 00));
            var fakeTimeProvider = new FakeTimeProvider(fakeDate);

            config.ConfigureServices(services =>
            {
                services.AddSingleton<TimeProvider>(_ => fakeTimeProvider);
            });
        });

        var itemToPost = new VendorItemRequestModel
        {
            Name = name,
            Url = url
        };

        var resource = $"/vendors";
        var response = await Host.Scenario(api =>
        {
            api.Post.Json(itemToPost).ToUrl(resource);
            api.StatusCodeShouldBe(201);
        });

        var responseFromThePost = response.ReadAsJson<VendorItemResponseDetailsModel>();
        Assert.NotNull(responseFromThePost);

        Assert.Equal(responseFromThePost.Name, itemToPost.Name);
        Assert.Equal(responseFromThePost.Url, itemToPost.Url);

        var getResponse = await Host.Scenario(api =>
        {
            api.Get.Url($"/vendors/{responseFromThePost.Id}");
        });

        var responseFromGet = getResponse.ReadAsJson<VendorItemResponseDetailsModel>();
        Assert.NotNull(responseFromGet);

        Assert.Equal(responseFromThePost, responseFromGet);

    }

    [Theory]
    [InlineData("")]
    [InlineData("A")]
    [InlineData("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA")]
    public async Task InvalidVendorName(string name)
    {
        var Host = await AlbaHost.For<Program>();

        var itemToPost = new VendorItemRequestModel
        {
            Name = name
        };

        var resource = $"/vendors";
        var response = Host.Scenario(api =>
        {
            api.Post.Json(itemToPost).ToUrl(resource);
            api.StatusCodeShouldBe(400);
        });
    }

    [Theory]
    [InlineData("http:\\testing.com")]
    [InlineData("invalid")]
    [InlineData("1")]
    public async Task InvalidUrlName(string url)
    {
        var Host = await AlbaHost.For<Program>();

        var itemToPost = new VendorItemRequestModel
        {
            Name = "Test",
            Url = url
        };

        var resource = $"/vendors";
        var response = Host.Scenario(api =>
        {
            api.Post.Json(itemToPost).ToUrl(resource);
            api.StatusCodeShouldBe(400);
        });
    }
}
