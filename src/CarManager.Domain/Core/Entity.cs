using CarManager.Domain.Core.Utility;

namespace CarManager.Domain.Core;

public abstract class Entity<TId> : IEquatable<Entity<TId>> where TId : Id
{
    protected Entity()
    {
    }

    protected Entity(TId id) : this()
    {
        Ensure.NotEmpty(id, "The identifier is required.", nameof(id));
        Id = id;
    }

    public TId Id { get; protected set; } = default!;

    public bool Equals(Entity<TId>? other)
    {
        if (other is null)
            return false;
        if (other.GetType() != GetType())
            return false;

        return other.Id == Id;
    }


    public static bool operator ==(Entity<TId>? first, Entity<TId>? second)
    {
        return first is not null && second is not null && first.Equals(second);
    }

    public static bool operator !=(Entity<TId>? first, Entity<TId>? second)
    {
        return !(first == second);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;

        if (ReferenceEquals(this, obj)) return true;

        if (obj.GetType() != GetType()) return false;

        if (obj is not Entity<TId> other) return false;

        if (Id == Guid.Empty || other.Id == Guid.Empty) return false;

        return Id == other.Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode() * 41;
    }
}