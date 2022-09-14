namespace CarManager.Domain.Core;

public record Id
{
    protected Id()
    {
        Value = Guid.NewGuid();
    }

    protected Id(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public override string ToString()
    {
        return Value.ToString();
    }

    public static implicit operator Id(Guid identity)
    {
        return new(identity);
    }

    public static implicit operator Guid(Id identity)
    {
        return identity.Value;
    }
}