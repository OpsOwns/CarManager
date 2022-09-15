namespace CarManager.API.Requests.Authentication;

internal sealed record SignUpRequest([Required] string FirstName, [Required] string LastName, [Required] string Password,
    [Required] string Email, [Required] string Role);