namespace CarManager.API.Requests.Authentication;

public sealed record SignInRequest([Required] string Email, [Required] string Password);