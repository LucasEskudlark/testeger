using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Testeger.Application.Services.Image;

public interface IImageService
{
    Task UploadTestCaseResultImagesAsync(IEnumerable<IFormFile> files, string testCaseResultId);
    Task<IEnumerable<string>> GetTestCaseResultImagesFilePathsAsync(string testCaseResultId);
    Task<(Stream ImageStream, string ContentType)> GetImageAsync(string imagePath);
}
