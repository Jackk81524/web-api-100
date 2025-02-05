using Marten;
using Microsoft.AspNetCore.Mvc;

namespace SoftwareCatalog.Api.Catalog.Endpoints;

public class AddingACatalogItem(IDocumentSession session) : ControllerBase
{
    // GET /catalog/pizza -> 404


    [HttpPost("/vendors/microsoft/opensource")]
    public async Task<ActionResult> AddOpenSourceItemToCatalogAsync(
        [FromBody] CatalogItemRequestModel request) => await AddCatalogItem(request, CatalogItemLicenceTypes.OpenSource);


    [HttpPost("/vendors/microsoft/paid")]
    public async Task<ActionResult> AddPaidSourceItemToCatalogAsync(
        [FromBody] CatalogItemRequestModel request) => await AddCatalogItem(request, CatalogItemLicenceTypes.Paid);

    private async Task<ActionResult> AddCatalogItem(CatalogItemRequestModel request, CatalogItemLicenceTypes license)
    {
        // TODO: 1. Do Some validation
        // TODO : 2 save it to the database.
        var entityToSave = request.ToCatalogItemEntity("Microsoft", license);

        session.Store(entityToSave);
        await session.SaveChangesAsync(); // Do the actual work and save into the database

        // Step 3: send them the response
        // fake this for right now
        var response = entityToSave.ToCatalogDetailsResponseModel();
        return StatusCode(201, response);
    }
}





