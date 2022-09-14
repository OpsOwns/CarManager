using CarManager.Application.Abstractions.Cqrs.Security;

namespace CarManager.Infrastructure.Queries.User;

internal sealed class UserInfoQueryHandler : IQueryHandler<UserInfoQuery, UserInfoResponse>
{
    private readonly DbSet<Domain.Entities.User> _users;
    private readonly IIdentity _identity;

    public UserInfoQueryHandler(CarManagerContext carManagerContext, IIdentity identity)
    {
        _users = carManagerContext.Users;
        _identity = identity;
    }

    public async ValueTask<UserInfoResponse> HandleAsync(UserInfoQuery query,
        CancellationToken cancellationToken = default)
    {
        var user = await _users.SingleAsync(x => x.Id == new UserId(_identity.UserId),
            cancellationToken);

        return new UserInfoResponse(user.Email, user.FirstName.Value, user.LastName.Value);
    }
}