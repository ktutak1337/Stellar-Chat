using StellarChat.Shared.Contracts.Storage;

namespace StellarChat.Server.Api.Features.Storage.UploadFile;

internal sealed record UploadFile(IFormFile File, string? Prefix = null, string? Directory = null) : ICommand<FileResponse>;
