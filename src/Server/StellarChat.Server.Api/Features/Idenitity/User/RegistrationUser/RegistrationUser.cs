namespace StellarChat.Server.Api.Features.Idenitity.User.RegistrationUser;

public class RegistrationUser : ICommand<RegistrationUserResponse>
{
    public string Username { get; set; } = default!;

    [Required, EmailAddress]
    public string Email { get; set; } = default!;
    [Required, DataType(DataType.Password)]
    public string Password { get; set; } = default!;
    [Required, DataType(DataType.Password), Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
    public string ConfirmPassword { get; set; } = default!;

}
