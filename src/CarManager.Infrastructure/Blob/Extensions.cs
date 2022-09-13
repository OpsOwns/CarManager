namespace CarManager.Infrastructure.Blob;

internal static class Extensions
{
    public static IServiceCollection AddAzureStorage(this IServiceCollection services)
    {
        services.AddSingleton(x => new AzureBlobStorageOptions(x.GetRequiredService<IConfiguration>()));

        services.TryAddSingleton(x =>
        {
            var options = x.GetRequiredService<AzureBlobStorageOptions>();

            if (string.IsNullOrEmpty(options.AccountKey))
            {
                var managedIdentity = new ManagedIdentityCredential();
                return new BlobServiceClient(options.Endpoint, managedIdentity);
            }

            var sharedKey = new StorageSharedKeyCredential(options.AccountName, options.AccountKey);
            return new BlobServiceClient(options.Endpoint, sharedKey);
        });

        services.AddScoped<IFileStorage, AzureBlobStorage>();

        return services;
    }
}