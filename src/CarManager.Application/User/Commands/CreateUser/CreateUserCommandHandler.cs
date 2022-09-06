namespace CarManager.Application.User.Commands.CreateUser;

public sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand>
{
    public ValueTask<Result> HandleAsync(CreateUserCommand command, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}