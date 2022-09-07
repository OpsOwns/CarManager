namespace CarManager.Application.User.Commands.SignUp;

public record SignUpCommand(string FirstName, string LastName, string Email, string Password, string Role) : ICommand;