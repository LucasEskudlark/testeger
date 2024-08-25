namespace Testeger.Client.ViewModels.Images;

public class ImageViewModel
{
    public required string Id { get; set; }
    public required string TestCaseResultId { get; set; }
    public required string FilePath { get; set; }
    public required string FileName { get; set; }
}
