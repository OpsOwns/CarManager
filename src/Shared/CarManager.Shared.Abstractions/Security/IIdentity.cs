namespace CarManager.Shared.Abstractions.Security;

public interface IIdentity
{
    Guid UserId { get; }
    void Set(JsonWebToken jonWebToken);
    SimpleToken? Get();
}