using CarManager.Domain.Core;

namespace CarManager.Domain.ValueObjects;

public sealed class LastName : ValueObject
{
    public string Value { get; }

    private LastName(string value)
    {
        Value = value;
    }

    public static Result<LastName> Create(string value)
    {
        if (string.IsNullOrEmpty(value))
            return Result.Failure<LastName>(CustomErrors.General.ValueIsRequired());

        if (value.Length < 3)
            return Result.Failure<LastName>(CustomErrors.General.ValueIsTooShort(3));

        if (value.Length > 35)
            return Result.Failure<LastName>(CustomErrors.General.ValueIsTooLong(13));

        return new LastName(value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}