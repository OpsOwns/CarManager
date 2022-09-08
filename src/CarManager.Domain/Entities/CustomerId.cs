namespace CarManager.Domain.Entities;

public record CustomerId : Id
{
    public CustomerId() : base()
    {
    }

    public CustomerId(Guid id) : base(id)
    {
    }
}