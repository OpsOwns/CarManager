namespace CarManager.Shared.Abstractions.Common;

public interface IUnitOfWork
{
    ValueTask<Result> ExecuteAsync(Func<ValueTask<Result>> action, CancellationToken cancellationToken);
}