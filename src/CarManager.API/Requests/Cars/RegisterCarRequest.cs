namespace CarManager.API.Requests.Cars;

public sealed record RegisterCarRequest([Required] string Engine, [Required] string Make, [Required] string Model,
    [Required] string Generation, [Required] int ProductionYear, [Required] string FuelType,
    [Required] string Power, [Required] string BodyType, IFormFile? CarImage);