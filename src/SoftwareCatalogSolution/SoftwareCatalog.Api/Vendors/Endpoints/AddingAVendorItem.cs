using FluentValidation;
using Marten;
using Microsoft.AspNetCore.Mvc;

namespace SoftwareCatalog.Api.Vendors.Endpoints;

public class AddingAVendorItem(IDocumentSession session, IValidator<VendorItemRequestModel> validator, TimeProvider systemTime) : ControllerBase
{
    [HttpPost("/vendors")]
    public async Task<ActionResult> AddOpenSourceItemToCatalogAsync(
        [FromBody] VendorItemRequestModel request)
    {
        var validationResults = await validator.ValidateAsync(request);

        if (!validationResults.IsValid)
        {
            return BadRequest(validationResults.ToDictionary()); // 400
        }

        var entityToSave = new VendorItemEntity
        {
            Name = request.Name,
            DateTimeAdded = systemTime.GetUtcNow(),
            Url = request.Url
        };

        session.Store(entityToSave);
        await session.SaveChangesAsync();

        var response = new VendorItemResponseDetailsModel(entityToSave.Id, entityToSave.Name, entityToSave.DateTimeAdded)
        {
            Url = entityToSave.Url
        };

        return StatusCode(201, response);
    }


}
