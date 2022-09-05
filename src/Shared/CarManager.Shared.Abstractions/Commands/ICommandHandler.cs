namespace CarManager.Shared.Abstractions.Commands;

public interface ICommandHandler<in TCommand> where TCommand : class, ICommand
{
    ValueTask<Result> HandleAsync(TCommand command, CancellationToken cancellationToken = default);
}