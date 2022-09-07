namespace CarManager.API.Controllers;

[Authorize]
public sealed class AuthController : ApiController
{
    private readonly IIdentity _identity;

    public AuthController(IDispatcher dispatcher, IIdentity identity) : base(dispatcher)
    {
        _identity = identity;
    }

    [HttpPost("sign-up")]
    [AllowAnonymous]
    [SwaggerOperation("Create the user account")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SignUp([FromBody] SignUpRequest signUpRequest)
    {
        var result = await Dispatcher.SendAsync(new SignUpCommand(signUpRequest.FirstName,
            signUpRequest.LastName,
            signUpRequest.Email, signUpRequest.Password, signUpRequest.Role), HttpContext.RequestAborted);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok();
    }

    [HttpPost("sign-in")]
    [AllowAnonymous]
    [SwaggerOperation("SignIn user and return the JSON Web Token")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(JsonWebToken), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SignIn([FromBody] SignInRequest signInRequest)
    {
        var result = await Dispatcher.SendAsync(new SignInCommand(signInRequest.Email, signInRequest.Password),
            HttpContext.RequestAborted);

        if (result.IsFailure)
            return BadRequest(result);

        return Ok(_identity.Get());
    }

    [HttpPost("sign-out")]
    [SwaggerOperation("Sign out user")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> LogoutUser()
    {
        var result = await Dispatcher.SendAsync(new SignOutCommand(),
            HttpContext.RequestAborted);

        if (result.IsFailure)
            return BadRequest(result);

        return NoContent();
    }

    [HttpPost("refresh")]
    [AllowAnonymous]
    [SwaggerOperation("Refresh the JSON Web Token")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(JsonWebToken), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SignIn([FromBody] RefreshJsonWebTokenRequest refreshJsonWebTokenRequest)
    {
        var result = await Dispatcher.SendAsync(
            new RefreshTokenCommand(refreshJsonWebTokenRequest.AccessToken, refreshJsonWebTokenRequest.RefreshToken),
            HttpContext.RequestAborted);

        if (result.IsFailure)
            return BadRequest(result);

        return Ok(_identity.Get());
    }

    [SwaggerOperation("Return information about current sign-in user")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet("me")]
    public async Task<ActionResult> Info()
    {
        return Ok(await Dispatcher.QueryAsync(new UserInfoQuery()));
    }
}