namespace CarManager.Application.User.Commands.SignUp;

internal sealed class SignUpHandler : ICommandHandler<SignUpCommand>
{
    private readonly IUserRepository _userRepository;

    public SignUpHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async ValueTask<Result> HandleAsync(SignUpCommand command, CancellationToken cancellationToken = default)
    {
        var emailResult = Email.Create(command.Email);
        var passwordResult = Password.Create(command.Password);
        var firstNameResult = FirstName.Create(command.FirstName);
        var lastNameResult = LastName.Create(command.LastName);
        var roleResult = Role.GetValueByName(command.Role);
        var combinedResult = Result.Combine(emailResult, passwordResult, firstNameResult, lastNameResult, roleResult);

        if (combinedResult.IsFailure)
        {
            return Result.Failure(combinedResult.Error);
        }

        var emailExists = await _userRepository.IsEmailUniqueAsync(emailResult.Value, cancellationToken);

        if (emailExists)
        {
            return Result.Failure(CustomErrors.UserAuth.EmailAlreadyExists(emailResult.Value.Value));
        }

        var user = new Domain.Entities.User(passwordResult.Value, emailResult.Value, firstNameResult.Value,
            lastNameResult.Value, roleResult.Value);

        await _userRepository.AddAsync(user, cancellationToken);

        return Result.Success();
    }
}