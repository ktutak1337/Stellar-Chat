namespace StellarChat.Server.Api.Options;

internal class OllamaOptions
{
    public const string Key = "ollama";

    public bool ENABLED { get; set; }
    public string ENDPOINT { get; set; } = string.Empty;
    public string EMBEDDING_MODEL { get; set; } = string.Empty;
}
