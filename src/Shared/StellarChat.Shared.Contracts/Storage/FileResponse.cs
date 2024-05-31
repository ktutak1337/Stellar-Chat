namespace StellarChat.Shared.Contracts.Storage;

public record FileResponse(string FileId, string Url, FileMetadata Metadata);

public record FileMetadata(string FileName, string Type, string Name, string ContentDisposition, string Length, string Size);
