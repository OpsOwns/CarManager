namespace CarManager.Domain.ValueObjects;

public class RefreshToken : ValueObject
{
    public string Value { get; }
    public DateTime ExpireTime { get; }
    public DateTime CreationDate { get; }
    public bool Used { get; }

    public RefreshToken(string value, DateTime expireTime, DateTime creationDate)
    {
        Ensure.NotNull(value, "Refresh token can't be null or empty", nameof(RefreshToken));

        Value = value;
        ExpireTime = expireTime;
        CreationDate = creationDate;
        Used = false;
    }


    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}