using Testeger.Domain.Models.Entities;

namespace Testeger.Infra.Repositories.TestRequests;

public interface ITestRequestRepository : IRepository<TestRequest>
{
    Task<int> GetNextNumberAsync(string projectId);
}
