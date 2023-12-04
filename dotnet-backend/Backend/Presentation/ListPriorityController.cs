using Backend.Bussiness;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Presentation;

[ApiController]
[Route("list")]
public class ListPriorityController : ControllerBase
{
    [HttpGet("priority")]
    public IActionResult Handle([FromServices] ListPriority.Handler service)
    {
        var response = service.Handle();
        return Ok(response);
    }
}
