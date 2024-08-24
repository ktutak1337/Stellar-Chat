using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace StellarChat.Shared.Infrastructure.Identity;

[CollectionName("roles")]
public class ApplicationRole : MongoIdentityRole<Guid>
{

} 
