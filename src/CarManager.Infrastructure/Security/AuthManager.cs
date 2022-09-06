using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CarManager.Shared.Abstractions.Security;
using CarManager.Shared.Abstractions.Utility;
using Microsoft.IdentityModel.Tokens;

namespace CarManager.Infrastructure.Security;

internal class AuthManager : IAuthManager
{
    private readonly IClock _clock;

    private readonly SigningCredentials _signingCredentials;
    private readonly AuthOptions _options;

    public AuthManager(AuthOptions options, IClock clock)
    {
        _clock = clock;
        _signingCredentials =
            new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SigningKey)),
                SecurityAlgorithms.HmacSha256);
        _options = options;
    }

    public JsonWebToken CreateToken(string userId, string? role = null, string? audience = null,
        IDictionary<string, IEnumerable<string>>? claims = null)
    {
        Ensure.NotEmpty(userId, "User ID can't be empty", nameof(userId));
        var now = _clock.Now;

        var jwtClaims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, userId),
            new(JwtRegisteredClaimNames.UniqueName, userId),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Iat, new DateTimeOffset(now).ToUnixTimeMilliseconds().ToString())
        };

        if (!string.IsNullOrWhiteSpace(role))
        {
            jwtClaims.Add(new Claim(ClaimTypes.Role, role));
        }

        if (!string.IsNullOrWhiteSpace(audience))
        {
            jwtClaims.Add(new Claim(JwtRegisteredClaimNames.Aud, audience));
        }

        if (claims?.Any() is true)
        {
            var customClaims = new List<Claim>();
            foreach (var (claim, values) in claims)
            {
                customClaims.AddRange(values.Select(value => new Claim(claim, value)));
            }

            jwtClaims.AddRange(customClaims);
        }

        var expires = now.Add(_options.Expire);

        var jwt = new JwtSecurityToken(
            _options.Issuer,
            claims: jwtClaims,
            notBefore: now,
            expires: expires,
            signingCredentials: _signingCredentials
        );

        var token = new JwtSecurityTokenHandler().WriteToken(jwt);

        return new JsonWebToken(token,, expires, now.Date);
    }
}