

using Alba;

namespace SoftwareCatalog.Tests.Status;
public class CanGetTheStatus
{
    [Fact]
    public async Task GetsA200WhenGettingTheStatus()
    {
        var host = await AlbaHost.For<Program>();

        await host.Scenario(api =>
        {
            api.Get.Url("/status");
            api.StatusCodeShouldBeOk();
        });
    }

}
