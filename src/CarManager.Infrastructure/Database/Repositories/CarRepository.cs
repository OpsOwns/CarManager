namespace CarManager.Infrastructure.Database.Repositories;

internal sealed class CarRepository : ICarRepository
{
    private readonly DbSet<Car> _cars;

    public CarRepository(CarManagerContext carManagerContext)
    {
        _cars = carManagerContext.Cars;
    }

    public async Task<Car?> GetByIdAsync(CarId carId, CancellationToken cancellationToken)
    {
        return await _cars.SingleOrDefaultAsync(x => x.Id == carId, cancellationToken);
    }

    public async Task AddAsync(Car car, CancellationToken cancellationToken)
    {
        await _cars.AddAsync(car, cancellationToken);
    }

    public async Task UpdateAsync(Car car)
    {
        _cars.Update(car);
        await Task.CompletedTask;
    }
}