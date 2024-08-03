using Microsoft.EntityFrameworkCore;
using Testeger.Domain.Models.Entities;

namespace Testeger.Infra.Repositories.Projects;

public class ProjectRepository : Repository<Project>, IProjectRepository
{
    private readonly DbSet<Project> _dbSet;

    public ProjectRepository(DbContext dbContext) : base(dbContext)
    {
        _dbSet = _dbContext.Set<Project>();
    }
}
