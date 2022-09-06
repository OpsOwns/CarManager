using CarManager.Shared.Infrastructure.Time;

namespace CarManager.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSecurity();
        services.AddRepositories();
        services.AddClock();

        return services;
    }
}