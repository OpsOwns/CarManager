namespace CarManager.Application.User.Commands.RefreshToken;

public record RefreshTokenCommand(string AccessToken, string RefreshToken) : ICommand;