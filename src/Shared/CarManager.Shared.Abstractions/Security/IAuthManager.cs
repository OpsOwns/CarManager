namespace CarManager.Shared.Abstractions.Security;

public interface IAuthManager
{
    JsonWebToken CreateToken(string userId, string email, string? role = null,
        IDictionary<string, IEnumerable<string>>? claims = null);
}