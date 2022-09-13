namespace CarManager.Domain.ValueObjects;

public sealed class FirstName : ValueObject
{
    public string Value { get; }

    private FirstName(string value)
    {
        Value = value;
    }


    public static Result<FirstName> Create(string value)
    {
        if (string.IsNullOrEmpty(value))
            return Result.Failure<FirstName>(CustomErrors.General.ValueIsRequired());

        if (value.Length < 3)
            return Result.Failure<FirstName>(CustomErrors.General.ValueIsTooShort(3));

        if (value.Length > 13)
            return Result.Failure<FirstName>(CustomErrors.General.ValueIsTooLong(13));

        return new FirstName(value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}