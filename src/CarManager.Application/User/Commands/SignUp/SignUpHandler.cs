namespace CarManager.Application.User.Commands.SignUp;

public sealed class SignUpHandler : ICommandHandler<SignUpCommand>
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

        var combinedResult = Result.Combine(emailResult, passwordResult, firstNameResult, lastNameResult);

        if (combinedResult.IsFailure)
        {
            return Result.Failure(combinedResult.Error);
        }

        var emailUnique = await _userRepository.IsEmailUniqueAsync(emailResult.Value, cancellationToken);

        if (!emailUnique)
        {
            return Result.Failure(Errors.UserAuth.EmailAlreadyExists(emailResult.Value.Value));
        }

        var user = Domain.Entities.User.Register(emailResult.Value, passwordResult.Value, firstNameResult.Value,
            lastNameResult.Value);

        await _userRepository.AddAsync(user, cancellationToken);

        return Result.Success();
    }
}