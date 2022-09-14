namespace CarManager.Application.Abstractions.Cqrs.Security;

public interface IAuthManager
{
    JsonWebToken CreateToken(string userId, string email, string? role = null,
        IDictionary<string, IEnumerable<string>>? claims = null);
    Guid UserId { get; }

    void ValidatePrincipalFromExpiredToken(string token);
}