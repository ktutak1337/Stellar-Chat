namespace StellarChat.Shared.Infrastructure.API.CORS;

public sealed class CorsOptions
{
    public bool Enabled { get; set; }
    public bool AllowCredentials { get; set; }
    public IEnumerable<string>? AllowedOrigins { get; set; }
    public IEnumerable<string>? AllowedMethods { get; set; }
    public IEnumerable<string>? AllowedHeaders { get; set; }
    public IEnumerable<string>? ExposedHeaders { get; set; }
}
