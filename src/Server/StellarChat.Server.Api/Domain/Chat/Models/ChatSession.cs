namespace StellarChat.Server.Api.Domain.Chat.Models;

internal class ChatSession
{
    public Guid Id { get; set; }
    public Guid AssistantId { get; set; }
    public string Title { get; set; }
    public string Metaprompt { get; set; }
    public HashSet<string> ActivePlugins { get; set; } = new();
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }

    public ChatSession(Guid id, Guid assistantId, string title, string metaprompt, HashSet<string> activePlugins, DateTimeOffset createdAt, DateTimeOffset updatedAt)
    {
        Id = id;
        AssistantId = assistantId;
        Title = title;
        Metaprompt = metaprompt;
        ActivePlugins = activePlugins;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static ChatSession Create(Guid id, Guid assistantId, string title, string metaprompt, HashSet<string> activePlugins, DateTimeOffset createdAt, DateTimeOffset updatedAt)
        => new(id, assistantId, title, metaprompt, activePlugins, createdAt, updatedAt);
}
