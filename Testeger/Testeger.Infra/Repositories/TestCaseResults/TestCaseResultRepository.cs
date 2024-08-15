using Microsoft.EntityFrameworkCore;
using Testeger.Domain.Models.Entities;
using Testeger.Infra.Context;

namespace Testeger.Infra.Repositories.TestCaseResults;

public class TestCaseResultRepository : Repository<TestCaseResult>, ITestCaseResultRepository
{
    private readonly DbSet<TestCaseResult> _dbSet;

    public TestCaseResultRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbSet = _dbContext.Set<TestCaseResult>();
    }

    public async Task<int> GetNextNumberAsync(string testCaseId)
    {
        var maxNumber = await _dbSet
            .Where(tr => tr.TestCaseId == testCaseId)
            .MaxAsync(tr => (int?)tr.Number) ?? 0;

        return maxNumber + 1;
    }

    public async Task<IEnumerable<TestCaseResult>> GetResultsByTestCaseId(string testCaseId)
    {
        var testCases = await _dbSet.Where(tr => tr.TestCaseId == testCaseId).ToListAsync();

        return testCases;
    }

    public Task UpdateTestCaseResult(TestCaseResult testCaseResult)
    {
        _dbSet.Attach(testCaseResult);
        _dbSet.Entry(testCaseResult).State = EntityState.Modified;

        var excludedProperties = new[] { "Number", "Id", "TestCaseId" };

        foreach (var propertyName in excludedProperties)
        {
            _dbSet.Entry(testCaseResult).Property(propertyName).IsModified = false;
        }

        return Task.CompletedTask;
    }
}
