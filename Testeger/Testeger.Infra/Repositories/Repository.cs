using Microsoft.EntityFrameworkCore;
using Testeger.Domain.Models.Pagination;

namespace Testeger.Infra.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly DbContext _dbContext;
    private readonly DbSet<T> _dbSet;

    public Repository(DbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<T>();
    }

    public Task Update(T entity)
    {
        _dbSet.Update(entity);
        return Task.CompletedTask;
    }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public Task Delete(T entity)
    {
        _dbSet.Remove(entity);
        return Task.CompletedTask;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.AsNoTracking().ToListAsync();
    }

    public async Task<T?> GetByIdAsync(string id)
    {
        var entity = await _dbSet.FindAsync(id);
        return entity;
    }

    public async Task<PagedResult<T>> GetAllPagedAsync(int pageSize, int pageNumber)
    {
        var totalItems = await _dbSet.AsNoTracking().CountAsync();

        var categories = await _dbSet
            .AsNoTracking()
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<T>
        {
            Items = categories,
            TotalItems = totalItems,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
    }
}
