namespace CarManager.Domain.ValueObjects;

public class FirstName : ValueObject
{
    private FirstName(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<FirstName> Create(string value)
    {
        if (string.IsNullOrEmpty(value))
            return Result.Failure<FirstName>(DomainErrors.General.ValueIsRequired());

        if (value.Length < 3)
            return Result.Failure<FirstName>(DomainErrors.General.ValueIsTooShort(3));

        if (value.Length > 13)
            return Result.Failure<FirstName>(DomainErrors.General.ValueIsTooLong(13));

        return new FirstName(value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}