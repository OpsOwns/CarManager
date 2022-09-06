namespace CarManager.Shared.Abstractions.Security;

public record JsonWebToken(string AccessToken, string RefreshToken, DateTime ExpireDate, DateTime CreationDate);