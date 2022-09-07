namespace CarManager.Infrastructure.Queries.User;

public class UserInfoQueryHandler : IQueryHandler<UserInfoQuery, UserInfoResponse>
{
    public ValueTask<UserInfoResponse> HandleAsync(UserInfoQuery query, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}