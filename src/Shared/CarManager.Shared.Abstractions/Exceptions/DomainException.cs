namespace CarManager.Shared.Abstractions.Exceptions;

[Serializable]
public class DomainException : Exception
{
    private const string DefaultCode = "systemError";
    public string Code { get; } = DefaultCode;

    public DomainException(string message) : base(message)
    {
    }

    protected DomainException(string code, string message) : base(message)
    {
        Code = code ?? throw new ArgumentNullException(nameof(code));
    }
}