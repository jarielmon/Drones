using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Drones.Domain.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
    Task AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task<TEntity> GetByIdAsync(int id);
    TEntity GetById(int id);
    Task<IEnumerable<TEntity>> GetAllAsync(IEnumerable<Expression<Func<TEntity, bool>>>? predicates = null);    
    void Delete(TEntity entity);
    Task DeleteAsync(TEntity entity);
    Task<int> Count(IEnumerable<Expression<Func<TEntity, bool>>>? predicates = null);
    Task<bool> ExistAsync(Expression<Func<TEntity, bool>> predicate);
}
