﻿using Microsoft.AspNetCore.Identity;

namespace Testeger.Domain.Models.Entities;

public class ApplicationUser : IdentityUser
{
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
    public DateTime BirthDate { get; set; }

    public ICollection<ProjectUser>? ProjectUsers { get; set; }
    public ICollection<Invitation>? Invitations { get; set; }
}
