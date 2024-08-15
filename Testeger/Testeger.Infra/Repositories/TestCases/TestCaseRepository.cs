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

    public async Task<TestCase> GetTestCaseByIdAsync(string id)
    {
        var testCase = await _dbSet
            .Include(tc => tc.Details)
            .Include(tc => tc.History)
            .FirstOrDefaultAsync(tc => tc.Id == id);

        return testCase;
    }

    public async Task<IEnumerable<TestCase>> GetTestCasesByProjectIdAsync(string projectId)
    {
        var testCases = await _dbSet
            .Include(tc => tc.Details)
            .Include(tc => tc.History)
            .Include(tc => tc.Results)
            .Where(tc => tc.ProjectId == projectId)
            .ToListAsync();

        return testCases;
    }

    public async Task<IEnumerable<TestCase>> GetTestCasesByTestRequestIdAsync(string testRequestId)
    {
        var testCases = await _dbSet
            .Include(tc => tc.Details)
            .Include(tc => tc.History)
            .Where(tc => tc.TestRequestId == testRequestId)
            .ToListAsync();

        return testCases;
    }
}
