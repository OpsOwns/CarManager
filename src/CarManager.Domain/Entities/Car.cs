using CarManager.Domain.Core;

namespace CarManager.Domain.Entities;

//TODO implement Vehicle
// Engine : 1.6 Make : Ford Model : foucs generation : 1 fuel type : gas power: 110 kw body type : 5 doors year : 2001
public sealed class Car : Entity<CarId>
{
    public string Engine { get; private set; }
    public Brand Brand { get; private set; }
    public FuelType FuelType { get; private set; }
    public UrlLink? ImageLink { get; private set; }
    public string Power { get; private set; }
    public string BodyType { get; private set; }

    private Car()
    {
        Engine = default!;
        Brand = default!;
        FuelType = default!;
        ImageLink = default!;
        Power = default!;
        BodyType = default!;
    }

    public Car(string engine, string power, string bodyType, Brand brand, FuelType fuelType) :
        base(new CarId())
    {
        Engine = engine;
        Power = power;
        BodyType = bodyType;
        Brand = brand;
        FuelType = fuelType;
    }

    public void AddImageLink(UrlLink imageLink)
    {
        ImageLink = imageLink;
    }
}