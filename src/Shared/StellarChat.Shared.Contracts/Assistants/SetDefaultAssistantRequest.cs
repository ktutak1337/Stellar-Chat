using System.ComponentModel.DataAnnotations;

namespace StellarChat.Shared.Contracts.Assistants;

public record SetDefaultAssistantRequest([Required] Guid Id, bool IsDefault);
