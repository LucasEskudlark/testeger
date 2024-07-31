namespace Testeger.Domain.Models.Entities;

public class Image
{
    public int Id { get; set; }
    public string? TestCaseId { get; set; }
    public byte[]? ImageData { get; set; }
    public string? FileName { get; set; }

}
