using Microsoft.AspNetCore.Http;

namespace Testeger.Application.Services.Image;

public interface IImageService
{
    Task UploadTestCaseResultImagesAsync(IEnumerable<IFormFile> files, string testCaseResultId);
}
