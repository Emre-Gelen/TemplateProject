using TemplateProject.Api.Application.Repositories;
using TemplateProject.Api.Domain.Entities;
using TemplateProject.Infrastructure.Persistence.Context;

namespace TemplateProject.Infrastructure.Persistence.Repositories
{
    public class UserWriteRepository : WriteRepository<User>,IUserWriteRepository
    {
        public UserWriteRepository(TemplateProjectContext dbContext) : base(dbContext){}
    }
}
