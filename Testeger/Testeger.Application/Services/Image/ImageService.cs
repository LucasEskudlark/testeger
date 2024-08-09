using AutoMapper;
using Microsoft.AspNetCore.Http;
using Testeger.Application.Exceptions;
using Testeger.Infra.UnitOfWork;
using DomainImage = Testeger.Domain.Models.Entities.Image;

namespace Testeger.Application.Services.Image;

public class ImageService : BaseService, IImageService
{
    private readonly string _targetFilePath;
    public ImageService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
        var currentDirectory = Directory.GetCurrentDirectory();
        var parentDirectory = Directory.GetParent(currentDirectory)?.FullName ?? currentDirectory;

        _targetFilePath = Path.Combine(parentDirectory, "UploadedImages");
    }

    public async Task UploadTestCaseResultImagesAsync(IEnumerable<IFormFile> files, string testCaseResultId)
    {
        var testCaseId = await GetTestCaseId(testCaseResultId);

        CreateBaseDirectoryIfNeeded();

        var testCaseDirectoryPath = Path.Combine(_targetFilePath, testCaseId);
        CreateTestCaseDirectoryIfNeeded(testCaseDirectoryPath);

        foreach (var file in files)
        {
            var fileName = GenerateGuid() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(testCaseDirectoryPath, fileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);

            await SaveImageInDatabase(testCaseResultId, fileName, filePath);
        }
    }

    private async Task<string> GetTestCaseId(string testCaseResultId)
    {
        var testCaseResult = await _unitOfWork.TestCaseResultRepository.GetByIdAsync(testCaseResultId)
            ?? throw new NotFoundException($"You must inform an existing test case result id. TestCaseResult with id {testCaseResultId} not found");

        return testCaseResult.TestCaseId;
    }

    private void CreateBaseDirectoryIfNeeded()
    {
        if (!Directory.Exists(_targetFilePath))
        {
            Directory.CreateDirectory(_targetFilePath);
        }
    }

    private static void CreateTestCaseDirectoryIfNeeded(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }

    private async Task SaveImageInDatabase(string testCaseResultId, string fileName, string filePath)
    {
        var image = new DomainImage
        {
            Id = GenerateGuid(),
            TestCaseResultId = testCaseResultId,
            FileName = fileName,
            FilePath = filePath
        };

        await _unitOfWork.ImageRepository.AddAsync(image);
        await _unitOfWork.CompleteAsync();
    }
}
