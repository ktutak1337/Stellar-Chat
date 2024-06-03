using Microsoft.AspNetCore.Http;

namespace StellarChat.Shared.Contracts.Storage;

public sealed record UploadFileRequest(IFormFile File, string? Prefix = null, string? Directory = null);
