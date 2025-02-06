using FluentValidation;
using Marten;
using Microsoft.AspNetCore.Mvc;

namespace SoftwareCatalog.Api.Vendors.Endpoints;
public class AddingAVendor(IDocumentSession session, IValidator<VendorCreateModel> validator) : ControllerBase
{
    [HttpPost("/vendors")]
    public async Task<ActionResult> CanAddVendorAsync(
        [FromBody] VendorCreateModel request)
    {
        var validations = await validator.ValidateAsync(request);
        if (!validations.IsValid)
        {
            return BadRequest();
        }
        var entity = new VendorEntity
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Link = request.Link,
            CreatedOn = DateTimeOffset.UnixEpoch
        };
        session.Store(entity);
        await session.SaveChangesAsync();
        var response = entity.MapToModel();
        return StatusCode(201, response);
    }
}