using Backend.Bussiness;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Presentation;

[ApiController]
[Route("delete")]
public class DeleteTaskController : ControllerBase
{
    [HttpDelete("task/{taskId}")]
    public IActionResult Handle([FromServices] DeleteTask.Handler service, int taskId)
    {
        var command = new DeleteTask.Command() { Id = taskId };
        service.Handle(command);
        return Ok();
    }
}
