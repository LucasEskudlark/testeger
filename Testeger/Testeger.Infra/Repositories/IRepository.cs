using Testeger.Domain.Models.Pagination;

namespace Testeger.Infra.Repositories;

public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(string id);
    Task<IEnumerable<T>> GetAllAsync();
    Task AddAsync(T entity);
    Task Update(T entity);
    Task Delete(T entity);
    Task<PagedResult<T>> GetAllPagedAsync(int pageSize, int pageNumber);
}
