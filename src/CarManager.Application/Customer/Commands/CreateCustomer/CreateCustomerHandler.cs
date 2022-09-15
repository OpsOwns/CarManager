namespace CarManager.Application.Customer.Commands.CreateCustomer;

internal sealed class CreateCustomerHandler : ICommandHandler<CreateCustomerCommand>
{
    private readonly ICustomerRepository _customerRepository;

    public CreateCustomerHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async ValueTask<Result> HandleAsync(CreateCustomerCommand command,
        CancellationToken cancellationToken = default)
    {
        var addressResult = Address.Create(command.Street, command.City, command.Residence);
        var firstNameResult = FirstName.Create(command.FirstName);
        var lastNameResult = LastName.Create(command.LastName);
        var contactEmailResult = Email.Create(command.ContactEmail);
        var phoneResult = Phone.Create(command.Phone);
        var peselResult = Pesel.Create(command.Pesel);

        var combinedResult =
            Result.Combine(addressResult, firstNameResult, lastNameResult, contactEmailResult, phoneResult, peselResult);

        if (combinedResult.IsFailure)
        {
            return combinedResult;
        }

        if (await _customerRepository.IsPeselExists(peselResult.Value, cancellationToken))
        {
            return Result.Failure<Result>(CustomErrors.Customer.PeselAlreadyExists());
        }

        var customer = new Domain.Entities.Customer(firstNameResult.Value, lastNameResult.Value,
            contactEmailResult.Value, phoneResult.Value, addressResult.Value, peselResult.Value);

        await _customerRepository.AddAsync(customer, cancellationToken);

        return Result.Success();
    }
}