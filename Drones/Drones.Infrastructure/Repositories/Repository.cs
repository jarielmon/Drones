using Drones.Domain.Entities;
using Drones.Domain.Repositories;
using Drones.Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Drones.Infrastructure.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly DbSet<TEntity> _entities;

    public Repository(DbSet<TEntity> entities) => _entities = entities;

    public async Task AddAsync(TEntity entity) => await _entities.AddAsync(entity);
    public async Task UpdateAsync(TEntity entity) => await Task.Run(() => { _entities.Update(entity); });
    public async Task<TEntity> GetByIdAsync(int id) => await _entities.FirstOrDefaultAsync(e => e.Id == id);
    public TEntity GetById(int id) => _entities.FirstOrDefault(e => e.Id == id);
    public async Task<IEnumerable<TEntity>> GetAllAsync(IEnumerable<Expression<Func<TEntity, bool>>>? predicates) => await _entities.Filter(predicates).ToListAsync();
    public void Delete(TEntity entity) => _entities.Remove(entity);
    public async Task DeleteAsync(TEntity entity) => await Task.Run(() => { Delete(entity); });
    public async virtual Task<int> Count(IEnumerable<Expression<Func<TEntity, bool>>>? predicates = null) => await _entities.Filter(predicates).CountAsync();
    public async Task<bool> ExistAsync(Expression<Func<TEntity, bool>> predicate) => await _entities.AnyAsync(predicate);
}
