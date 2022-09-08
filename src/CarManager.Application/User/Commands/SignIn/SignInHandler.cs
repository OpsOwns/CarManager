namespace CarManager.Application.User.Commands.SignIn;

internal sealed class  SignInHandler : ICommandHandler<SignInCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IIdentity _identity;
    private readonly IAuthManager _authManager;

    public SignInHandler(IUserRepository userRepository, IIdentity identity, IAuthManager authManager)
    {
        _userRepository = userRepository;
        _identity = identity;
        _authManager = authManager;
    }

    public async ValueTask<Result> HandleAsync(SignInCommand command, CancellationToken cancellationToken = default)
    {
        var emailResult = Email.Create(command.Email);
        var passwordResult = Password.Create(command.Password);
        var combinedResult = Result.Combine(emailResult, passwordResult);

        if (combinedResult.IsFailure)
        {
            return combinedResult;
        }

        var existingUser = await _userRepository.GetByEmailAsync(emailResult.Value, cancellationToken);

        if (existingUser == Domain.Entities.User.NotFound())
        {
            return Result.Failure<Result>(CustomErrors.UserAuth.UserNotFoundByEmail(emailResult.Value.Value));
        }

        var passwordValid = passwordResult.Value.IsMatch(existingUser.HashedPassword);

        if (!passwordValid)
        {
            return Result.Failure<Result>(CustomErrors.UserAuth.InvalidPassword());
        }

        var jsonWebToken = _authManager.CreateToken(existingUser.Id.ToString(), existingUser.Email);

        var token = new Domain.ValueObjects.RefreshToken(jsonWebToken.RefreshToken.Value,
            jsonWebToken.RefreshToken.ExpireDate,
            jsonWebToken.CreationDate);

        existingUser.ChangeRefreshToken(token);

        await _userRepository.UpdateAsync(existingUser);

        _identity.Set(jsonWebToken);

        return Result.Success();
    }
}