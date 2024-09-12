using Testeger.Domain.Models.Entities;

namespace Testeger.Infra.Repositories.Projects;

public interface IProjectRepository : IRepository<Project>
{
    Task<IEnumerable<Project>> GetProjectsForUserAsync(string userId);
    Task<Project> GetProjectByIdAsync(string id);
}
