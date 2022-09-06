﻿using RefreshToken = CarManager.Shared.Abstractions.Security.RefreshToken;

namespace CarManager.Infrastructure.Security;

internal class AuthManager : IAuthManager
{
    private readonly IClock _clock;
    private const string DefaultRole = "Worker";
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

    public JsonWebToken CreateToken(string userId, string email, string? role = null,
        IDictionary<string, IEnumerable<string>>? claims = null)
    {
        Ensure.NotEmpty(userId, "User ID can't be empty", nameof(userId));
        var now = _clock.Now;

        var jwtClaims = new List<Claim>
        {
            new(JwtRegisteredClaimNames_.Sub, userId),
            new(JwtRegisteredClaimNames_.UniqueName, userId),
            new(JwtRegisteredClaimNames_.Email, email),
            new(JwtRegisteredClaimNames_.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames_.Iat, new DateTimeOffset(now).ToUnixTimeMilliseconds().ToString()),
            new Claim(JwtRegisteredClaimNames_.Aud, _options.Audience),
            string.IsNullOrWhiteSpace(role) ? new Claim(ClaimTypes.Role, DefaultRole) : new Claim(ClaimTypes.Role, role)
        };

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
            audience: _options.Audience,
            signingCredentials: _signingCredentials
        );

        var token = new JwtSecurityTokenHandler().WriteToken(jwt);

        return new JsonWebToken(token, new RefreshToken(GenerateRefreshToken(), now.Add(_options.ExpireRefreshToken)),
            expires, now.Date);
    }


    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var generator = RandomNumberGenerator.Create();
        generator.GetBytes(randomNumber);
        var token = Convert.ToBase64String(randomNumber);

        return token;
    }
}