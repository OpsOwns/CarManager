namespace CarManager.Domain.Types;

public record CustomerId : Id
{
    public CustomerId()
    {
    }

    public CustomerId(Guid id) : base(id)
    {
    }
}