namespace CarManager.Domain.ValueObjects;

public sealed class Email : ValueObject
{
    public string Value { get; }

    private Email(string value)
    {
        Value = value;
    }


    public static Result<Email> Create(string value)
    {
        if (string.IsNullOrEmpty(value))
            return Result.Failure<Email>(CustomErrors.General.ValueIsRequired());

        value = value.Trim();

        if (value.Length > 200)
            return Result.Failure<Email>(CustomErrors.General.ValueIsTooLong(200));

        if (!Regex.IsMatch(value, @"^(.+)@(.+)$"))
            return Result.Failure<Email>(CustomErrors.General.ValueIsInvalid());

        return Result.Success(new Email(value));
    }

    public static implicit operator string(Email email)
    {
        return email.Value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}