namespace CarManager.API.Controllers;

[Authorize]
public sealed class AuthController : ApiController
{
    public AuthController(IDispatcher dispatcher) : base(dispatcher)
    {
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterRequest registerRequest)
    {
        var result = await Dispatcher.SendAsync(new CreateUserCommand(registerRequest.FirstName,
            registerRequest.LastName,
            registerRequest.Email, registerRequest.Password));

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok();
    }
}