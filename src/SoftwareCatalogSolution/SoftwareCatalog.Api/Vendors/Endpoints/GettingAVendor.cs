
using Marten;
using Microsoft.AspNetCore.Mvc;

namespace SoftwareCatalog.Api.Vendors.Endpoints;

public class GettingAVendor(IDocumentSession session) : ControllerBase
{

    [HttpGet("/vendors/{id:guid}")]
    public async Task<ActionResult> GetVendorAsync([FromRoute] Guid id)
    {
        var response = await session.Query<VendorEntity>()
            .Where(v => v.Id == id)
            .ProjectToModel()
            .SingleOrDefaultAsync();

        return response switch
        {
            null => NotFound(),
            _ => Ok(response)
        };
    }

}