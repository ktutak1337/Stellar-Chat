namespace StellarChat.Shared.Infrastructure.Semantic;

internal class QdrantOptions
{
    public const string Key = "qdrant";
    public string ApiKey { get; set; } = string.Empty;
    public string Endpoint { get; set; } = string.Empty;
}
