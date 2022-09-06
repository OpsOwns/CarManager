namespace CarManager.API.Requests.Authentication;

public record SignInRequest([Required] string Email, [Required] string Password);