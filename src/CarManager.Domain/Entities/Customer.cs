namespace CarManager.Domain.Entities;

public class Customer : Entity<CustomerId>, IAggregateRoot
{
    public FirstName FirstName { get; private set; } = default!;
    public LastName LastName { get; private set; } = default!;
    public Email EmailContact { get; private set; } = default!;
    public Phone Phone { get; private set; } = default!;
    public Address Address { get; private set; } = default!;

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

    public Result RegisterCar(Car car)
    {
        if (_cars.Any(x => x.Id == car.Id))
        {
            return Result.Failure<Result>(CustomErrors.Car.CarAlreadyRegistered(car.Id.Value));
        }

        _cars.Add(car);

        return Result.Success();
    }
}