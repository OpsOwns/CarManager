using CarManager.Application.Abstractions.Cqrs.Commands;

namespace CarManager.Application.User.Commands.RefreshToken;

public sealed record RefreshTokenCommand(string AccessToken, string RefreshToken) : ICommand;