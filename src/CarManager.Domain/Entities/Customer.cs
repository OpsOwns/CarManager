namespace CarManager.Domain.Entities;

public sealed class Customer : Entity<CustomerId>, IAggregateRoot
{
    public FirstName FirstName { get; private set; }
    public LastName LastName { get; private set; }
    public Email EmailContact { get; private set; }
    public Phone Phone { get; private set; }
    public Address Address { get; private set; }
    public Pesel Pesel { get; private set; }

    private readonly List<Car> _cars = new();
    public IReadOnlyList<Car> Cars => _cars;

    private Customer()
    {
        FirstName = default!;
        LastName = default!;
        EmailContact = default!;
        Phone = default!;
        Address = default!;
        Pesel = default!;
    }

    public Customer(FirstName firstName, LastName lastName, Email email, Phone phone, Address address,
        Pesel pesel) : base(new CustomerId())
    {
        FirstName = firstName;
        LastName = lastName;
        EmailContact = email;
        Phone = phone;
        Address = address;
        Pesel = pesel;
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