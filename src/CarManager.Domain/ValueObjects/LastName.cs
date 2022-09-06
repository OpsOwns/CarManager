namespace CarManager.Domain.ValueObjects;

public class LastName : ValueObject
{
    public string Value { get; }

    private LastName(string value)
    {
        Value = value;
    }
    
    public static Result<LastName> Create(string value)
    {
        if (string.IsNullOrEmpty(value))
            return Result.Failure<LastName>(Errors.Errors.General.ValueIsRequired());

        if (value.Length < 3)
            return Result.Failure<LastName>(Errors.Errors.General.ValueIsTooShort(3));

        if (value.Length > 35)
            return Result.Failure<LastName>(Errors.Errors.General.ValueIsTooLong(13));

        return new LastName(value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}