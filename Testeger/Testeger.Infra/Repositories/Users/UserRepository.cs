using Microsoft.EntityFrameworkCore;
using Testeger.Domain.Models.Entities;
using Testeger.Infra.Context;

namespace Testeger.Infra.Repositories.Users;

public class UserRepository : Repository<ApplicationUser>, IUserRepository
{
    private DbSet<ApplicationUser> _dbSet;

    public UserRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbSet = _dbContext.Set<ApplicationUser>();
    }

    public async Task<IEnumerable<ApplicationUser>> GetUsersByProject(string projectId)
    {
        var users = await _dbSet
            .Where(u => u.ProjectUsers.Any(pu => pu.ProjectId == projectId))
            .ToListAsync();

        return users;
    }
}
