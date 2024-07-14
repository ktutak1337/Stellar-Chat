namespace StellarChat.Server.Api.Domain.Settings.Models;

public class Integration
{
    public string Name { get; set; }
    public string Endpoint { get; set; }
    public string ApiKey { get; set; }
    public bool IsEnabled { get; set; }

    public Integration(string name, string endpoint, string apiKey, bool isEnabled)
    {
        Name = name;
        Endpoint = endpoint;
        ApiKey = apiKey;
        IsEnabled = isEnabled;
    }
}
