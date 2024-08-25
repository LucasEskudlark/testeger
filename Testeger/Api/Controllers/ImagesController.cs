using Microsoft.AspNetCore.Mvc;
using Testeger.Application.Services.Image;

namespace Testeger.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ImagesController : ControllerBase
{
    private readonly IImageService _imageService;

    public ImagesController(IImageService imageService)
    {
        _imageService = imageService;
    }

    [HttpPost("upload/{testCaseResultId}")]
    public async Task<IActionResult> UploadImage([FromForm] IEnumerable<IFormFile> files, [FromRoute] string testCaseResultId)
    {
        await _imageService.UploadTestCaseResultImagesAsync(files, testCaseResultId);

        return NoContent();
    }

    [HttpGet("result/{testCaseResultId}")]
    public async Task<IActionResult> GetTestCaseImagesFilePaths(string testCaseResultId)
    {
        var response = await _imageService.GetTestCaseResultImagesFilePathsAsync(testCaseResultId);

        return Ok(response);
    }

    [HttpGet("{*imagePath}")]
    public async Task<IActionResult> GetImageAsync(string imagePath)
    {
        var (imageStream, contentType) = await _imageService.GetImageAsync(imagePath);

        if (imageStream == null)
        {
            return NotFound();
        }

        return File(imageStream, contentType);
    }
}

