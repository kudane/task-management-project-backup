using Backend.Bussiness;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Presentation;

[ApiController]
[Route("list")]
public class ListTaskController : ControllerBase
{
    [HttpGet("task")]
    public IActionResult Handle(
        [FromServices] ListTask.Handler service,
        [FromQuery] int? type,
        [FromQuery] int? priority)
    {
        var command = new ListTask.Command() { TypeId = type, PriorityId = priority };
        var response = service.Handle(command);
        return Ok(response);
    }
}
