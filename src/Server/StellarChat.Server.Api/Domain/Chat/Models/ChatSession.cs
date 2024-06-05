namespace StellarChat.Server.Api.Domain.Chat.Models;

internal class ChatSession
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Metaprompt { get; set; }
    public HashSet<string> ActivePlugins { get; set; } = new();
    public string AvatarUrl { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }

    public ChatSession(Guid id, string title, string metaprompt, HashSet<string> activePlugins, string avatarUrl, DateTimeOffset createdAt, DateTimeOffset updatedAt)
    {
        Id = id;
        Title = title;
        Metaprompt = metaprompt;
        ActivePlugins = activePlugins;
        AvatarUrl = avatarUrl;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static ChatSession Create(Guid id, string title, string metaprompt, HashSet<string> activePlugins, string avatarUrl, DateTimeOffset createdAt, DateTimeOffset updatedAt)
        => new(id, title, metaprompt, activePlugins, avatarUrl, createdAt, updatedAt);
}
