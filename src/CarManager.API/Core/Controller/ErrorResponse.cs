namespace CarManager.API.Core.Controller;

public sealed class ErrorResponse
{
    public IReadOnlyCollection<Error> Errors { get; }

    public ErrorResponse(params Error[] errors)
    {
        Errors = errors;
    }

    public ErrorResponse(IEnumerable<Error> errors)
    {
        Errors = errors.ToImmutableArray();
    }
}