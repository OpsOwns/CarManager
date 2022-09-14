using CarManager.Infrastructure.Core.Cqrs.Commands;
using CarManager.Infrastructure.Core.Cqrs.Queries;
using CarManager.Infrastructure.Time;

namespace CarManager.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();

        services.AddSingleton<IQueryDispatcher, QueryDispatcher>();
        services.Scan(s => s.FromAssemblies(assemblies)
            .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.AddSingleton<ICommandDispatcher, CommandDispatcher>();
        services.Scan(s => s.FromAssemblies(assemblies)
            .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.AddCqrsDispatcher();
        services.AddSecurity();
        services.AddDatabase();
        services.AddClock();
        services.AddAzureStorage();

        return services;
    }
}