using CarManager.Domain.Core.Utility;

namespace CarManager.Domain.ValueObjects;

public sealed class HashedPassword : ValueObject
{
    public string Hash { get; }
    public string Salt { get; }

    public HashedPassword(string hash, string salt)
    {
        Ensure.NotNullOrEmpty((hash, nameof(hash)), (salt, nameof(salt)));

        Hash = hash;
        Salt = salt;
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Hash;
        yield return Salt;
    }
}