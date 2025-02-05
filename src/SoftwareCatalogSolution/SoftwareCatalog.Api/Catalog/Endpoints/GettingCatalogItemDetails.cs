using Marten;
using Microsoft.AspNetCore.Mvc;

namespace SoftwareCatalog.Api.Catalog.Endpoints;

public class GettingCatalogItemDetails : ControllerBase
{
    [HttpGet("/catalog/{id:guid}")]
    public async Task<ActionResult> GetItemById(Guid id, [FromServices] IDocumentSession session)
    {
        var savedEntity = await session.Query<CatalogItemEntity>().Where(c => c.Id == id).SingleOrDefaultAsync();

        if (savedEntity == null)
        {
            return NotFound();
        }
        else
        {
            var response = new CatalogItemResponseDetailsModel
            {
                Id = savedEntity.Id,
                Licence = savedEntity.Licence,
                Name = savedEntity.Name,
                Vendor = savedEntity.Vendor
            };
            return Ok(savedEntity);
        }
    }
}
