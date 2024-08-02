﻿namespace Testeger.Infra.Repositories;

public interface IRepository<T> where T : class
{
    Task<T> GetByIdAsync(string id);
    Task<IEnumerable<T>> GetAllAsync();
    Task AddAsync(T entity);
    Task DeleteAsync(string id);
}