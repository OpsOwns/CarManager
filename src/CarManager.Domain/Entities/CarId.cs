namespace CarManager.Domain.Entities;

public record CarId : Id
{
    public CarId() : base()
    {
    }

    public CarId(Guid id) : base(id)
    {
    }
}