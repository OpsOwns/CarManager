namespace CarManager.Domain.ValueObjects;

public class Email : ValueObject
{
    private Email(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<Email> Create(string value)
    {
        if (string.IsNullOrEmpty(value))
            return Result.Failure<Email>(DomainErrors.General.ValueIsRequired());

        value = value.Trim();

        if (value.Length > 200)
            return Result.Failure<Email>(DomainErrors.General.ValueIsTooLong(200));

        if (!Regex.IsMatch(value, @"^(.+)@(.+)$"))
            return Result.Failure<Email>(DomainErrors.General.ValueIsInvalid());

        return Result.Success(new Email(value));
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}