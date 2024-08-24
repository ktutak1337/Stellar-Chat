namespace StellarChat.Shared.Contracts.Identity.User.GetProfile;

public class GetProfileResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public string? UserName { get; set; } 
    public string? Email { get; set; } 
    public string? FirstName { get; set; } 
    public string? LastName { get; set; } 
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
    public bool IsPhoneNumberConfirmed { get; set; }
    public string? Role { get; set; } 
}
