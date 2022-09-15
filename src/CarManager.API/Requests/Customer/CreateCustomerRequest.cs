namespace CarManager.API.Requests.Customer;

internal record CreateCustomerRequest([Required] string FirstName, [Required] string LastName,
    [Required] string ContactEmail, [Required] string Phone, [Required] string Street, [Required] string City,
    [Required] string Residence, [Required] string Pesel);