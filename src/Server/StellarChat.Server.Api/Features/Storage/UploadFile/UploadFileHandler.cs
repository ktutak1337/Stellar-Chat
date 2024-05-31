using StellarChat.Shared.Contracts.Storage;

namespace StellarChat.Server.Api.Features.Storage.UploadFile;

internal sealed class UploadFileHandler : ICommandHandler<UploadFile, FileResponse>
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UploadFileHandler(IHttpContextAccessor httpContextAccessor) 
        => _httpContextAccessor = httpContextAccessor;

    public async ValueTask<FileResponse> Handle(UploadFile command, CancellationToken cancellationToken)
    {
        var (file, directory, prefix) = command;

        var fileId = prefix!.IsEmpty() 
            ? $"_{Guid.NewGuid()}" 
            : $"_{prefix}_{Guid.NewGuid()}";

        var (filePath, fileName, writeToDirectory) = CreateFilePath(file, fileId, directory!);

        await SaveFileAsync(file, filePath);

        var formattedBytesSize = $"{file.Length:#,0} Bytes".Replace(",", " ");
        var type = file.ContentType.Split('/')[1];

        var fileMetadata = new FileMetadata(file.FileName, type, file.Name, file.ContentDisposition, formattedBytesSize, file.Length.ToFormatSize());
        var fileUrl = CreateFileUrl(writeToDirectory, fileName);

        return new FileResponse(fileId, fileUrl, fileMetadata);
    }


    private (string, string, string) CreateFilePath(IFormFile file, string fileId, string directory)
    {
        var (targetDirectory, writeToDirectory) = GetTargetDirectory(file, directory);
        var fileExtension = Path.GetExtension(file.FileName);
        var fileName = $"{fileId}{fileExtension}";
        var filePath = Path.Combine(targetDirectory, fileName);

        return (filePath, fileName, writeToDirectory);
    }

    private (string, string) GetTargetDirectory(IFormFile? file, string? directory)
    {
        var targetDirectory = string.Empty;
        var writeToDirectory = string.Empty;

        if (file is not null)
        {
            writeToDirectory = file.ContentType switch
            {
                var contentType when contentType.StartsWith("image/") => "images",
                var contentType when contentType.StartsWith("audio/") => "audio",
                var contentType when contentType.StartsWith("video/") => "videos",
                var contentType when contentType.StartsWith("application/") => "documents",
                var contentType when contentType.StartsWith("text/") => "documents",
                _ => directory
            };

            targetDirectory = Path.Combine(Directory.GetCurrentDirectory(), "_data", writeToDirectory!);

            if (!Directory.Exists(targetDirectory))
            {
                Directory.CreateDirectory(targetDirectory);
            }
        }

        return (targetDirectory, writeToDirectory!);
    }

    private async Task SaveFileAsync(IFormFile? file, string filePath, CancellationToken cancellationToken = default)
    {
        if(file is not null)
        {
            using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream, cancellationToken);
        }
    }

    private string CreateFileUrl(string directory, string fileName)
    {
        var request = _httpContextAccessor?.HttpContext?.Request;
        var fileUrl = $"{request?.Scheme}://{request?.Host}/files/{directory}/{fileName}";

        return fileUrl;
    }
}
