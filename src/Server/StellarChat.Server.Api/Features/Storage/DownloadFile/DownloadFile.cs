using StellarChat.Shared.Contracts.Storage;

namespace StellarChat.Server.Api.Features.Storage.DownloadFile;

internal sealed record DownloadFile(string FileId) : ICommand<DownloadFileResponse>;