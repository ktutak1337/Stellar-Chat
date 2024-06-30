namespace StellarChat.Client.Web.Shared.Http;

public interface IRestHttpClient
{
    Task<ApiResponse<T>> GetAsync<T>(string endpoint);
    Task<ApiResponse> PostAsync(string endpoint, object request);
    Task<ApiResponse<T>> PostAsync<T>(string endpoint, object request);
    Task<ApiResponse> PutAsync(string endpoint, object request);
    Task<ApiResponse> DeleteAsync(string endpoint);
}
