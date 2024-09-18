using Testeger.Infra.Context;
using Testeger.Infra.Repositories.Images;
using Testeger.Infra.Repositories.Invitations;
using Testeger.Infra.Repositories.Projects;
using Testeger.Infra.Repositories.TestCaseResults;
using Testeger.Infra.Repositories.TestCases;
using Testeger.Infra.Repositories.TestRequests;

namespace Testeger.Infra.UnitOfWork;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private AppDbContext _dbContext;
    private IProjectRepository? _projectRepository;
    private ITestRequestRepository? _testRequestRepository;
    private ITestCaseRepository? _testCaseRepository;
    private ITestCaseResultRepository? _testCaseResultRepository;
    private IImageRepository? _imageRepository;
    private IInvitationRepository? _invitationRepository;

    public UnitOfWork(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IProjectRepository ProjectRepository
    {
        get
        {
            return _projectRepository ??= new ProjectRepository(_dbContext);
        }
    }

    public ITestRequestRepository TestRequestRepository
    {
        get
        {
            return _testRequestRepository ??= new TestRequestRepository(_dbContext);
        }
    }

    public ITestCaseRepository TestCaseRepository
    {
        get
        {
            return _testCaseRepository ??= new TestCaseRepository(_dbContext);
        }
    }

    public ITestCaseResultRepository TestCaseResultRepository
    {
        get
        {
            return _testCaseResultRepository ??= new TestCaseResultRepository(_dbContext);
        }
    }

    public IImageRepository ImageRepository
    {
        get
        {
            return _imageRepository ??= new ImageRepository(_dbContext);
        }
    }

    public IInvitationRepository InvitationRepository
    {
        get
        {
            return _invitationRepository ??= new InvitationRepository(_dbContext);
        }
    }

    public async Task CompleteAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

    private bool disposed = false;
    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }
        this.disposed = true;
    }
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
