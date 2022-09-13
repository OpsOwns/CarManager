namespace CarManager.API.Controllers;

[Authorize]
public sealed class CarController : ApiController
{
    public CarController(IDispatcher dispatcher) : base(dispatcher)
    {
    }

    [HttpPost("register")]
    [SwaggerOperation("Register car to the system")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterCar([FromBody] RegisterCarRequest registerCarRequest)
    {
        await Dispatcher.SendAsync(new RegisterCarCommand(registerCarRequest.Engine, registerCarRequest.Make,
                registerCarRequest.Model, registerCarRequest.Generation, registerCarRequest.ProductionYear,
                registerCarRequest.FuelType, registerCarRequest.Power, registerCarRequest.BodyType,
                await FormFileConverter.Convert(registerCarRequest.CarImage, HttpContext.RequestAborted)),
            HttpContext.RequestAborted);

        return Ok();
    }
}