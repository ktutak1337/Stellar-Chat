using Microsoft.AspNetCore.StaticFiles;
using StellarChat.Shared.Contracts.Storage;

namespace StellarChat.Server.Api.Features.Storage.DownloadFile;

internal class DownloadFileHandler : ICommandHandler<DownloadFile, DownloadFileResponse>
{
    public async ValueTask<DownloadFileResponse> Handle(DownloadFile command, CancellationToken cancellationToken)
    {
        string targetDirectory = Path.Combine(Directory.GetCurrentDirectory(), "_data");
        var directories = Directory.GetDirectories(targetDirectory, "*", SearchOption.AllDirectories);

        foreach (var dir in directories)
        {
            var files = Directory.GetFiles(dir);
            var file = files
                .SingleOrDefault(f => Path.GetFileNameWithoutExtension(f)
                .Equals(command.FileId, StringComparison.OrdinalIgnoreCase));

            if (file is not null)
            {
                var memory = new MemoryStream();
                using var stream = new FileStream(file, FileMode.Open, FileAccess.Read);
                await stream.CopyToAsync(memory);

                memory.Position = 0;
                var contentType = GetContentType(file);

                return new DownloadFileResponse(memory, contentType, Path.GetFileName(file));
            }
        }

        return new DownloadFileResponse(null, string.Empty, string.Empty);
    }

    private string GetContentType(string path)
    {
        var provider = new FileExtensionContentTypeProvider();
        
        if (!provider.TryGetContentType(path, out var contentType))
        {
            contentType = "application/octet-stream";
        }

        return contentType;
    }
}
