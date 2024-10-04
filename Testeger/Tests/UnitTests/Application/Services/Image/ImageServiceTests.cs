using Org.BouncyCastle.Asn1.Ocsp;
using Testeger.Application.Services.File;
using Testeger.Application.Services.Image;
using Testeger.Infra.Repositories.Images;
using Testeger.Infra.Repositories.TestCaseResults;

namespace UnitTests.Application.Services.Image;

public class ImageServiceTests : BaseServiceTests
{
    private readonly ImageService _imageService;
    private readonly Mock<IFileSystem> _fileSystem;
    private readonly Mock<ITestCaseResultRepository> _testCaseResultRepository;
    private readonly Mock<IImageRepository> _imageRepository;
    private const string FakeTestCaseResultId = "FakeTestCaseResultId";

    public ImageServiceTests()
    {
        _testCaseResultRepository = new Mock<ITestCaseResultRepository>();
        _imageRepository = new Mock<IImageRepository>();
        _fileSystem = new Mock<IFileSystem>();

        _unitOfWork.SetupGet(u => u.TestCaseResultRepository).Returns(_testCaseResultRepository.Object);
        _unitOfWork.SetupGet(u => u.ImageRepository).Returns(_imageRepository.Object);
        _imageService = new(_unitOfWork.Object, _mapper.Object, _fileSystem.Object);
    }

    [Fact]
    public async Task GetTestCaseResultImagesFilePathsAsync_GivenValidId_ShouldReturnAsExpected()
    {
        IEnumerable<string> paths = [string.Empty, string.Empty];

        _unitOfWork.Setup(u => u.ImageRepository.GetTestCaseResultImagesFilePathsAsync(It.IsAny<string>()))
            .ReturnsAsync(paths);

        var result = await _imageService.GetTestCaseResultImagesFilePathsAsync(It.IsAny<string>());

        result.Should().BeEquivalentTo(paths);
    }

    [Fact]
    public async Task GetTestCaseResultImagesFilePathsAsync_GivenEmptyPaths_ShouldThrowNotFoundException()
    {
        IEnumerable<string> paths = [];

        _unitOfWork.Setup(u => u.ImageRepository.GetTestCaseResultImagesFilePathsAsync(It.IsAny<string>()))
            .ReturnsAsync(paths);

        await _imageService
            .Invoking(service => service.GetTestCaseResultImagesFilePathsAsync(It.IsAny<string>()))
            .Should()
            .ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task GetImageAsync_GivenExistingImage_ShouldReturnsStreamAndContentType()
    {
        var imagePath = "image/testCaseId/image.jpg";
        var fullPath = Path.GetFullPath(Path.Combine("../", imagePath));
        var expectedStream = new MemoryStream();
        var expectedContentType = "image/jpeg";

        _fileSystem.Setup(fs => fs.FileExists(fullPath)).Returns(true);
        _fileSystem.Setup(fs => fs.OpenRead(fullPath)).Returns(expectedStream);

        var result = await _imageService.GetImageAsync(imagePath);

        result.Should().NotBeNull();
        result.ImageStream.Should().BeSameAs(expectedStream);
        result.ContentType.Should().Be(expectedContentType);
        _fileSystem.Verify(fs => fs.FileExists(fullPath), Times.Once);
        _fileSystem.Verify(fs => fs.OpenRead(fullPath), Times.Once);
    }

    [Fact]
    public async Task GetImageAsync_GivenNonExistingImage_ShouldThrowsNotFoundException()
    {
        var imagePath = "image/testCaseId/non_existing_image.jpg";
        var fullPath = Path.GetFullPath(Path.Combine("../", imagePath));

        _fileSystem.Setup(fs => fs.FileExists(fullPath)).Returns(false);

        await _imageService
            .Invoking(service => service.GetImageAsync(imagePath))
            .Should()
            .ThrowAsync<NotFoundException>();

        _fileSystem.Verify(fs => fs.FileExists(fullPath), Times.Once);
        _fileSystem.Verify(fs => fs.OpenRead(fullPath), Times.Never);
    }

    [Theory]
    [InlineData("image.png", "image/png")]
    [InlineData("image.gif", "image/gif")]
    [InlineData("image.bmp", "application/octet-stream")]
    public async Task GetImageAsync_GivenDifferentFileExtensions_ShouldReturnCorrectContentType(string fileName, string expectedContentType)
    {
        var imagePath = $"image/testCaseId/{fileName}";
        var fullPath = Path.GetFullPath(Path.Combine("../", imagePath));
        var expectedStream = new MemoryStream();

        _fileSystem.Setup(fs => fs.FileExists(fullPath)).Returns(true);
        _fileSystem.Setup(fs => fs.OpenRead(fullPath)).Returns(expectedStream);

        var result = await _imageService.GetImageAsync(imagePath);

        result.Should().NotBeNull();
        result.ImageStream.Should().BeSameAs(expectedStream);
        result.ContentType.Should().Be(expectedContentType);
    }
}
