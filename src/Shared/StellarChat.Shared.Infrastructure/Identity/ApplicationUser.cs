using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace StellarChat.Shared.Infrastructure.Identity;

[CollectionName("users")]
public class ApplicationUser : MongoIdentityUser<Guid>
{ 
    public string? FirstName { get; set; }
    public string? LastName { get; set; } 
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }
    public DateTime RegistredOn { get; set; }
    public DateTime ModifiedOn { get; set; }
}

