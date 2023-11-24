using Backend.Bussiness;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Presentation;

[ApiController]
[Route("get")]
public class GetTaskController : ControllerBase
{
    [HttpGet("task/{taskId}")]
    public IActionResult Handle([FromServices] ByIdTask.Handler service, int taskId)
    {
        var command = new ByIdTask.Command() { Id = taskId };
        var response = service.Handle(command);
        return Ok(response);
    }
}
