using CarManager.Domain.Core;

namespace CarManager.Domain.Types;

public sealed record CarId : Id
{
    public CarId()
    {
    }

    public CarId(Guid id) : base(id)
    {
    }
}