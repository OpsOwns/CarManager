namespace CarManager.Domain.Enumeration;

public sealed class FuelType : Enumeration<FuelType>
{
    public static readonly FuelType None = new(1, "Gasoline");
    public static readonly FuelType Diesel = new(2, "Diesel");
    public static readonly FuelType Cng = new(3, "CNG");

    private FuelType(int value, string name)
        : base(value, name)
    {
    }

    private FuelType(int value)
        : base(value, FromValue(value)?.Name!)
    {
    }
}