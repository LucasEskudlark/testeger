using System.ComponentModel.DataAnnotations;

namespace Testeger.Client.ViewModels;

public class TestCaseCreationViewModel
{
    [Required(ErrorMessage = "Title is required")]
    [MinLength(5, ErrorMessage = "Title must be at least 5 characters")]
    [MaxLength(50, ErrorMessage = "Title must be at 50 characters at most")]
    public string? Title { get; set; }

    [Required(ErrorMessage = "Description is required")]
    [MinLength(20, ErrorMessage = "Description must be at least 20 characters")]
    [MaxLength(1500, ErrorMessage = "Description must be 1500 characters at most")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Pre Conditions are required")]
    [MinLength(20, ErrorMessage = "Pre Conditions must be at least 20 characters")]
    [MaxLength(700, ErrorMessage = "Pre Conditions must be 700 characters at most")]
    public string? PreConditions { get; set; }

    [Required(ErrorMessage = "Steps are required")]
    [MinLength(20, ErrorMessage = "Steps must be at least 20 characters")]
    [MaxLength(700, ErrorMessage = "Steps must be 700 characters at most")]
    public string? Steps { get; set; }

    [Required(ErrorMessage = "Expected Result is required")]
    [MinLength(20, ErrorMessage = "Expected Result must be at least 20 characters")]
    [MaxLength(700, ErrorMessage = "Expected Result must be 700 characters at most")]
    public string? ExpectedResult { get; set; }

    [Required(ErrorMessage = "Environment are required")]
    [MinLength(5, ErrorMessage = "Environment must be at least 5 characters")]
    [MaxLength(50, ErrorMessage = "Environment must be 50 characters at most")]
    public string? Environment { get; set; }
}
