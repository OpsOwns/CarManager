namespace CarManager.Application.Car.Commands.RegisterCar;

public sealed record RegisterCarCommand(string Engine, string Make, string Model,
    string Generation, int ProductionYear, string FuelType,
    string Power, string BodyType, byte[]? ImageBytes) : ICommand;