namespace CarManager.Application.Abstractions.Cqrs.Security;

public record SimpleToken(string AccessToken, string RefreshToken);

public record JsonWebToken(string AccessToken, RefreshToken RefreshToken, DateTime ExpireDate, DateTime CreationDate);

public record RefreshToken(string Value, DateTime ExpireDate);