using System.Linq.Expressions;

namespace TemplateProject.Api.Application.Repositories
{
    public interface IReadRepository<TEntity>
    {
        Task<List<TEntity>> GetAllAsync();
        Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> expression, bool noTracking = true, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity> GetByIdAsync(Guid id, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> expression, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes);
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> expression, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes);
    }
}
