namespace CarManager.Domain.Repositories;

public interface ICustomerRepository : IRepository
{
    Task AddAsync(Customer customer, CancellationToken cancellationToken);
    Task<bool> IsPeselExists(Pesel pesel, CancellationToken cancellationToken);
}