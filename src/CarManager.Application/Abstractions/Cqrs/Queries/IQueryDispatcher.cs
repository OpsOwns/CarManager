namespace CarManager.Application.Abstractions.Cqrs.Queries;

public interface IQueryDispatcher
{
    ValueTask<TResult> QueryAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default);
}