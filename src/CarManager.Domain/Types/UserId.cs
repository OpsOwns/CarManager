using CarManager.Domain.Core;

namespace CarManager.Domain.Types;

public sealed record UserId : Id
{
    public UserId()
    {
    }

    public UserId(Guid id) : base(id)
    {
    }

    public static implicit operator UserId(string userAccountId)
    {
        if (!Guid.TryParse(userAccountId, out var userId))
        {
            throw new InvalidCastException(nameof(userAccountId));
        }

        return new UserId(userId);
    }

    public override string ToString()
    {
        return Value.ToString();
    }

    public static implicit operator UserId(Guid id)
    {
        return new UserId(id);
    }

    public static implicit operator string(UserId id)
    {
        return id.Value.ToString();
    }
}