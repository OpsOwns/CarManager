namespace CarManager.Infrastructure.Database.Repositories;

internal sealed class CustomerRepository : ICustomerRepository
{
    private readonly DbSet<Customer> _customers;

    public CustomerRepository(CarManagerContext carManagerContext)
    {
        _customers = carManagerContext.Customers;
    }

    public async Task AddAsync(Customer customer, CancellationToken cancellationToken)
    {
        await _customers.AddAsync(customer, cancellationToken);
    }

    public async Task<bool> IsPeselExists(Pesel pesel, CancellationToken cancellationToken)
    {
        return await _customers.AnyAsync(x => x.Pesel == pesel, cancellationToken);
    }
}