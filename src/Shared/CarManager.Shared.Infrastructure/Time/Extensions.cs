namespace CarManager.Shared.Infrastructure.Time;

public static class Extensions
{
    public static IServiceCollection AddClock(this IServiceCollection services)
    {
        services.AddTransient<IClock, UtcClock>();

        return services;
    }
}