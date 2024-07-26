using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StellarChat.Shared.Contracts.Actions;

public sealed record ExecuteNativeActionRequest([property: JsonIgnore][Required] Guid Id, [Required] Guid ChatId, string ServiceId, string Message);
