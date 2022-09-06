namespace CarManager.Shared.Abstractions.Utility;

public static class Ensure
{
    public static void NotEmpty(string value, string message, string argumentName)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException(message, argumentName);
    }

    public static void NotEmpty(Id value, string message, string argumentName)
    {
        if (value.Value == Guid.Empty) throw new ArgumentException(message, argumentName);
    }

    public static void NotEmpty(DateTime value, string message, string argumentName)
    {
        if (value == default) throw new ArgumentException(message, argumentName);
    }

    public static void NotNullOrEmpty(params (string, string)[] values)
    {
        foreach (var value in values)
            if (string.IsNullOrEmpty(value.Item1))
                throw new ArgumentNullException(value.Item2);
    }

    public static void NotNull<T>(T value, string message, string argumentName)
        where T : class
    {
        if (value is null) throw new ArgumentNullException(argumentName, message);
    }
}