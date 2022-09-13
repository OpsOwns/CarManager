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

        var context = scope.ServiceProvider.GetRequiredService<CarManagerContext>();

        await context.Database
            .EnsureCreatedAsync(cancellationToken);

        _logger.LogInformation("Initialize database success");
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}