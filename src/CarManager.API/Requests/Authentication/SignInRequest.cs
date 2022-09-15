namespace CarManager.API.Requests.Authentication;

internal sealed record SignInRequest([Required] string Email, [Required] string Password);