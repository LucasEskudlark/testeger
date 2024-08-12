using Microsoft.EntityFrameworkCore;
using Testeger.Domain.Models.Entities;
using Testeger.Infra.Context;

namespace Testeger.Infra.Repositories.TestCases;

public class TestCaseRepository : Repository<TestCase>, ITestCaseRepository
{
    private readonly DbSet<TestCase> _dbSet;

    public TestCaseRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbSet = _dbContext.Set<TestCase>();
    }

    public async Task<IEnumerable<TestCase>> GetTestCasesByTestRequestIdAsync(string testRequestId)
    {
        var testCases = await _dbSet.Where(tc => tc.TestRequestId == testRequestId).ToListAsync();
        return testCases;
    }
}
