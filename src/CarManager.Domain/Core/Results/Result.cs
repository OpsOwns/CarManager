namespace CarManager.Domain.Core.Results;

[Serializable]
public class Result
{
    protected Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None) throw new InvalidOperationException();

        if (!isSuccess && error == Error.None) throw new InvalidOperationException();

        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; }

    public static Result Success()
    {
        return new(true, Error.None);
    }

    public static Result<TValue> Success<TValue>(TValue value)
    {
        return new(value, true, Error.None);
    }

    public static Result<TValue> Create<TValue>(TValue? value, Error error)
        where TValue : class
    {
        return value is null ? Failure<TValue>(error) : Success(value);
    }

    public static Result Failure(Error error)
    {
        return new(false, error);
    }

    public static Result<TValue> Failure<TValue>(Error error)
    {
        return new(default!, false, error);
    }

    public static Result Combine(params Result[] results)
    {
        foreach (var result in results)
            if (result.IsFailure)
                return result;

        return Success();
    }
}