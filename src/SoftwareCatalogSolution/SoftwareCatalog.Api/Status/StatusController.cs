using Microsoft.AspNetCore.Mvc;

namespace SoftwareCatalog.Api.Status;

// Must be public and must extend* ControllerBase
public class StatusController : ControllerBase
{
    [HttpGet("/status")]
    public ActionResult GetTheStatus()
    {
        var response = new StatusResponse(DateTimeOffset.Now, "Looks Good!");
        return Ok(response);
    }
}

public record StatusResponse(DateTimeOffset LastChecked, String Message);

