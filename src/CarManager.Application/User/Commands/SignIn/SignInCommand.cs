namespace CarManager.Application.User.Commands.SignIn;

public sealed record SignInCommand(string Email, string Password) : ICommand;