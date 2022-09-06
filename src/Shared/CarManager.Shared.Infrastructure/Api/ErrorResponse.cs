namespace CarManager.Shared.Infrastructure.Api;

public class ErrorResponse
{
    public ErrorResponse(params Error[] errors)
    {
        Errors = errors;
    }

    public ErrorResponse(IEnumerable<Error> errors)
    {
        Errors = errors.ToImmutableArray();
    }

    public IReadOnlyCollection<Error> Errors { get; }
}