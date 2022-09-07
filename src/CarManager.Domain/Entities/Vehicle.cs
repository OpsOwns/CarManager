namespace CarManager.Domain.Entities;

//TODO implement Vehicle
public class Vehicle : Entity<VehicleId>
{
}

public record VehicleId : Id
{
    public VehicleId() : base()
    {
    }

    public VehicleId(Guid id) : base(id)
    {
    }
}