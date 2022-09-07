namespace CarManager.Application.User.Queries.UserInformation;

public record UserInfoResponse
{
    public string Email { get; }
    public string FirstName { get; }
    public string LastName { get; }

    public UserInfoResponse(string email, string firstName, string lastName)
    {
        Email = email;
        FirstName = firstName;
        LastName = lastName;
    }
}