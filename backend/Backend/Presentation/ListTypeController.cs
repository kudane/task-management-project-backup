using Backend.Bussiness;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Presentation;

[ApiController]
[Route("list")]
public class ListTypeController : ControllerBase
{
    [HttpGet("type")]
    public IActionResult Handle([FromServices] ListType.Handler service)
    {
        var response = service.Handle();
        return Ok(response);
    }
}
