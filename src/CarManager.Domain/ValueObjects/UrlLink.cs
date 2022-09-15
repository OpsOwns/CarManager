namespace CarManager.Domain.ValueObjects;

public sealed class UrlLink : ValueObject
{
    public string Value { get; }

    private UrlLink(string value)
    {
        Value = value;
    }

    public static Result<UrlLink> Create(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return Result.Failure<UrlLink>(CustomErrors.General.ValueIsRequired());
        }

        if (!Uri.TryCreate(value, UriKind.Absolute, out _))
        {
            return Result.Failure<UrlLink>(CustomErrors.General.InvalidLink(value));
        }

        return new UrlLink(value);
    }


    protected override IEnumerable<object> GetEqualityComponents()
    {
        throw new NotImplementedException();
    }
}