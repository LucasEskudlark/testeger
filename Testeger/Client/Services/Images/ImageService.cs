using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http;

namespace Testeger.Client.Services.Images;

public class ImageService : BaseService, IImageService
{
    private readonly string BaseAddress = "api/images";

    public ImageService(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<bool> UploadImageAsync(IEnumerable<IBrowserFile> files, string testCaseResultId)
    {
        using var content = new MultipartFormDataContent();

        foreach (var file in files)
        {
            var fileContent = new StreamContent(file.OpenReadStream(maxAllowedSize: 10_000_000));
            content.Add(content: fileContent, name: "files", fileName: file.Name);
        }

        var address = BaseAddress + $"/upload/{testCaseResultId}";
        var response = await _httpClient.PostAsync(address, content);

        if (!response.IsSuccessStatusCode)
        {
            return false;
        }

        return true;
    }
}
