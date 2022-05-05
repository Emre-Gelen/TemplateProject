using Microsoft.EntityFrameworkCore;
using TemplateProject.Api.Application.Repositories;
using TemplateProject.Api.Domain.Entities;

namespace TemplateProject.Infrastructure.Persistence.Repositories
{
    public class UserWriteRepository : WriteRepository<User>,IUserWriteRepository
    {
        public UserWriteRepository(DbContext dbContext) : base(dbContext){}
    }
}
