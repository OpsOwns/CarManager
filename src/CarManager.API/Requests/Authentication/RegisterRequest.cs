namespace CarManager.API.Requests.Authentication;

public record RegisterRequest([Required] string FirstName, [Required] string LastName, [Required] string Password,
    [Required] string Email);