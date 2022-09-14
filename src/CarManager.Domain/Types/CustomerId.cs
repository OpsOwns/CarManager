using CarManager.Domain.Core;

namespace CarManager.Domain.Types;

public sealed record CustomerId : Id
{
    public CustomerId()
    {
    }

    public CustomerId(Guid id) : base(id)
    {
    }
}