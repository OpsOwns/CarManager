namespace CarManager.Domain.ValueObjects;

public class Role : ValueObject
{
    public string Value { get; }
    public static IEnumerable<string> AvailableRoles { get; } = new[] { "worker", "admin" };

    private Role(string value)
    {
        Value = value;
    }

    public static Result<Role> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) return Result.Failure<Role>(CustomErrors.General.ValueIsRequired());

        if (value.Length > 30) return Result.Failure<Role>(CustomErrors.General.ValueIsTooLong(30));

        if (!AvailableRoles.Contains(value))
            return Result.Failure<Role>(CustomErrors.UserAuth.RoleIsOutOfRange(string.Join(',', AvailableRoles)));

        return new Role(value);
    }

    public static Role Admin()
    {
        return new("admin");
    }

    public static Role Worker()
    {
        return new("worker");
    }

    public static implicit operator Role(string value)
    {
        return new(value);
    }

    public static implicit operator string(Role value)
    {
        return value.Value;
    }

    public override string ToString()
    {
        return Value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}