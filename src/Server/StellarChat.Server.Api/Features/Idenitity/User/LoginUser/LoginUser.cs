namespace StellarChat.Server.Api.Features.Idenitity.User.LoginUser;

public class LoginUser : ICommand<LoginUserResponse>
{

    [Required, EmailAddress]
    public string Email { get; set; } = default!;
    [Required, DataType(DataType.Password)]
    public string Password { get; set; } = default!;

}
