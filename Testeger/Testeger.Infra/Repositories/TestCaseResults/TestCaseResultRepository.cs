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
}
