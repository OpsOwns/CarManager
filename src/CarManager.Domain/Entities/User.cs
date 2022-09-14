using CarManager.Domain.Core;

namespace CarManager.Domain.Entities;

public sealed class User : Entity<UserId>
{
    public HashedPassword HashedPassword { get; private set; }
    public Email Email { get; private set; }
    public LastName LastName { get; private set; }
    public FirstName FirstName { get; private set; }
    public Role Role { get; private set; }
    public RefreshToken? RefreshToken { get; private set; }
    public static User NotFound() => new();

    private User()
    {
        HashedPassword = default!;
        Email = default!;
        LastName = default!;
        FirstName = default!;
        Role = default!;
    }

    public User(Password password, Email email, FirstName firstName,
        LastName lastName, Role role) : base(new UserId())
    {
        HashedPassword = password.Hash();
        Email = email;
        LastName = lastName;
        FirstName = firstName;
        Role = role;
    }

    public void ChangeRefreshToken(RefreshToken refreshToken)
    {
        RefreshToken = refreshToken;
    }

    public void RemoveToken()
    {
        RefreshToken = null;
    }
}