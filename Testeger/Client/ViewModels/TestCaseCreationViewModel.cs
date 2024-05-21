using System.ComponentModel.DataAnnotations;

namespace Testeger.Client.ViewModels;

public class TestCaseCreationViewModel
{
    [Required(ErrorMessage = "Title is required")]
    [MinLength(5, ErrorMessage = "Title must be at least 5 characters")]
    [MaxLength(30, ErrorMessage = "Title must be at 30 characters at most")]
    public string? Title { get; set; }

    [Required(ErrorMessage = "Description is required")]
    [MinLength(5, ErrorMessage = "Title must be at least 5 characters")]
    [MaxLength(1500, ErrorMessage = "Title must be 1500 characters at most")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Pre Conditions are required")]
    [MinLength(5, ErrorMessage = "Pre Conditions must be at least 5 characters")]
    [MaxLength(700, ErrorMessage = "Pre Conditions must be 700 characters at most")]
    public string? PreConditions { get; set; }

    [Required(ErrorMessage = "Steps are required")]
    [MinLength(20, ErrorMessage = "Steps must be at least 20 characters")]
    [MaxLength(700, ErrorMessage = "Steps must be 700 characters at most")]
    public string? Steps { get; set; }
}
