namespace CarManager.Domain.ValueObjects;

public sealed class HashedPassword : ValueObject
{
    public HashedPassword(string hash, string salt)
    {
        Ensure.NotNullOrEmpty((hash, nameof(hash)), (salt, nameof(salt)));

        Hash = hash;
        Salt = salt;
    }

    public string Hash { get; }
    public string Salt { get; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Hash;
        yield return Salt;
    }
}