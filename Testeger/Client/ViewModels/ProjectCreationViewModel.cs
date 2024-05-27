using System.ComponentModel.DataAnnotations;

namespace Testeger.Client.ViewModels;

public class ProjectCreationViewModel
{
    [Required(ErrorMessage = "Project Name is required.")]
    [StringLength(50, MinimumLength = 5, ErrorMessage = "Project Name must be between 5 and 50 characters.")]
    public string? ProjectName { get; set; }

    public List<UserViewModel> Members { get; set; } = new List<UserViewModel>();
}
