using Microsoft.EntityFrameworkCore;
using TemplateProject.Api.Application.Repositories;
using TemplateProject.Api.Domain.Entities;

namespace TemplateProject.Infrastructure.Persistence.Repositories
{
    public class UserReadRepository : ReadRepository<User>,IUserReadRepository
    {
        public UserReadRepository(DbContext dbContext) : base(dbContext) { }
    }
}
