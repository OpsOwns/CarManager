namespace CarManager.API.Requests.Cars;

internal sealed record UploadImageCarRequest([Required] IFormFile File);