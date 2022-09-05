namespace CarManager.Shared.Abstractions.Commands;

public interface ICommandDispatcher
{
    ValueTask<Result> SendAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default)
        where TCommand : class, ICommand;
}