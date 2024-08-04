using Microsoft.EntityFrameworkCore;
using Testeger.Domain.Models.Entities;
using Testeger.Infra.Context;

namespace Testeger.Infra.Repositories.TestRequests;

public class TestRequestRepository : Repository<TestRequest>, ITestRequestRepository
{
    private readonly DbSet<TestRequest> _dbSet;
    public TestRequestRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbSet = _dbContext.Set<TestRequest>();
    }
}
