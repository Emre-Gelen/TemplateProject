using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TemplateProject.Api.Application.Repositories
{
    public interface IWriteRepository<TEntity>
    {
        Task<int> AddAsync(TEntity entity);
        Task<int> AddRangeAsync(IEnumerable<TEntity> entities);
        int Add(TEntity entity);
        int AddRange(IEnumerable<TEntity> entities);

        Task<int> UpdateAsync(TEntity entity);
        int Update(TEntity entity);

        Task<int> RemoveAsync(TEntity entity);
        Task<int> RemoveAsync(Guid Id);
        Task<bool> RemoveRangeAsync(Expression<Func<TEntity,bool>> expression);

        int Remove(TEntity entity);
        int Remove(Guid Id);
        bool RemoveRange(Expression<Func<TEntity, bool>> expression);

        Task<int> AddOrUpdateAsync(TEntity entity);
        int AddOrUpdate(TEntity entity);

        Task BulkDeleteById(IEnumerable<Guid> ids);
        Task BulkDelete(IEnumerable<TEntity> entities);
        Task BulkDelete(Expression<Func<TEntity, bool>> expression);
        Task BulkUpdate(IEnumerable<TEntity> entities);
        Task BulkAdd(IEnumerable<TEntity> entities);

        Task<int> SaveAsync();
    }
}
