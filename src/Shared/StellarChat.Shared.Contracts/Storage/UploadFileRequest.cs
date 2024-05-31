using Microsoft.AspNetCore.Http;

namespace StellarChat.Shared.Contracts.Storage;

public sealed record UploadFileRequest(IFormFile File, string? Directory = null, string? Prefix = null);
