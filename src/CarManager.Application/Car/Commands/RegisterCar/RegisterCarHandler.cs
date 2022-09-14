using CarManager.Application.Abstractions.Cqrs.Commands;

namespace CarManager.Application.Car.Commands.RegisterCar;

internal sealed class RegisterCarHandler : ICommandHandler<RegisterCarCommand>
{
    private readonly ICarRepository _carRepository;

    public RegisterCarHandler(ICarRepository carRepository)
    {
        _carRepository = carRepository;
    }

    public async ValueTask<Result> HandleAsync(RegisterCarCommand command,
        CancellationToken cancellationToken = default)
    {
        var brandResult = Brand.Create(command.Make, command.Model, command.Generation, command.ProductionYear);
        var fuelTypeResult = FuelType.GetValueByName(command.FuelType);
        var combinedResult = Result.Combine(brandResult, fuelTypeResult);

        if (combinedResult.IsFailure)
        {
            return combinedResult;
        }

        var car = new Domain.Entities.Car(command.Engine, command.Power, command.BodyType, brandResult.Value,
            fuelTypeResult.Value);

        await _carRepository.AddAsync(car, cancellationToken);

        return Result.Success();
    }
}