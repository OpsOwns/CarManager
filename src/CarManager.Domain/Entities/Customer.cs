namespace CarManager.Domain.Entities;

//TODO implement Customer
public class Customer : Entity<CustomerId>
{
}

public record CustomerId : Id
{
    public CustomerId() : base()
    {
    }

    public CustomerId(Guid id) : base(id)
    {
    }
}