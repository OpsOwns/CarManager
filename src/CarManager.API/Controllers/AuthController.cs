using CarManager.Application.User.Commands.LoginUser;
using Swashbuckle.AspNetCore.Annotations;

namespace CarManager.API.Controllers;

[Authorize]
public sealed class AuthController : ApiController
{
    private readonly IIdentity<UserId> _identity;

    public AuthController(IDispatcher dispatcher, IIdentity<UserId> identity) : base(dispatcher)
    {
        _identity = identity;
    }

    [HttpPost("sign-up")]
    [AllowAnonymous]
    [SwaggerOperation("Create the user account")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SignUp([FromBody] SignUpRequest signUpRequest)
    {
        var result = await Dispatcher.SendAsync(new CreateUserCommand(signUpRequest.FirstName,
            signUpRequest.LastName,
            signUpRequest.Email, signUpRequest.Password), HttpContext.RequestAborted);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok();
    }

    [HttpPost("sign-in")]
    [AllowAnonymous]
    [SwaggerOperation("SignIn user and return the JSON Web Token")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SignIn([FromBody] SignInRequest signInRequest)
    {
        var result = await Dispatcher.SendAsync(new LoginUserCommand(signInRequest.Email, signInRequest.Password),
            HttpContext.RequestAborted);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(_identity.Get());
    }
}