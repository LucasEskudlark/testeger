using System.ComponentModel.DataAnnotations;
using Testeger.Shared.DTOs.Enumerations;

namespace Testeger.Client.ViewModels.Users;

public class UserInvitationViewModel
{
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Email needs to be valid.")]
    public string? Email { get; set; }
    [Required(ErrorMessage = "Role is required.")]
    public RoleType? RoleType { get; set; }
}
