using Marten;
using Microsoft.AspNetCore.Mvc;

namespace SoftwareCatalog.Api.Catalog.Endpoints;

public class GettingCatalogItemDetails : ControllerBase
{
    [HttpGet("/catalog/{id:guid}")]
    public async Task<ActionResult> GetItemById(Guid id, [FromServices] IDocumentSession session)
    {
        var item = await session.Query<CatalogItemEntity>()
            .Where(c => c.Id == id)
            .ProjectToDetailsModel()
            .SingleOrDefaultAsync();


        return item switch
        {
            null => NotFound(),
            _ => Ok(item),
        };
    }
}
