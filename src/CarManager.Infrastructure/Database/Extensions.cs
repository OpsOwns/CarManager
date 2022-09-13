namespace CarManager.Infrastructure.Database;

internal static class Extensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddSingleton(x => new DatabaseOptions(x.GetRequiredService<IConfiguration>()));
        services.AddScoped(x => new CarManagerContext(x.GetRequiredService<DatabaseOptions>()));

        services.Scan(s => s.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
            .AddClasses(c => c.AssignableTo(typeof(IRepository)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.AddScoped<IUnitOfWork, CarManagerUnitOfWork>();

        services.AddHostedService<DatabaseInitialize>();

        services.TryDecorate(typeof(ICommandHandler<>), typeof(UnitOfWorkDecorator<>));

        return services;
    }
}