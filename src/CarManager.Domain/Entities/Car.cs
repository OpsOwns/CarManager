namespace CarManager.Domain.Entities;

//TODO implement Vehicle
// Engine : 1.6 Make : Ford Model : foucs generation : 1 fuel type : gas power: 110 kw body type : 5 doors year : 2001
public class Car : Entity<CarId>
{
    public string Engine { get; private set; } = default!;
    public Brand Brand { get; private set; } = default!;
    public FuelType FuelType { get; private set; } = default!;
    public UrlLink ImageLink { get; private set; } = default!;
    public string Power { get; private set; } = default!;
    public string BodyType { get; private set; } = default!;
    
    private Car() { }

    public Car(string engine, string power, string bodyType, Brand brand, FuelType fuelType, UrlLink imageLink) :
        base(new CarId())
    {
        Engine = engine;
        Power = power;
        BodyType = bodyType;
        Brand = brand;
        FuelType = fuelType;
        ImageLink = imageLink;
    }
}