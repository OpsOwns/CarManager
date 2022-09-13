namespace CarManager.Infrastructure.Blob;

internal sealed class AzureBlobStorageOptions
{
    public Uri Endpoint { get; }
    public string AccountName { get; }
    public string AccountKey { get; }

    private const string Section = "Storage";

    public AzureBlobStorageOptions(IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);

        var section = configuration.GetSection(Section);

        if (section is null)
        {
            throw new InvalidOperationException($"Cannot find {Section} from IConfiguration");
        }

        Endpoint = section.GetValue<Uri>(nameof(Endpoint));
        AccountKey = section.GetValue<string>(nameof(AccountKey));
        AccountName = section.GetValue<string>(nameof(AccountName));
    }
}