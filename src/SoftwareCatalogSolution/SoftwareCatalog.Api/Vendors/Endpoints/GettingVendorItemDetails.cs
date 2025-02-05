using Marten;
using Microsoft.AspNetCore.Mvc;

namespace SoftwareCatalog.Api.Vendors.Endpoints;

public class GettingVendorsItemDetails : ControllerBase
{
    [HttpGet("/vendors/{id:guid}")]
    public async Task<ActionResult> GetItemById(Guid id, [FromServices] IDocumentSession session)
    {
        var item = await session.Query<VendorItemEntity>()
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
