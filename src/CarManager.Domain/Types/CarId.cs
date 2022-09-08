namespace CarManager.Domain.Types;

public record CarId : Id
{
    public CarId()
    {
    }

    public CarId(Guid id) : base(id)
    {
    }
}