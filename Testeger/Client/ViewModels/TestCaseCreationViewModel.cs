using System.ComponentModel.DataAnnotations;
using Testeger.Client.ViewModels.TestCases;

namespace Testeger.Client.ViewModels;

public class TestCaseCreationViewModel
{
    [Required(ErrorMessage = "Title is required")]
    [MinLength(5, ErrorMessage = "Title must be at least 5 characters")]
    [MaxLength(50, ErrorMessage = "Title must be at 50 characters at most")]
    public string? Title { get; set; }
    public TestCaseDetailsViewModel Details { get; set; } = new();
    public string? TestRequestId { get; set; }
    public string? ProjectId { get; set; }
}
