using Testeger.Domain.Models.Entities;

namespace Testeger.Infra.Repositories.TestCaseResults;

public interface ITestCaseResultRepository : IRepository<TestCaseResult>
{
    Task<int> GetNextNumberAsync(string testCaseId);
    Task<IEnumerable<TestCaseResult>> GetResultsByTestCaseId(string testCaseId);
}
