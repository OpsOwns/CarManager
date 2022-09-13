namespace CarManager.Application.Abstractions;

public interface IFileStorage
{
    Task<string> Upload(byte[] file, string path, CancellationToken cancellationToken);
    Task<byte[]> Download(string path, CancellationToken cancellationToken);
}