namespace CarManager.API.Controllers;

[Authorize]
internal sealed class CustomerController : ApiController
{
    public CustomerController(IDispatcher dispatcher) : base(dispatcher)
    {
    }

    [HttpPost("create")]
    [SwaggerOperation("Create customer to the system")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerRequest createCustomerRequest)
    {
        var result = await Dispatcher.SendAsync(new CreateCustomerCommand(createCustomerRequest.FirstName, createCustomerRequest.LastName,
                createCustomerRequest.ContactEmail, createCustomerRequest.Phone, createCustomerRequest.Street,
                createCustomerRequest.City, createCustomerRequest.Residence, createCustomerRequest.Pesel),
            HttpContext.RequestAborted);

        if (result.IsFailure)
        {
            return BadRequest(result);
        }

        return Ok();
    }
}