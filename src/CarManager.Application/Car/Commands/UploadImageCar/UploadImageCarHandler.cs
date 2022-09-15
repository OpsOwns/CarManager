namespace CarManager.Application.Car.Commands.UploadImageCar;

internal sealed class UploadImageCarHandler : ICommandHandler<UploadImageCarCommand>
{
    private readonly IFileStorage _fileStorage;
    private readonly ICarRepository _carRepository;

    public UploadImageCarHandler(IFileStorage fileStorage, ICarRepository carRepository)
    {
        _fileStorage = fileStorage;
        _carRepository = carRepository;
    }

    public async ValueTask<Result> HandleAsync(UploadImageCarCommand command,
        CancellationToken cancellationToken = default)
    {
        var car = await _carRepository.GetByIdAsync(command.CarId, cancellationToken);

        if (car is null)
        {
            return Result.Failure(CustomErrors.General.NotFound(command.CarId));
        }

        var fileUrl = await _fileStorage.Upload(command.BlobBytes, "cars", cancellationToken);
        var urlLinkResult = UrlLink.Create(fileUrl);

        if (urlLinkResult.IsFailure)
        {
            return urlLinkResult;
        }

        car.AddImageLink(urlLinkResult.Value);

        await _carRepository.UpdateAsync(car);

        return Result.Success();
    }
}