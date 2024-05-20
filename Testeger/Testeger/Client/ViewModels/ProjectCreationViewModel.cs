using System.ComponentModel.DataAnnotations;

namespace Testeger.Client.ViewModels;

public class ProjectCreationViewModel
{
    [Required(ErrorMessage = "Project Name is required.")]
    [StringLength(100, MinimumLength = 5, ErrorMessage = "Project Name must be between 5 and 100 characters.")]
    public string? ProjectName { get; set; }

    //[Required(ErrorMessage = "At least one user email is required.")]
    //[EmailAddress(ErrorMessage = "Invalid email address format.")]
    //public List<string> UserEmails { get; set; } = new List<string>();
}
