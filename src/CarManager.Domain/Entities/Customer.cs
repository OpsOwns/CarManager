namespace CarManager.Domain.Entities;

//TODO implement Customer
public class Customer : Entity<CustomerId>
{
    public FirstName FirstName { get; private set; } = null!;
    public LastName LastName { get; private set; } = null!;
    public Email EmailContact { get; private set; } = null!;
    public Phone Phone { get; private set; } = null!;
    public Address Address { get; private set; } = null!;

    private readonly List<Car> _cars = new();
    public IReadOnlyList<Car> Cars => _cars;

    private Customer()
    {
    }

    public Customer(FirstName firstName, LastName lastName, Email email, Phone phone, Address address) : base(
        new CustomerId())
    {
        FirstName = firstName;
        LastName = lastName;
        EmailContact = email;
        Phone = phone;
        Address = address;
    }

    public void AddCar(Car car)
    {
        _cars.Add(car);
    }
}