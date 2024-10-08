﻿using Microsoft.EntityFrameworkCore;
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

    public async Task<int> GetNextNumberAsync(string projectId)
    {
        var maxNumber = await _dbSet
            .Where(tr => tr.ProjectId == projectId)
            .MaxAsync(tr => (int?)tr.Number) ?? 0;

        return maxNumber + 1;
    }

    public async Task<TestRequest> GetTestRequestByIdAsync(string id)
    {
        var testRequest = await _dbSet
                            .Include(tr => tr.History)
                            .FirstOrDefaultAsync(tr => tr.Id == id);
        return testRequest;
    }

    public async Task<IEnumerable<TestRequest>> GetTestRequestsByProjectIdAsync(string projectId)
    {
        return await _dbSet.Where(tr => tr.ProjectId == projectId).ToListAsync();
    }
}
