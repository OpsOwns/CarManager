namespace CarManager.Application.User.Commands.LoginUser;

public record LoginUserCommand(string Email, string Password) : ICommand;