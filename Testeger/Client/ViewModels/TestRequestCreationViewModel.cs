using System.ComponentModel.DataAnnotations;
using Testeger.Shared.DTOs.Enumerations;

namespace Testeger.Client.ViewModels;

public class TestRequestCreationViewModel
{
    public string? ProjectId { get; set; }

    [Required(ErrorMessage = "Title is required")]
    [MinLength(5, ErrorMessage = "Title must be at least 5 characters")]
    [MaxLength(50, ErrorMessage = "Title must be at 50 characters at most")]
    public string? Title { get; set; }

    [Required(ErrorMessage = "Description is required")]
    [MinLength(5, ErrorMessage = "Title must be at least 5 characters")]
    [MaxLength(1500, ErrorMessage = "Title must be at 1500 characters at most")]
    public string? Description { get; set; }

    [Url(ErrorMessage = "Story Link must be a valid URL")]
    public string? StoryLink { get; set; }

    [Required(ErrorMessage = "Priority Level is required")]
    [EnumDataType(typeof(PriorityLevel))]
    public PriorityLevel PriorityLevel { get; set; }

    [Required(ErrorMessage = "Due Date is required")]
    public DateTime? DueDate { get; set; }
}
