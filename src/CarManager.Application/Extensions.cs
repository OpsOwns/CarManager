using CarManager.Application.SeedWork.Dispatchers;

namespace CarManager.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddDispatchers();

        return services;
    }
}