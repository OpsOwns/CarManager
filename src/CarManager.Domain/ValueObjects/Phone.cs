namespace CarManager.Domain.ValueObjects;

public class Phone : ValueObject
{
    public string Value { get; }

    private Phone(string value)
    {
        Value = value;
    }

    public static Result<Phone> Create(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return Result.Failure<Phone>(CustomErrors.General.ValueIsRequired());
        }

        return new Phone(value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}