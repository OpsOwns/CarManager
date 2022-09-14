namespace CarManager.API.Core.Converters;

public static class FormFileConverter
{
    public static async Task<byte[]> Convert(this IFormFile formFile,
        CancellationToken cancellationToken = default)
    {
        using var ms = new MemoryStream();
        await formFile.CopyToAsync(ms, cancellationToken);

        return ms.ToArray();
    }
}