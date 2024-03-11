namespace StellarChat.Shared.Infrastructure.Observability.Logging;

public sealed class SerilogOptions
{
    public string Level { get; set; } = string.Empty;
    public ConsoleOptions Console { get; set; } = new();
    public FileOptions File { get; set; } = new();
    public SeqOptions Seq { get; set; } = new();
    public IEnumerable<string>? ExcludePaths { get; set; }
    public IEnumerable<string>? ExcludeProperties { get; set; }
    public Dictionary<string, string> Overrides { get; set; } = new();
    public Dictionary<string, object> Tags { get; set; } = new();

    public sealed class ConsoleOptions
    {
        public bool Enabled { get; set; }
    }

    public sealed class FileOptions
    {
        public bool Enabled { get; set; }
        public string Path { get; set; } = string.Empty;
        public string Interval { get; set; } = string.Empty;
    }

    public sealed class SeqOptions
    {
        public bool Enabled { get; set; }
        public string Url { get; set; } = string.Empty;
        public string ApiKey { get; set; } = string.Empty;
    }
}
