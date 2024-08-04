using Microsoft.EntityFrameworkCore;
using Testeger.Domain.Models.Entities;
using Testeger.Infra.Context;

namespace Testeger.Infra.Repositories.Projects;

public class ProjectRepository : Repository<Project>, IProjectRepository
{
    private readonly DbSet<Project> _dbSet;

    public ProjectRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbSet = _dbContext.Set<Project>();
    }
}
