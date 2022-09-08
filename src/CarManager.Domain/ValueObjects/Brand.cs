namespace CarManager.Domain.ValueObjects;

public class Brand : ValueObject
{
    public string Make { get; set; }
    public string Model { get; set; }
    public string Generation { get; set; }
    public int ProductionYear { get; set; }

    private Brand(string make, string model, string generation, int productionYear)
    {
        Make = make;
        Model = model;
        Generation = generation;
        ProductionYear = productionYear;
    }

    public static Result<Brand> Create(string make, string model, string generation, int productionYear)
    {
        if (string.IsNullOrEmpty(make))
        {
            return Result.Failure<Brand>(CustomErrors.General.ValueIsRequired());
        }

        if (string.IsNullOrEmpty(model))
        {
            return Result.Failure<Brand>(CustomErrors.General.ValueIsRequired());
        }

        if (string.IsNullOrEmpty(generation))
        {
            return Result.Failure<Brand>(CustomErrors.General.ValueIsRequired());
        }

        if (productionYear < 1910)
        {
            return Result.Failure<Brand>(CustomErrors.Car.ProductionYearIsTooLow());
        }


        return new Brand(make, model, generation, productionYear);
    }


    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Make;
        yield return Model;
        yield return Generation;
        yield return ProductionYear;
    }
}