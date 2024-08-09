using Testeger.Domain.Models.Entities;

namespace Testeger.Infra.Repositories.Images;

public interface IImageRepository : IRepository<Image>
{
    Task<IEnumerable<string>> GetTestCaseResultImagesFilePathsAsync(string testCaseResultId);
}
