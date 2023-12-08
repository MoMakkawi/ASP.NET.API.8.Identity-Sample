using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Identity;

namespace Demo.API.Identity;

public class ApplicationUser : IdentityUser
{
    public bool IsActive { get; set; } = true;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    [Required]
    public string FullName { get; set; }
    [Required]
    public string LocationText { get; set; }
    [Required]
    public string Role { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
