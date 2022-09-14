using CarManager.Application.Abstractions.Cqrs.Security;

namespace CarManager.Infrastructure.Security;

internal static class Extensions
{
    public static IServiceCollection AddSecurity(this IServiceCollection services)
    {
        services.AddSingleton(x =>
            {
                var authenticationOptions = x.GetRequiredService<AuthOptions>();
                return new TokenValidationParameters
                {
                    ValidIssuer = authenticationOptions.Issuer,
                    ClockSkew = TimeSpan.Zero,
                    RequireAudience = true,
                    ValidateIssuer = true,
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationOptions.SigningKey)),
                    ValidateLifetime = true,
                    ValidateActor = false,
                    ValidateTokenReplay = false,
                    ValidateIssuerSigningKey = true
                };
            })
            .AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, null!);

        services.TryAddSingleton<IIdentity, Identity>();

        services.AddAuthorization(authorization =>
        {
            authorization.AddPolicy("admin", policy => { policy.RequireRole("admin"); });
            authorization.AddPolicy("worker", policy => { policy.RequireRole("worker"); });
        });

        services.AddSingleton<IConfigureOptions<JwtBearerOptions>, ConfigureJwtBearerOptions>()
            .AddSingleton<IAuthManager, AuthManager>();

        services.AddSingleton(x => new AuthOptions(x.GetRequiredService<IConfiguration>()));

        return services;
    }
}