namespace StellarChat.Shared.Contracts.Identity.User.LoginUser;

public sealed record LoginUserResponse(bool Success, string Message, string? Token);
 