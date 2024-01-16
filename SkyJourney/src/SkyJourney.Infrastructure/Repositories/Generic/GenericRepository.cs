using Amirez.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using SkyJourney.Infrastructure.Data.Models;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace SkyJourney.Infrastructure.Repositories.Generic
{
    public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly DatabaseContext _context;

        public GenericRepository(DatabaseContext dbContext)
        {
            _context = dbContext;
        }

        /// <summary>
        /// Create Entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task CreateAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }


        /// <summary>
        /// Create Range of Entities.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task CreateRangeAsync(IEnumerable<TEntity> entities)
        {
            await _context.Set<TEntity>().AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Delete Entity.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> Delete(Guid id)
        {
            var entity = await FindById(id);
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        /// <summary>
        /// Delete Entities.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task DeleteRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Save Changes Async.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Find List of entities depending on the filter and order and the included list
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public virtual IQueryable<TEntity> FindAll(
           Expression<Func<TEntity, bool>> filter = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
           string includeProperties = "")
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).AsNoTracking();
            }
            else
            {
                return query.AsNoTracking();
            }
        }

        /// <summary>
        /// Find first element depending on the filter and the included list
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> FindFirstAsync(
           Expression<Func<TEntity, bool>> filter = null,
           string includeProperties = "")
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync();
        }

        /// <summary>
        /// Find first element depending on the filter and the included list
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public virtual async Task<TResult> FindFirstValueAsync<TResult>(
           Expression<Func<TEntity, bool>> filter = null,
           Expression<Func<TEntity, TResult>> selector= null)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            query = query.AsNoTracking();
            return await query
                .Select(selector)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Find first element depending on the filter and the included list
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public virtual IQueryable<TEntity> FindFirst(
           Expression<Func<TEntity, bool>> filter = null,
           string includeProperties = "")
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            query = query.AsNoTracking();
            return query;
        }

        /// <summary>
        /// Find entity by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> FindById(Guid? id)
        {
            return await _context.Set<TEntity>()
               .AsNoTracking()
               .FirstOrDefaultAsync(e => e.Id == id);
        }

        /// <summary>
        /// Find entity by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> FindQueryableById(Guid? id)
        {
            return await _context.Set<TEntity>()
               .AsNoTracking()
               .FirstOrDefaultAsync(e => e.Id == id);
        }

        /// <summary>
        /// Update Entity.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UpdateAsync(Guid id, TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Update Entity.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().UpdateRange(entities);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Checks if an entity exists by its id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<bool> Exists(Guid id)
        {
            return await _context.Set<TEntity>().AnyAsync(entity => entity.Id == id);
        }

        /// <summary>
        /// Checks if an entity exists by its id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<bool> Exists(Expression<Func<TEntity, bool>> filter = null)
        {
            if (filter != null)
            {
                return await _context.Set<TEntity>()
                    .AsNoTracking()
                    .AnyAsync(filter);
            }
            else
            {
                return await _context.Set<TEntity>()
                    .AsNoTracking()
                    .AnyAsync();
            }

        }

        /// <summary>
        /// Checks if an entity exists by its id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<int> Count(Expression<Func<TEntity, bool>> filter = null)
        {
            if (filter != null)
            {
                return await _context.Set<TEntity>()
                    .AsNoTracking()
                    .CountAsync(filter);
            }
            else
            {
                return await _context.Set<TEntity>()
                    .AsNoTracking()
                    .CountAsync();
            }

        }

        /// <summary>
        /// Checks if an entity exists by its id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<double> Sum([NotNull] Expression<Func<TEntity, double>> filter)
        {
            return await _context.Set<TEntity>()
                   .SumAsync(filter);

        }
    }
}
