namespace CarManager.Infrastructure.Time;

public static class Extensions
{
    public static IServiceCollection AddClock(this IServiceCollection services)
    {
        services.AddSingleton<IClock, UtcClock>();

        return services;
    }
}