using System.ComponentModel.DataAnnotations;
using Testeger.Client.ViewModels.Users;

namespace Testeger.Client.ViewModels;

public class ProjectCreationViewModel
{
    [Required(ErrorMessage = "Project Name is required.")]
    [StringLength(50, MinimumLength = 5, ErrorMessage = "Project Name must be between 5 and 50 characters.")]
    public string? Name { get; set; }

    [ValidateComplexType]
    public List<UserInvitationViewModel>? Users { get; set; } = new();
}
