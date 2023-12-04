using Backend.Bussiness;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Presentation;

[ApiController]
[Route("new")]
public class NewTaskController : ControllerBase
{
    [HttpPost("task")]
    public IActionResult Handle(
        [FromServices] NewTask.Handler service,
        [FromBody] NewTask.Command command)
    {
        service.Handle(command);
        return Ok();
    }
}
