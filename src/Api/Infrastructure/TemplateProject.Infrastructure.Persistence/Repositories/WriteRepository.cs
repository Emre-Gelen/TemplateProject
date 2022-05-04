using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TemplateProject.Api.Application.Repositories;
using TemplateProject.Common.Bases;
using TemplateProject.Infrastructure.Persistence.Context;

namespace TemplateProject.Infrastructure.Persistence.Repositories
{
    public class WriteRepository<TEntity> : IWriteRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly TemplateProjectContext _dbContext;
        protected DbSet<TEntity> entity => _dbContext.Set<TEntity>();

        public WriteRepository(TemplateProjectContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<int> AddAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public int Add(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public int AddRange(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public int Update(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> RemoveAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> RemoveAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveRangeAsync(Expression<Func<TEntity, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public int Remove(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public int Remove(Guid Id)
        {
            throw new NotImplementedException();
        }

        public bool RemoveRange(Expression<Func<TEntity, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<int> AddOrUpdateAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public int AddOrUpdate(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task BulkDeleteById(IEnumerable<Guid> ids)
        {
            throw new NotImplementedException();
        }

        public Task BulkDelete(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public Task BulkDelete(Expression<Func<TEntity, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task BulkUpdate(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public Task BulkAdd(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }
        public async Task<int> SaveAsync()
            => await _dbContext.SaveChangesAsync();
    }
}
