namespace StellarChat.Server.Api.Domain.Actions.Models;

internal class NativeAction
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public string Icon { get; set; }
    public string Model { get; set; }
    public string Metaprompt { get; set; }
    public bool IsSingleMessageMode { get; set; }
    public bool IsRemoteAction { get; set; }
    public bool ShouldRephraseResponse { get; set; }
    public Webhook? Webhook { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }

    public NativeAction(
        Guid id,
        string name,
        string category,
        string icon,
        string model,
        string metaprompt,
        bool isSingleMessageMode,
        bool isRemoteAction,
        bool shouldRephraseResponse,
        Webhook? webhook,
        DateTimeOffset createdAt,
        DateTimeOffset updatedAt)
    {
        Id = id;
        Name = name;
        Category = category;
        Icon = icon;
        Model = model;
        Metaprompt = metaprompt;
        IsSingleMessageMode = isSingleMessageMode;
        IsRemoteAction = isRemoteAction;
        ShouldRephraseResponse = shouldRephraseResponse;
        Webhook = webhook;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static NativeAction Create(
        Guid id,
        string name,
        string category,
        string icon,
        string model,
        string metaprompt,
        bool isSingleMessageMode,
        bool isRemoteAction,
        bool shouldRephraseResponse,
        Webhook? webhook,
        DateTimeOffset createdAt,
        DateTimeOffset updatedAt)
            => new(id, name, category, icon, model, metaprompt, isSingleMessageMode, isRemoteAction, shouldRephraseResponse, webhook, createdAt, updatedAt);
}
