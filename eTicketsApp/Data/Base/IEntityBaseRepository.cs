﻿using eTicketsApp.Models;
using System.Linq.Expressions;

namespace eTicketsApp.Data.Base
{
    public interface IEntityBaseRepository<T> where T: class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties);
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task<T> UpdateAsync(int id, T entity);
        Task DeleteAsync(int id);
    }
}
