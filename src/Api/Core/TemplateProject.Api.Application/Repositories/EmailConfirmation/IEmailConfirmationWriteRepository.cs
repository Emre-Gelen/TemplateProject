using System.Threading.Tasks;
using TemplateProject.Api.Domain.Entities;

namespace TemplateProject.Api.Application.Repositories
{
    public interface IEmailConfirmationWriteRepository : IWriteRepository<EmailConfirmation>
    {
    }
}
