namespace CarManager.Infrastructure.Database.UnitOfWork;

internal sealed class UnitOfWorkDecorator<TCommand> : ICommandHandler<TCommand> where TCommand : class, ICommand
{
    private readonly ICommandHandler<TCommand> _commandHandler;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UnitOfWorkDecorator<TCommand>> _logger;

    public UnitOfWorkDecorator(ICommandHandler<TCommand> commandHandler, IUnitOfWork unitOfWork,
        ILogger<UnitOfWorkDecorator<TCommand>> logger)
    {
        _commandHandler = commandHandler;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async ValueTask<Result> HandleAsync(TCommand command, CancellationToken cancellationToken = default)
    {
        var commandName = typeof(TCommand).Name;
        _logger.LogInformation("Started handling a command: {CommandName}...", commandName);
        var result = await _unitOfWork.ExecuteAsync(() => _commandHandler.HandleAsync(command, cancellationToken),
            cancellationToken);
        _logger.LogInformation("Completed handling a command: {CommandName}.", commandName);

        return result;
    }
}