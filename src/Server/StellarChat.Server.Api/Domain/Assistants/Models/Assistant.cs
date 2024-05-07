namespace StellarChat.Server.Api.Domain.Assistants.Models;

internal class Assistant
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Metaprompt { get; set; }
    public string Description { get; set; }
    public string AvatarUrl { get; set; }
    public string DefaultModel { get; set; }
    public string DefaultVoice { get; set; }
    public bool IsDefault { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }

    public Assistant(
        Guid id, 
        string name, 
        string metaprompt, 
        string description, 
        string avatarUrl, 
        string defaultModel, 
        string defaultVoice, 
        bool isDefault, 
        DateTimeOffset createdAt, 
        DateTimeOffset updatedAt)
    {
        Id = id;
        Name = name;
        Metaprompt = metaprompt;
        Description = description;
        AvatarUrl = avatarUrl;
        DefaultModel = defaultModel;
        DefaultVoice = defaultVoice;
        IsDefault = isDefault;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static Assistant Create(
        Guid id, 
        string name, 
        string metaprompt, 
        string description, 
        string avatarUrl, 
        string defaultModel, 
        string defaultVoice, 
        bool isDefault, 
        DateTimeOffset createdAt, 
        DateTimeOffset updatedAt)
            => new(id, name, metaprompt, description, avatarUrl, defaultModel, defaultVoice, isDefault, createdAt, updatedAt);
}
