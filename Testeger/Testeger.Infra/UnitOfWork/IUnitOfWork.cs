using Testeger.Infra.Repositories.Images;
using Testeger.Infra.Repositories.Projects;
using Testeger.Infra.Repositories.TestCaseResults;
using Testeger.Infra.Repositories.TestCases;
using Testeger.Infra.Repositories.TestRequests;

namespace Testeger.Infra.UnitOfWork;

public interface IUnitOfWork
{
    IProjectRepository ProjectRepository { get; }
    ITestRequestRepository TestRequestRepository { get; }
    ITestCaseRepository TestCaseRepository { get; }
    ITestCaseResultRepository TestCaseResultRepository { get; }
    IImageRepository ImageRepository { get; }

    Task CompleteAsync();
}
