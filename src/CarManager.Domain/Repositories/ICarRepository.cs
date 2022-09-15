namespace CarManager.Domain.Repositories;

public interface ICarRepository : IRepository
{
    Task<Car?> GetByIdAsync(CarId carId, CancellationToken cancellationToken);
    Task AddAsync(Car car, CancellationToken cancellationToken);
    Task UpdateAsync(Car car);
}