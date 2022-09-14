using Microsoft.AspNetCore.Mvc;

namespace CarManager.Shared.Infrastructure.Api;

[ApiController]
[Route("api/[controller]")]
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

    protected IActionResult BadRequest(Result result)
    {
        return BadRequest(new ErrorResponse(result.Error));
    }

    protected IActionResult BadRequest(Error error)
    {
        return BadRequest(new ErrorResponse(error));
    }
}