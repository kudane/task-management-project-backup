using Backend.Bussiness;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Presentation;

[ApiController]
[Route("edit")]
public class EditTaskController : ControllerBase
{
    [HttpPut("task")]
    public IActionResult Handle(
        [FromServices] EditTask.Handler service,
        [FromBody] EditTask.Command command)
    {
        service.Handle(command);
        return Ok();
    }
}
