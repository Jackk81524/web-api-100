using Microsoft.AspNetCore.Mvc;

namespace SoftwareCatalog.Api.Status;


// Have to be public classes. And they have to extend* ControllerBase
public class StatusController : ControllerBase
{
    // GET /status
    [HttpGet("/status")]
    public ActionResult GetTheStatus()
    {
        // 
        // this is fake...
        var response = new StatusResponse(DateTimeOffset.Now, "Looks Good!");
        return Ok(response);
    }
}


public record StatusResponse(DateTimeOffset LastChecked, String Message);