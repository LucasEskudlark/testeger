﻿using Microsoft.EntityFrameworkCore;
using Testeger.Domain.Models.Entities;
using Testeger.Infra.Context;

namespace Testeger.Infra.Repositories.Invitations;

public class InvitationRepository : Repository<Invitation>, IInvitationRepository
{
    private readonly DbSet<Invitation> _dbSet;

    public InvitationRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbSet = _dbContext.Set<Invitation>();
    }

    public async Task<Invitation> GetByIdAndTokenAsync(string id, string token)
    {
        var invitation = await _dbSet.FirstOrDefaultAsync(i => i.Id == id && i.InvitationToken == token);

        return invitation;
    }
}
