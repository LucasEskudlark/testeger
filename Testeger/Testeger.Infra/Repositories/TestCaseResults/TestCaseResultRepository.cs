using Microsoft.EntityFrameworkCore;
using Testeger.Domain.Models.Entities;

namespace Testeger.Infra.Repositories.TestCaseResults;

public class TestCaseResultRepository : Repository<TestCaseResult>, ITestCaseResultRepository
{
    private readonly DbSet<TestCaseResult> _dbSet;

    public TestCaseResultRepository(DbContext dbContext) : base(dbContext)
    {
        _dbSet = _dbContext.Set<TestCaseResult>();
    }
}
