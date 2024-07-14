namespace StellarChat.Server.Api.DAL.Mongo.Documents.Settings;

public class IntegrationDocument
{
    public string Name { get; set; } = string.Empty;
    public string Endpoint { get; set; } = string.Empty;
    public string ApiKey { get; set; } = string.Empty;
    public bool IsEnabled { get; set; }
}
