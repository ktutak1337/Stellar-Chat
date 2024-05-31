namespace StellarChat.Shared.Contracts.Storage;

public sealed record DownloadFileResponse(MemoryStream? Stream, string ContentType, string FileName);
