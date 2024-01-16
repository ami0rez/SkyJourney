using SkyJourney.Infrastructure.Data.Models;
using System.Linq.Expressions;

namespace SkyJourney.Infrastructure.Repositories.Generic
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        IQueryable<TEntity> FindAll(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");

        Task<TEntity> FindFirstAsync(
           Expression<Func<TEntity, bool>> filter = null,
           string includeProperties = "");
        Task<TResult> FindFirstValueAsync<TResult>(
           Expression<Func<TEntity, bool>> filter = null,
           Expression<Func<TEntity, TResult>> selector = null);
        IQueryable<TEntity> FindFirst(
           Expression<Func<TEntity, bool>> filter = null,
           string includeProperties = "");

        Task<TEntity> FindById(Guid? id);

        Task CreateAsync(TEntity entity);

        Task CreateRangeAsync(IEnumerable<TEntity> entities);

        Task<bool> Exists(Guid id);

        Task<bool> Exists(Expression<Func<TEntity, bool>> filter = null);
        Task<int> Count(Expression<Func<TEntity, bool>> filter = null);
        Task<double> Sum(Expression<Func<TEntity, double>> filter = null);

        Task UpdateAsync(Guid id, TEntity entity);

        Task UpdateRangeAsync(IEnumerable<TEntity> entities);

        Task<TEntity> Delete(Guid id);

        /// <summary>
        /// Delete Entities.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteRange(IEnumerable<TEntity> entities);

        Task SaveChangesAsync();
    }
}
