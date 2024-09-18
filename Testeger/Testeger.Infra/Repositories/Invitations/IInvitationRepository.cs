using Testeger.Domain.Models.Entities;

namespace Testeger.Infra.Repositories.Invitations;

public interface IInvitationRepository : IRepository<Invitation>
{
    Task<Invitation> GetByIdAndTokenAsync(string id, string token);
}
