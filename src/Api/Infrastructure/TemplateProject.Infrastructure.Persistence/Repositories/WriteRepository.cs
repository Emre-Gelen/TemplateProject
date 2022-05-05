using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TemplateProject.Api.Application.Repositories;
using TemplateProject.Common.Bases;

namespace TemplateProject.Infrastructure.Persistence.Repositories
{
    public class WriteRepository<TEntity> : IWriteRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly DbContext _dbContext;
        protected DbSet<TEntity> entity => _dbContext.Set<TEntity>();

        public WriteRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<int> AddAsync(TEntity entity)
        {
            await this.entity.AddAsync(entity);
            return await _dbContext.SaveChangesAsync();
        }

        public virtual async Task<int> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await this.entity.AddRangeAsync(entities);
            return await _dbContext.SaveChangesAsync();
        }

        public virtual int Add(TEntity entity)
        {
            this.entity.Add(entity);
            return _dbContext.SaveChanges();
        }

        public virtual int AddRange(IEnumerable<TEntity> entities)
        {
            this.entity.AddRange(entities);
            return _dbContext.SaveChanges();
        }

        public virtual async Task<int> UpdateAsync(TEntity entity)
        {
            this.entity.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;

            return await _dbContext.SaveChangesAsync();
        }

        public virtual int Update(TEntity entity)
        {
            this.entity.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;

            return _dbContext.SaveChanges();
        }

        public virtual async Task<int> RemoveAsync(TEntity entity)
        {
            if (_dbContext.Entry(entity).State == EntityState.Detached)
            {
                this.entity.Attach(entity);
            }
            this.entity.Remove(entity);

            return await _dbContext.SaveChangesAsync();
        }

        public virtual async Task<int> RemoveAsync(Guid Id)
        {
            var entity = this.entity.Find(Id);
            return await RemoveAsync(entity);
        }

        public virtual async Task<bool> RemoveRangeAsync(Expression<Func<TEntity, bool>> expression)
        {
            _dbContext.RemoveRange(entity.Where(expression));
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public virtual int Remove(TEntity entity)
        {
            if (_dbContext.Entry(entity).State == EntityState.Detached)
            {
                this.entity.Attach(entity);
            }
            this.entity.Remove(entity);

            return _dbContext.SaveChanges();
        }

        public virtual int Remove(Guid Id)
        {
            var entity = this.entity.Find(Id);
            return Remove(entity);
        }

        public virtual bool RemoveRange(Expression<Func<TEntity, bool>> expression)
        {
            _dbContext.RemoveRange(entity.Where(expression));
            return _dbContext.SaveChanges() > 0;
        }

        public virtual Task<int> AddOrUpdateAsync(TEntity entity)
        {
            if (!this.entity.Local.Any(record => EqualityComparer<Guid>.Default.Equals(record.Id,entity.Id)))
                _dbContext.Update(entity);
            return _dbContext.SaveChangesAsync();

        }

        public virtual int AddOrUpdate(TEntity entity)
        {
            if (!this.entity.Local.Any(record => EqualityComparer<Guid>.Default.Equals(record.Id, entity.Id)))
                _dbContext.Update(entity);
            return _dbContext.SaveChanges();
        }

        public async Task<int> SaveAsync()
            => await _dbContext.SaveChangesAsync();
        public int Save()
           => _dbContext.SaveChanges();
    }
}
