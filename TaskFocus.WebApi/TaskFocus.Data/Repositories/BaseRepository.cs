using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using TaskFocus.Data.Entities;
using TaskFocus.Data.Interfaces;

namespace TaskFocus.Data.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly Context _db;

        protected abstract IQueryable<TEntity> CollectionWithIncludes { get; set; }

        private DbSet<TEntity> DbSet { get; }

        protected BaseRepository(Context context)
        {
            _db = context;
            DbSet = context.Set<TEntity>();
        }

        public async Task<ICollection<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            if (filter == null)
            {
                return await CollectionWithIncludes.ToListAsync();
            }

            return await CollectionWithIncludes.Where(filter).ToListAsync();
        }

        public IQueryable<TEntity> GetAllQueryable(Expression<Func<TEntity, bool>> filter = null)
        {
            return filter == null ? CollectionWithIncludes : CollectionWithIncludes.Where(filter);
        }

        public async Task<List<TEntity>> AddRangeAsync(List<TEntity> entities)
        {
            await DbSet.AddRangeAsync(entities);

            var savedAmount = await _db.SaveChangesAsync();

            if (savedAmount > 0)
            {
                return entities;
            }

            return null;
        }

        public async Task<IQueryable<TEntity>> UpdateRangeAsync(List<TEntity> entities)
        {
            DbSet.UpdateRange(entities);

            await _db.SaveChangesAsync();

            var entitiesIds = entities.Select(x => x.Id);

            var updatedEntities = CollectionWithIncludes
                .Where(x => entitiesIds.Contains(x.Id));

            return updatedEntities;
        }

        public async Task<TEntity> FindByIdAsync(Guid id, bool include = true)
        {
            var collection = include ? CollectionWithIncludes : DbSet;

            return await collection.FirstOrDefaultAsync(entity => entity.Id == id);
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            var updatedEntityEntry = DbSet.Update(entity);

            await _db.SaveChangesAsync();

            var updatedEntity = await FindByIdAsync(updatedEntityEntry.Entity.Id);

            return updatedEntity;
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var deletingEntity = await CollectionWithIncludes.FirstOrDefaultAsync(x => x.Id == id);

            DbSet.Remove(deletingEntity);

            await _db.SaveChangesAsync();
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            await DbSet.AddAsync(entity);

            var savedAmount = await _db.SaveChangesAsync();

            if (savedAmount > 0)
            {
                return await FindByIdAsync(entity.Id);
            }

            return null;
        }

        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await CollectionWithIncludes.FirstOrDefaultAsync(filter);
        }

        public async Task Delete(IEnumerable<TEntity> entities)
        {
            DbSet.RemoveRange(entities);

            await _db.SaveChangesAsync();
        }

        public async Task<PagedResult<TEntity>> GetPagedAsync(
            List<Expression<Func<TEntity, bool>>> filters = null,
            Expression<Func<TEntity, object>> sorter = null,
            bool sortAsc = true,
            int skip = 0,
            int take = 0,
            Expression<Func<TEntity, TEntity>> selectExpression = null)
        {
            var result = new PagedResult<TEntity>();

            var query = GetOrderedQueryableCollection(filters, sorter, sortAsc);

            result.TotalCount = await query.CountAsync();

            if (skip > 0)
            {
                query = query.Skip(skip);
            }

            if (take > 0)
            {
                query = query.Take(take);
            }

            query = selectExpression == null ? query : query.Select(selectExpression);

            result.Items = await query.AsNoTracking().ToListAsync();

            return result;
        }

        public IQueryable<TEntity> GetOrderedQueryableCollection(
            List<Expression<Func<TEntity, bool>>> filters = null,
            Expression<Func<TEntity, object>> sorter = null,
            bool sortAsc = true)
        {
            var query = CollectionWithIncludes;

            if (filters != null)
            {
                filters.ForEach(f => query = query.Where(f));
            }

            if (sorter != null)
            {
                query = sortAsc ? query.OrderBy(sorter).ThenBy(x => x.Id) : query.OrderByDescending(sorter).ThenBy(x => x.Id);
            }

            return query;
        }

        public async Task<IDbContextTransaction> CreateTransaction(IsolationLevel isolationLevel)
        {
            var transaction = await _db.Database.BeginTransactionAsync(isolationLevel);

            return transaction;
        }
    }
}