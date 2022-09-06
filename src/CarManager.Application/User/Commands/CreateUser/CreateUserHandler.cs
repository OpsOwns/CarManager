namespace CarManager.Application.User.Commands.CreateUser;

public sealed class CreateUserHandler : ICommandHandler<CreateUserCommand>
{
    private readonly IUserRepository _userRepository;

    public CreateUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async ValueTask<Result> HandleAsync(CreateUserCommand command, CancellationToken cancellationToken = default)
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