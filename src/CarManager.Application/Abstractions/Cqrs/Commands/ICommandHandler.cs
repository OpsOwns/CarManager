namespace CarManager.Application.Abstractions.Cqrs.Commands;

public interface ICommandHandler<in TCommand> where TCommand : class, ICommand
{
    ValueTask<Result> HandleAsync(TCommand command, CancellationToken cancellationToken = default);
}