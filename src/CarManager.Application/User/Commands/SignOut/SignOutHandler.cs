namespace CarManager.Application.User.Commands.SignOut;

internal sealed class SignOutHandler : ICommandHandler<SignOutCommand>
{
    private readonly IIdentity _identity;
    private readonly IUserRepository _userRepository;

    public SignOutHandler(IIdentity identity, IUserRepository userRepository)
    {
        _identity = identity;
        _userRepository = userRepository;
    }

    public async ValueTask<Result> HandleAsync(SignOutCommand command, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetByIdAsync(_identity.UserId, cancellationToken);

        if (user == Domain.Entities.User.NotFound())
            return Result.Failure<Result>(Errors.UserAuth.UserNotFound());

        if (user.RefreshToken is null)
            return Result.Success();

        user.RemoveToken();

        await _userRepository.UpdateAsync(user);

        return Result.Success();
    }
}