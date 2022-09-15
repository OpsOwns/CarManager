namespace CarManager.Application.Customer.Commands.CreateCustomer;

public sealed record CreateCustomerCommand(string FirstName, string LastName,
    string ContactEmail, string Phone, string Street, string City, string Residence, string Pesel) : ICommand;