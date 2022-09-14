namespace CarManager.Infrastructure.Database.Repositories;

internal sealed class CarRepository : ICarRepository
{
    public Task<Car?> GetByIdAsync(CarId carId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(Car car, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Car car)
    {
        throw new NotImplementedException();
    }
}