namespace StellarChat.Server.Api.Options;

internal class QdrantOptions
{
    public const string Key = "qdrant";
    public string API_KEY { get; set; } = string.Empty;
    public string ENDPOINT { get; set; } = string.Empty;
}
