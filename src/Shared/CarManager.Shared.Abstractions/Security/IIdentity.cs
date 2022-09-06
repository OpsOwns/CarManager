namespace CarManager.Shared.Abstractions.Security;

public interface IIdentity<out T> where T : Id
{
    T UserId { get; }
    void Set(JsonWebToken jonWebToken);
    JsonWebToken? Get();
}