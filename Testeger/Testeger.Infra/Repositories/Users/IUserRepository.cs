using Testeger.Domain.Models.Entities;

namespace Testeger.Infra.Repositories.Users;

public interface IUserRepository : IRepository<ApplicationUser>
{
    Task<IEnumerable<ApplicationUser>> GetUsersByProject(string projectId);
}
