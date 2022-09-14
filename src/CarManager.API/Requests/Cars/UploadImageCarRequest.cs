namespace CarManager.API.Requests.Cars;

public sealed record UploadImageCarRequest([Required] IFormFile File);