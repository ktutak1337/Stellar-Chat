using StellarChat.Shared.Contracts.Storage;

namespace StellarChat.Server.Api.Features.Storage.UploadFile;

internal sealed record UploadFile(IFormFile File, string? Directory = null, string? Prefix = null) : ICommand<FileResponse>;
