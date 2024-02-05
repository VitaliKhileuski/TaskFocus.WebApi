using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TaskFocus.Data.Entities;

namespace TaskFocus.Data.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        Task<ICollection<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null);

        IQueryable<TEntity> GetAllQueryable(Expression<Func<TEntity, bool>> filter = null);

        Task<List<TEntity>> AddRangeAsync(List<TEntity> entities);

        Task<IQueryable<TEntity>> UpdateRangeAsync(List<TEntity> entities);

        Task<TEntity> FindByIdAsync(Guid id, bool include = true);
        
        Task<TEntity> UpdateAsync(TEntity entity);
        
        Task DeleteByIdAsync(Guid id);
        
        Task<TEntity> CreateAsync(TEntity entity);

        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> filter);

        Task Delete(IEnumerable<TEntity> entities);
        
        Task<PagedResult<TEntity>> GetPagedAsync(List<Expression<Func<TEntity, bool>>> filters = null,
            Expression<Func<TEntity, object>> sorter = null, bool sortAsc = true, int skip = 0, int take = 20, Expression<Func<TEntity, TEntity>> selectExpression = null);

        IQueryable<TEntity> GetOrderedQueryableCollection(
            List<Expression<Func<TEntity, bool>>> filters = null,
            Expression<Func<TEntity, object>> sorter = null,
            bool sortAsc = true);
    }
}