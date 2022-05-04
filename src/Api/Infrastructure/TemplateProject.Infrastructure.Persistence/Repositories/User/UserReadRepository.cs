using TemplateProject.Api.Application.Repositories;
using TemplateProject.Api.Domain.Entities;
using TemplateProject.Infrastructure.Persistence.Context;

namespace TemplateProject.Infrastructure.Persistence.Repositories
{
    public class UserReadRepository : ReadRepository<User>,IUserReadRepository
    {
        public UserReadRepository(TemplateProjectContext dbContext) : base(dbContext) { }
    }
}
