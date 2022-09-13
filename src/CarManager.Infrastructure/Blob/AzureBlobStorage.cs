namespace CarManager.Infrastructure.Blob;

internal sealed class AzureBlobStorage : IFileStorage
{
    private readonly BlobServiceClient _blobServiceClient;

    public AzureBlobStorage(BlobServiceClient blobServiceClient)
    {
        _blobServiceClient = blobServiceClient;
    }

    public async Task<string> Upload(byte[] file, string path, CancellationToken cancellationToken)
    {
        if (file is null || file.Length == 0)
        {
            throw new InvalidOperationException();
        }

        if (string.IsNullOrEmpty(path))
        {
            throw new ArgumentNullException(nameof(path), "Missing file path");
        }

        var containerClient = _blobServiceClient.GetBlobContainerClient(path);
        var blobClient = containerClient.GetBlobClient($"{path}/{Guid.NewGuid()}");

        await using MemoryStream memoryStream = new(file);
        await blobClient.UploadAsync(memoryStream, true, cancellationToken);

        var absolutePath = blobClient.Uri.AbsolutePath;

        return absolutePath;
    }

    public async Task<byte[]> Download(string path, CancellationToken cancellationToken)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(path.Split("/")[0]);
        var blobClient = containerClient.GetBlobClient(path);

        var blob = await blobClient.DownloadAsync(cancellationToken);

        await using MemoryStream memoryStream = new();
        await blob.Value.Content.CopyToAsync(memoryStream, cancellationToken);

        return memoryStream.ToArray();
    }
}