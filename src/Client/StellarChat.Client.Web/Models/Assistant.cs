using System.ComponentModel.DataAnnotations;

namespace StellarChat.Client.Web.Models;

public class Assistant
{
    public Guid Id { get; set; }
    
    [Required]
    [MinLength(2, ErrorMessage = "Name must be at least 2 characters long.")]
    public string Name { get; set; } = string.Empty;
    
    [Required] 
    public string Metaprompt { get; set; } = string.Empty;
    
    [Required]
    [MinLength(5, ErrorMessage = "Name must be at least 5 characters long.")]
    public string Description { get; set; } = string.Empty;
    
    [Required]
    public string AvatarUrl { get; set; } = string.Empty;

    [Required] 
    public string DefaultModel { get; set; } = string.Empty;

    public string DefaultVoice { get; set; } = string.Empty;
    public bool IsDefault { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}
