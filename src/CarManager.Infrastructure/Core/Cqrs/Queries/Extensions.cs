namespace CarManager.Infrastructure.Core.Cqrs.Queries;

public static class Extensions
{
    public static IServiceCollection AddCqrsDispatcher(this IServiceCollection services)
    {
        services.AddSingleton<IDispatcher, Dispatcher>();
        return services;
    }
}