namespace CarManager.Domain.Entities;

public sealed class User : Entity<UserId>
{
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

    public HashedPassword HashedPassword { get; } = null!;
    public Email Email { get; } = null!;
    public LastName LastName { get; } = null!;
    public FirstName FirstName { get; } = null!;

    public static User Register(Email email, Password password, FirstName firstName, LastName lastName)
    {
        var hashed = password.Hash();
        return new User(hashed, email, firstName, lastName);
    }
}