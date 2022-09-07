namespace CarManager.Application.User.Commands.SignIn;

public record SignInCommand(string Email, string Password) : ICommand;