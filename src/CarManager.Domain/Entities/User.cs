namespace CarManager.Domain.Entities;

public sealed class User : Entity<UserId>
{
    public HashedPassword HashedPassword { get; private set; } = null!;
    public Email Email { get; private set; } = null!;
    public LastName LastName { get; private set; } = null!;
    public FirstName FirstName { get; private set; } = null!;
    public RefreshToken RefreshToken { get; private set; } = null!;
    public static User NotFound() => new();

    private User()
    {
    }

    private User(HashedPassword hashedPassword, Email email, FirstName firstName,
        LastName lastName) : base(new UserId())
    {
        HashedPassword = hashedPassword;
        Email = email;
        LastName = lastName;
        FirstName = firstName;
    }

    public static User Register(Email email, Password password, FirstName firstName, LastName lastName)
    {
        var hashed = password.Hash();
        return new User(hashed, email, firstName, lastName);
    }

    public void ChangeRefreshToken(RefreshToken refreshToken)
    {
        RefreshToken = refreshToken;
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

    public static implicit operator string(UserId id)
    {
        return id.ToString();
    }
}