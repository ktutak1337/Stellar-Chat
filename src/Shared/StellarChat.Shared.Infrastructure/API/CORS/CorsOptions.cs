namespace StellarChat.Shared.Infrastructure.API.CORS;

public sealed class CorsOptions
{
    public bool ENABLED { get; set; }
    public bool ALLOW_CREDENTIALS { get; set; }
    public IEnumerable<string>? ALLOWED_ORIGINS { get; set; }
    public IEnumerable<string>? ALLOWED_METHODS { get; set; }
    public IEnumerable<string>? ALLOWED_HEADERS { get; set; }
    public IEnumerable<string>? EXPOSED_HEADERS { get; set; }
}
