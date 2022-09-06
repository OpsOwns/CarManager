namespace CarManager.Domain;

public record UserId : Id
{
    public UserId()
    {
    }

    public UserId(Guid id) : base(id)
    {
    }
}