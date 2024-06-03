using Microsoft.AspNetCore.Components.Forms;
using StellarChat.Shared.Contracts.Storage;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace StellarChat.Client.Web.Services.Storage;

public class StorageService(IHttpClientFactory httpClientFactory) : IStorageService
{
    private const string HttpClientName = "WebAPI";

    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

    public async ValueTask<FileResponse> UploadFileAsync(IBrowserFile file, string? prefix = null, string? directory = null)
    {
        var httpClient = _httpClientFactory.CreateClient(HttpClientName);

        var payload = CreateMultipartContent(file, prefix, directory);

        var response = await httpClient.PostAsync($"/files", payload);

        FileResponse? result = null;

        if(response.IsSuccessStatusCode)
        {
            result = await response.Content.ReadFromJsonAsync<FileResponse>();
        }

        return result!;
    }

    public ValueTask<Stream> DownloadFileAsync(string fileId)
    {
        throw new NotImplementedException();
    }

    private MultipartFormDataContent CreateMultipartContent(IBrowserFile file, string? prefix = null, string ? directory = null)
    {
        var content = new MultipartFormDataContent();
        var stream = file.OpenReadStream(maxAllowedSize: 5 * 1024 * 1024); // 5MB limit

        var fileContent = new StreamContent(stream);
        fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);

        content.Add(fileContent, nameof(UploadFileRequest.File), file.Name);
        content.Add(new StringContent(prefix ?? string.Empty), nameof(UploadFileRequest.Prefix));
        content.Add(new StringContent(directory ?? string.Empty), nameof(UploadFileRequest.Directory));

        return content;
    }

}
