using Microsoft.AspNetCore.Mvc;

namespace CarManager.Shared.Infrastructure.Api;

[ApiController]
[Route("api")]
public class ApiController : ControllerBase
{
    private IDispatcher Dispatcher { get; }

    protected ApiController(IDispatcher dispatcher) =>
        Dispatcher = dispatcher ?? throw new ArgumentNullException(nameof(dispatcher));

    protected IActionResult BadRequest(IEnumerable<Error> error) => BadRequest(new ErrorResponse(error));

    protected IActionResult BadRequest(Error error) => BadRequest(new ErrorResponse(error));
}