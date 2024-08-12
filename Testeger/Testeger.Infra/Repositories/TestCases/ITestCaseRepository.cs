using Testeger.Domain.Models.Entities;

namespace Testeger.Infra.Repositories.TestCases;

public interface ITestCaseRepository : IRepository<TestCase>
{
    Task<IEnumerable<TestCase>> GetTestCasesByTestRequestIdAsync(string testRequestId);
}
