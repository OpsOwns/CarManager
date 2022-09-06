using System.Runtime.CompilerServices;

namespace CarManager.Shared.Abstractions.Primitives;

public sealed class Error : ValueObject
{
    public Error(string code, string message, [CallerFilePath] string path = "",
        [CallerMemberName] string methodName = "", [CallerLineNumber] int lineNumber = 0)
    {
        Code = code;
        Message = message;
        FileName = Path.GetFileName(path).Split(".")[0];
        LineNumber = lineNumber;
        MethodName = methodName;
    }

    public string Code { get; }
    public string Message { get; }
    public string MethodName { get; }
    public string FileName { get; }
    public int LineNumber { get; }

    internal static Error None => new(string.Empty, string.Empty);

    public static implicit operator string(Error error)
    {
        return error?.Code ?? string.Empty;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Code;
        yield return Message;
    }
}