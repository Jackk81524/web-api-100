using Marten;
using Microsoft.AspNetCore.Mvc;

namespace SoftwareCatalog.Api.Catalog;

public class CatalogController(IDocumentSession session) : ControllerBase
{
    [HttpGet("/catalog/{id:guid}")]
    public async Task<ActionResult> GetItemById(Guid id)
    {
        var savedEntity = await session.Query<CatalogItemEntity>().Where(c => c.Id == id).SingleOrDefaultAsync();

        if (savedEntity == null)
        {
            return NotFound();
        }
        else
        {
            return Ok(savedEntity);
        }
    }

    [HttpPost("/vendors/microsoft/opensource")]
    public async Task<ActionResult> AddItemToCatalogAsync(
        [FromBody] CatalogItemRequestModel request)
    {
        var entityToSave = new CatalogItemEntity
        {
            Id = Guid.NewGuid(),
            License = CatalogItemLicenseTypes.OpenSource,
            Name = request.Name,
            Vendor = "Microsoft"
        };

        session.Store(entityToSave);
        await session.SaveChangesAsync();

        var fakeResponse = new CatalogItemResponseDetailsModel
        {
            Id = entityToSave.Id,
            License = entityToSave.License,
            Name = entityToSave.Name,
            Vendor = entityToSave.Vendor
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

public class CatalogItemEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Vendor { get; set; } = string.Empty;
    public CatalogItemLicenseTypes License { get; set; }

}