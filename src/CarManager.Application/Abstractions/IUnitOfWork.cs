namespace CarManager.Application.Abstractions;

public interface IUnitOfWork
{
    ValueTask<Result> ExecuteAsync(Func<ValueTask<Result>> action, CancellationToken cancellationToken);
}