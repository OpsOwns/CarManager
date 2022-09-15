namespace CarManager.Domain.ValueObjects;

public sealed class Address : ValueObject
{
    public string Street { get; }
    public string City { get; }
    public string Residence { get; }


    private Address(string street, string city, string residence)
    {
        Street = street;
        City = city;
        Residence = residence;
    }

    public static Result<Address> Create(string street, string city, string residence)
    {
        if (string.IsNullOrEmpty(street))
        {
            return Result.Failure<Address>(CustomErrors.General.ValueIsRequired());
        }

        if (string.IsNullOrEmpty(city))
        {
            return Result.Failure<Address>(CustomErrors.General.ValueIsRequired());
        }

        if (string.IsNullOrEmpty(residence))
        {
            return Result.Failure<Address>(CustomErrors.General.ValueIsRequired());
        }

        return new Address(street, city, residence);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Street;
        yield return City;
        yield return Residence;
    }
}