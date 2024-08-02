using Microsoft.EntityFrameworkCore;
using Testeger.Domain.Models.Entities;

namespace Testeger.Infra.Repositories.TestRequests;

public class TestRequestRepository : Repository<TestRequest>, ITestRequestRepository
{
    private readonly DbSet<TestRequest> _dbSet;
    public TestRequestRepository(DbContext dbContext) : base(dbContext)
    {
        _dbSet = _dbContext.Set<TestRequest>();
    }
}
