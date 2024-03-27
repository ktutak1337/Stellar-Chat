namespace StellarChat.Server.Api.Domain.Chat.Models;

public class ChatSession
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Metaprompt { get; set; }
    public HashSet<string> ActivePlugins { get; set; } = new();
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }

    public ChatSession(Guid id, string title, string metaprompt, HashSet<string> activePlugins, DateTimeOffset createdAt, DateTimeOffset updatedAt)
    {
        Id = id;
        Title = title;
        Metaprompt = metaprompt;
        ActivePlugins = activePlugins;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static ChatSession Create(Guid id, string title, string metaprompt, HashSet<string> activePlugins, DateTimeOffset createdAt, DateTimeOffset updatedAt)
        => new(id, title, metaprompt, activePlugins, createdAt, updatedAt);
}
