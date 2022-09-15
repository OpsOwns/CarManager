namespace CarManager.API.Requests.Car;

internal sealed record UploadImageCarRequest([Required] IFormFile File);