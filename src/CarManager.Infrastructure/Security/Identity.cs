using CarManager.Application.Abstractions.Cqrs.Security;

namespace CarManager.Infrastructure.Security;

public sealed class Identity : IIdentity
{
    private const string TokenKey = "jwt";
    private readonly IHttpContextAccessor _httpContextAccessor;

    public Identity(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }


    public SimpleToken? Get()
    {
        if (_httpContextAccessor.HttpContext is null)
        {
            return null;
        }

        if (_httpContextAccessor.HttpContext.Items.TryGetValue(TokenKey, out var jwt))
        {
            return jwt as SimpleToken;
        }

        return null;
    }

    public void Set(JsonWebToken jsonWebToken) =>
        _httpContextAccessor.HttpContext?.Items.TryAdd(TokenKey,
            new SimpleToken(jsonWebToken.AccessToken, jsonWebToken.RefreshToken.Value));

    public Guid UserId
    {
        get
        {
            if (_httpContextAccessor.HttpContext?.User is null)
            {
                throw new InvalidOperationException("HttpContext is null or HttpContext.User");
            }

            var userAccountClaim =
                _httpContextAccessor.HttpContext.User.Claims
                    .FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub);

            if (userAccountClaim is null)
            {
                throw new InvalidOperationException($"Claim {ClaimTypes.NameIdentifier} not found");
            }

            if (!Guid.TryParse(userAccountClaim.Value, out var result))
            {
                throw new InvalidCastException("Invalid cast claim userId");
            }

            return result;
        }
    }
}