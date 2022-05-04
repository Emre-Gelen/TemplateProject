using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TemplateProject.Api.Domain.Entities;
using TemplateProject.Common.Bases;

namespace TemplateProject.Infrastructure.Persistence.Context
{
    public class TemplateProjectContext : DbContext
    {
        public TemplateProjectContext()
        {


        }
        public TemplateProjectContext(DbContextOptions options) : base(options){}

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = "Server=.;Database=TemplateProject;Trusted_Connection=True;";
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        public override int SaveChanges()
        {
            OnBeforeSave();
            return base.SaveChanges();
        }
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSave();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            OnBeforeSave();
            return SaveChangesAsync(acceptAllChangesOnSuccess: true, cancellationToken);
        }
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            OnBeforeSave();
            return SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
        private void OnBeforeSave()
        {
            var addedEntities = ChangeTracker.Entries()
                .Where(entity => entity.State == EntityState.Added)
                .Select(entity => (BaseEntity)entity.Entity);

            PrepareAddedEntities(addedEntities);
        }
        private void PrepareAddedEntities(IEnumerable<BaseEntity> baseEntities)
        {
            foreach (var currentEntity in baseEntities)
            {
                currentEntity.CreateDate = DateTime.Now;
            }
        }
    }
}
