using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace CarManager.Infrastructure.Security;

public class Identity : IIdentity<UserId>
{
    private const string TokenKey = "jwt";
    private readonly IHttpContextAccessor _httpContextAccessor;

    public Identity(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }


    JsonWebToken? IIdentity<UserId>.Get()
    {
        if (_httpContextAccessor.HttpContext is null)
        {
            return null;
        }

        if (_httpContextAccessor.HttpContext.Items.TryGetValue(TokenKey, out var jwt))
        {
            return jwt as JsonWebToken;
        }

        return null;
    }

    public void Set(JsonWebToken jonWebToken) =>
        _httpContextAccessor.HttpContext?.Items.TryAdd(TokenKey, jonWebToken.AccessToken);

    public UserId UserId

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

            return userAccountClaim.Value;
        }
    }
}