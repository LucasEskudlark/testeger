using Microsoft.EntityFrameworkCore;
using Testeger.Domain.Models.Entities;
using Testeger.Infra.Context;

namespace Testeger.Infra.Repositories.Images;

public class ImageRepository : Repository<Image>, IImageRepository
{
    private readonly DbSet<Image> _dbSet;

    public ImageRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbSet = _dbContext.Set<Image>();
    }

    public async Task<IEnumerable<string>> GetTestCaseResultImagesFilePathsAsync(string testCaseResultId)
    {
        return await _dbSet
            .Where(i => i.TestCaseResultId == testCaseResultId)
            .Select(i => i.FilePath)
            .ToListAsync();
    }
}
