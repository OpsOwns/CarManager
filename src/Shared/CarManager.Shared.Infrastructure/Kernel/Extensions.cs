namespace CarManager.Shared.Infrastructure.Kernel;

public static class Extensions
{
    public static IServiceCollection AddDispatchers(this IServiceCollection services)
    {
        services.AddSingleton<IQueryDispatcher, QueryDispatcher>();
        services.AddSingleton<IDispatcher, Dispatcher>();
        services.AddSingleton<ICommandDispatcher, CommandDispatcher>();

        return services;
    }
}