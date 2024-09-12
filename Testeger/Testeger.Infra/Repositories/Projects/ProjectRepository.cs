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

    public async Task<IEnumerable<Project>> GetProjectsForUserAsync(string userId)
    {
        var projects = await _dbSet
            .Where(p => p.ProjectUsers.Any(pu => pu.UserId == userId))
            .ToListAsync();

        return projects;
    }

    public async Task<Project> GetProjectByIdAsync(string id)
    {
        var project = await _dbSet
                            .Include(tr => tr.ProjectUsers)
                            .FirstOrDefaultAsync(tr => tr.Id == id);
        return project;
    }
}
