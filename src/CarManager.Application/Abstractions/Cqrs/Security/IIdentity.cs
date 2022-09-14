namespace CarManager.Application.Abstractions.Cqrs.Security;

public interface IIdentity
{
    Guid UserId { get; }
    void Set(JsonWebToken jonWebToken);
    SimpleToken? Get();
}