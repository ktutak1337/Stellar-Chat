namespace StellarChat.Shared.Contracts.Identity.User.RegistrationUser;

public sealed record RegistrationUserResponse(bool Success, string Message, string? Username, string? Email);
 