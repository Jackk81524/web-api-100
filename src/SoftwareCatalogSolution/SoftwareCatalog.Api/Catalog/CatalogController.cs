using Microsoft.AspNetCore.Mvc;

namespace SoftwareCatalog.Api.Catalog;

public class CatalogController : ControllerBase
{
    [HttpPost("/vendors/microsoft/opensource")]
    public async Task<ActionResult> AddItemToCatalogAsync(
        [FromBody] CatalogItemRequestModel request)
    {
        var fakeResponse = new CatalogItemResponseDetailsModel
        {
            Id = Guid.NewGuid(),
            License = CatalogItemLicenseTypes.OpenSource,
            Name = request.Name,
            Vendor = "Microsoft"
        };
        return StatusCode(201, fakeResponse);
    }
}


public record CatalogItemRequestModel
{
    public string Name { get; set; } = string.Empty;
}

public enum CatalogItemLicenseTypes { OpenSource, Free, Paid };

public record CatalogItemResponseDetailsModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Vendor { get; set; } = string.Empty;

    public CatalogItemLicenseTypes License { get; set; }
}
