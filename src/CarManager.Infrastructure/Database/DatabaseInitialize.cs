namespace CarManager.Infrastructure.Database;

internal sealed class DatabaseInitialize : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<DatabaseInitialize> _logger;

    public DatabaseInitialize(IServiceProvider serviceProvider, ILogger<DatabaseInitialize> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();

        await scope.ServiceProvider.GetRequiredService<CarManagerContext>().Database
            .EnsureCreatedAsync(cancellationToken);

        _logger.LogInformation("Migrate database success");
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}