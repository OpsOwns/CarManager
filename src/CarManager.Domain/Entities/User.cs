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

    private User()
    {
    }

    private User(HashedPassword hashedPassword, Email email, FirstName firstName,
        LastName lastName, Role role) : base(new UserId())
    {
        HashedPassword = hashedPassword;
        Email = email;
        LastName = lastName;
        FirstName = firstName;
        Role = role;
    }

    public static User Register(Email email, Password password, FirstName firstName, LastName lastName, Role role)
    {
        var hashed = password.Hash();
        return new User(hashed, email, firstName, lastName, role);
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

public record UserId : Id
{
    public UserId()
    {
    }

    public UserId(Guid id) : base(id)
    {
    }

    public static implicit operator UserId(string userAccountId)
    {
        if (!Guid.TryParse(userAccountId, out var userId))
        {
            throw new InvalidCastException(nameof(userAccountId));
        }

        return new UserId(userId);
    }

    public override string ToString()
    {
        return Value.ToString();
    }

    public static implicit operator UserId(Guid id)
    {
        return new UserId(id);
    }

    public static implicit operator string(UserId id)
    {
        return id.Value.ToString();
    }
}