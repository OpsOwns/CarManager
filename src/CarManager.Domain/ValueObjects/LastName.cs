namespace CarManager.Domain.ValueObjects;

public class LastName : ValueObject
{
    private LastName(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<LastName> Create(string value)
    {
        if (string.IsNullOrEmpty(value))
            return Result.Failure<LastName>(DomainErrors.General.ValueIsRequired());

        if (value.Length < 3)
            return Result.Failure<LastName>(DomainErrors.General.ValueIsTooShort(3));

        if (value.Length > 35)
            return Result.Failure<LastName>(DomainErrors.General.ValueIsTooLong(13));

        return new LastName(value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}