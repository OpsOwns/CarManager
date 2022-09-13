namespace CarManager.Shared.Infrastructure.Converters;

public static class FormFileConverter
{
    public static async Task<byte[]?> Convert(IFormFile? formFile,
        CancellationToken cancellationToken = default)
    {
        if (formFile is null)
        {
            return null;
        }

        using var ms = new MemoryStream();
        await formFile.CopyToAsync(ms, cancellationToken);

        return ms.ToArray();
    }
}