using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories;

public interface IRepository<TEntity, TId> where TEntity : Entity<TId>, new()
{
    Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null, bool enableAutoInclude = true);
    Task<TEntity?> GetByIdAsync(TId id);
    Task<TEntity?> UpdateAsync(TEntity entity);
    Task<TEntity?> AddAsync(TEntity entity);
    Task<TEntity?> RemoveAsync(TEntity entity);
}
