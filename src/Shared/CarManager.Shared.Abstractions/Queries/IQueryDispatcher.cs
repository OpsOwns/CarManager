namespace CarManager.Shared.Abstractions.Queries;

public interface IQueryDispatcher
{
    ValueTask<TResult> QueryAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default);
}