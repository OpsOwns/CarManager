namespace CarManager.Domain.Entities;

public sealed class User : Entity<UserId>
{
    public HashedPassword HashedPassword { get; private set; } = null!;
    public Email Email { get; private set; } = null!;
    public LastName LastName { get; private set; } = null!;
    public FirstName FirstName { get; private set; } = null!;
    public Role Role { get; private set; } = null!;
    public RefreshToken? RefreshToken { get; private set; }
    public static User NotFound() => new();

    private User() { }

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