namespace CarManager.Shared.Abstractions.Security;

public record JsonWebToken(string AccessToken, RefreshToken RefreshToken, DateTime ExpireDate, DateTime CreationDate);

public record RefreshToken(string Value, DateTime ExpireDate);