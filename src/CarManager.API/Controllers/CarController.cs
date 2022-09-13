namespace CarManager.API.Controllers;

[Authorize]
public sealed class CarController : ApiController
{
    public CarController(IDispatcher dispatcher) : base(dispatcher)
    {
    }

    [AllowAnonymous]
    [HttpPost("register")]
    [SwaggerOperation("Register car to the system")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterCar([FromForm] RegisterCarRequest registerCarRequest)
    {
        var result = await Dispatcher.SendAsync(new RegisterCarCommand(registerCarRequest.Engine,
                registerCarRequest.Make,
                registerCarRequest.Model, registerCarRequest.Generation, registerCarRequest.ProductionYear,
                registerCarRequest.FuelType, registerCarRequest.Power, registerCarRequest.BodyType),
            HttpContext.RequestAborted);

        if (result.IsFailure)
            return BadRequest(result);

        return Ok();
    }
}