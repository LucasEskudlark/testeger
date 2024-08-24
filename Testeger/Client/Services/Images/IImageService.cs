using Microsoft.AspNetCore.Components.Forms;

namespace Testeger.Client.Services.Images;

public interface IImageService
{
    Task<bool> UploadImageAsync(IEnumerable<IBrowserFile> files, string testCaseResultId);
}
