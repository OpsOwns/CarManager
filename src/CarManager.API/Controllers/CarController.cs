using CarManager.API.Requests.Car;

namespace CarManager.API.Controllers;

[Authorize]
internal sealed class CarController : ApiController
{
    public CarController(IDispatcher dispatcher) : base(dispatcher)
    {
    }

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

    [HttpPost("{carId:guid}/upload")]
    [SwaggerOperation("Upload image car")]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UploadImage([FromRoute] Guid carId, [FromForm] UploadImageCarRequest file)
    {
        var result = await Dispatcher.SendAsync(new UploadImageCarCommand(new CarId(carId),
            await file.File.Convert(HttpContext.RequestAborted)));

        if (result.IsFailure)
        {
            return BadRequest(result);
        }

        return Accepted();
    }
}