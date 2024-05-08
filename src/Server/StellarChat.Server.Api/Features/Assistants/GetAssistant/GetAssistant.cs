namespace StellarChat.Server.Api.Features.Assistants.GetAssistant;

internal sealed record GetAssistant : IQuery<AssistantResponse>
{
    public Guid Id { get; set; }
}