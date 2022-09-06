using Microsoft.AspNetCore.Mvc;

namespace CarManager.Shared.Infrastructure.Api;

[ApiController]
[Route("api")]
public class ApiController : ControllerBase
{
    protected ApiController(IDispatcher dispatcher)
    {
        Dispatcher = dispatcher ?? throw new ArgumentNullException(nameof(dispatcher));
    }

    protected IDispatcher Dispatcher { get; }

    protected IActionResult BadRequest(IEnumerable<Error> error)
    {
        return BadRequest(new ErrorResponse(error));
    }

    protected IActionResult BadRequest(Error error)
    {
        return BadRequest(new ErrorResponse(error));
    }
}