namespace CarManager.Shared.Abstractions.Primitives;

public record Id
{
    public Guid Value { get; }

    protected Id()
    {
        Value = Guid.NewGuid();
    }

    protected Id(Guid value)
    {
        Value = value;
    }

    public override string ToString() => Value.ToString();

    public static implicit operator Id(Guid identity) => new(identity);

    public static implicit operator Guid(Id identity) => identity.Value;
}