namespace CarManager.Application.User.Commands.LoginUser;

public class LoginUserHandler : ICommandHandler<LoginUserCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IIdentity<UserId> _identity;
    private readonly IAuthManager _authManager;

    public LoginUserHandler(IUserRepository userRepository, IIdentity<UserId> identity, IAuthManager authManager)
    {
        _userRepository = userRepository;
        _identity = identity;
        _authManager = authManager;
    }

    public async ValueTask<Result> HandleAsync(LoginUserCommand command, CancellationToken cancellationToken = default)
    {
        var emailResult = Email.Create(command.Email);
        var passwordResult = Password.Create(command.Password);

        var combinedResult = Result.Combine(emailResult, passwordResult);

        if (combinedResult.IsFailure)
        {
            return combinedResult;
        }

        var existingUser = await _userRepository.GetByEmailAsync(emailResult.Value, cancellationToken);

        if (existingUser is null)
        {
            return Result.Failure<Result>(Errors.UserAuth.UserNotFoundByEmail(emailResult.Value.Value));
        }

        var passwordValid = passwordResult.Value.IsMatch(existingUser.HashedPassword);

        if (!passwordValid)
        {
            return Result.Failure<Result>(Errors.UserAuth.InvalidPassword());
        }

        var jsonWebToken = _authManager.CreateToken(existingUser.Id);

        var token = new RefreshToken(jsonWebToken.RefreshToken, jsonWebToken.ExpireDate, jsonWebToken.CreationDate);

        existingUser.ChangeRefreshToken(token);

        await _userRepository.UpdateAsync(existingUser);

        _identity.Set(jsonWebToken);

        return Result.Success();
    }
}