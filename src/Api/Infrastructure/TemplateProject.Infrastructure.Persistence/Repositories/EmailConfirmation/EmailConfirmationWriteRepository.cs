using TemplateProject.Api.Domain.Entities;
using TemplateProject.Api.Application.Repositories;
using TemplateProject.Infrastructure.Persistence.Context;

namespace TemplateProject.Infrastructure.Persistence.Repositories
{
    public class EmailConfirmationWriteRepository : WriteRepository<EmailConfirmation>, IEmailConfirmationWriteRepository
    {
        public EmailConfirmationWriteRepository(TemplateProjectContext dbContext) : base(dbContext) { }
    }
}
