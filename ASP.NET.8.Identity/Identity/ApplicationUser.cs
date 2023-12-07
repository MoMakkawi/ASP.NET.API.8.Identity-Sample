﻿using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Identity;

namespace Demo.API.Identity;

public class ApplicationUser : IdentityUser
{
    [Required]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public string LocationText { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public bool IsActive { get; set; } = true;

}