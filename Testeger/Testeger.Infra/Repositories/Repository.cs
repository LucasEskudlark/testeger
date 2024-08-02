using Microsoft.EntityFrameworkCore;

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

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public async Task DeleteAsync(string id)
    {
        var entity = await _dbSet.FindAsync(id) ?? throw new ArgumentException($"No entity with id {id} was found");
        _dbSet.Remove(entity);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.AsNoTracking().ToListAsync();
    }

    public async Task<T> GetByIdAsync(string id)
    {
        var entity = await _dbSet.FindAsync(id) ?? throw new ArgumentException($"No entity with id {id} was found");
        return entity;
    }
}
