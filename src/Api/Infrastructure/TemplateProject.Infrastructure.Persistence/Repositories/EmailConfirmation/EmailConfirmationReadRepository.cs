using TemplateProject.Api.Domain.Entities;
using TemplateProject.Api.Application.Repositories;
using TemplateProject.Infrastructure.Persistence.Context;

namespace TemplateProject.Infrastructure.Persistence.Repositories
{
    public class EmailConfirmationReadRepository : ReadRepository<EmailConfirmation>, IEmailConfirmationReadRepository
    {
        public EmailConfirmationReadRepository(TemplateProjectContext dbContext) : base(dbContext) { }
    }
}
