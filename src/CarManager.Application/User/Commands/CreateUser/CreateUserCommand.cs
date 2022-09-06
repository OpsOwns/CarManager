namespace CarManager.Application.User.Commands.CreateUser;

public record CreateUserCommand(string FirstName, string LastName, string Email, string Password) : ICommand;