using Microsoft.AspNetCore.Components.Forms;
using StellarChat.Shared.Contracts.Storage;

namespace StellarChat.Client.Web.Services.Storage;

public interface IStorageService
{
    ValueTask<FileResponse> UploadFileAsync(IBrowserFile file, string? prefix = null, string? directory = null);
    ValueTask<Stream> DownloadFileAsync(string fileId);
}
