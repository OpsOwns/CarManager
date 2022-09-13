namespace CarManager.Application.Car.Commands.UploadImageCar;

public sealed record UploadImageCarCommand(CarId CarId, byte[] BlobBytes) : ICommand;