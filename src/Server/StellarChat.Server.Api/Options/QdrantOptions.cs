namespace StellarChat.Server.Api.Options;

internal class QdrantOptions
{
    public const string Key = "qdrant";
    public string ApiKey { get; set; } = string.Empty;
    public string Endpoint { get; set; } = string.Empty;
}
