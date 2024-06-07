namespace StellarChat.Server.Api.Options;

internal class OpenAiOptions
{
    public const string Key = "openAI";
    public string TEXT_MODEL { get; set; } = string.Empty;
    public string API_KEY { get; set; } = string.Empty;
    public string EMBEDDING_MODEL { get; set; } = string.Empty;
}
