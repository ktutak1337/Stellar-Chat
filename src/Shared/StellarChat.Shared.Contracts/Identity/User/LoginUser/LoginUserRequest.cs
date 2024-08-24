using System.ComponentModel.DataAnnotations;

namespace StellarChat.Shared.Contracts.Identity.User.LoginUser;

public class LoginUserRequest
{ 

    [Required, EmailAddress]
    public string Email { get; set; } = default!;
    [Required, DataType(DataType.Password)]
    public string Password { get; set; } = default!; 

}
