namespace CarManager.Application.User.Commands.RefreshToken;

internal sealed class RefreshTokenHandler : ICommandHandler<RefreshTokenCommand>
{
    private readonly IIdentity _identity;
    private readonly IUserRepository _userRepository;
    private readonly IAuthManager _authManager;

    public RefreshTokenHandler(IIdentity identity, IAuthManager authManager, IUserRepository userRepository)
    {
        _identity = identity;
        _authManager = authManager;
        _userRepository = userRepository;
    }

    public async ValueTask<Result> HandleAsync(RefreshTokenCommand command,
        CancellationToken cancellationToken = default)
    {
        _authManager.ValidatePrincipalFromExpiredToken(command.AccessToken);

        var user = await _userRepository.GetByIdAsync(_authManager.UserId, cancellationToken);

        if (user == Domain.Entities.User.NotFound())
        {
            return Result.Failure<Result>(CustomErrors.UserAuth.UserNotFound());
        }

        if (user.RefreshToken is null)
        {
            return Result.Failure<Result>(CustomErrors.UserAuth.RefreshTokenNotFound());
        }

        if (user.RefreshToken.IsTokenExpired())
        {
            return Result.Failure<Result>(CustomErrors.UserAuth.RefreshTokenExpired());
        }

        if (user.RefreshToken.Used)
        {
            return Result.Failure<Result>(CustomErrors.UserAuth.RefreshTokenUsed());
        }

        var token = new Domain.ValueObjects.RefreshToken(user.RefreshToken.Value, user.RefreshToken.ExpireTime,
            user.RefreshToken.CreationDate, true);

        user.ChangeRefreshToken(token);

        await _userRepository.UpdateAsync(user);

        var jwtToken = _authManager.CreateToken(user.Id, user.Email);

        _identity.Set(jwtToken);

        return Result.Success();
    }
}