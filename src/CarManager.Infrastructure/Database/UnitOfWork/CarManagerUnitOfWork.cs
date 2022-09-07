namespace CarManager.Infrastructure.Database.UnitOfWork;

internal sealed class CarManagerUnitOfWork : IUnitOfWork
{
    private readonly CarManagerContext _dbContext;

    public CarManagerUnitOfWork(CarManagerContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async ValueTask<Result> ExecuteAsync(Func<ValueTask<Result>> action, CancellationToken cancellationToken)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            var result = await action();

            if (result.IsFailure)
                return result;

            await _dbContext.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            return Result.Success();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}