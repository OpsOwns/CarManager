namespace CarManager.Application.User.Commands.SignUp;

public sealed record SignUpCommand
    (string FirstName, string LastName, string Email, string Password, string Role) : ICommand;