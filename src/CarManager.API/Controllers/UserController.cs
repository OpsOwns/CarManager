namespace CarManager.API.Controllers;

public sealed class UserController : ApiController
{
    public UserController(IDispatcher dispatcher) : base(dispatcher)
    {
    }

    [HttpGet]
    public IActionResult Throw()
    {
        throw new ArgumentException();
    }
}