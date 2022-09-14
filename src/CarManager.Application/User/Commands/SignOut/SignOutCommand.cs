using CarManager.Application.Abstractions.Cqrs.Commands;

namespace CarManager.Application.User.Commands.SignOut;

public sealed record SignOutCommand : ICommand;