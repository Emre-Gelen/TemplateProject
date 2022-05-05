using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateProject.Api.Domain.Entities;

namespace TemplateProject.Api.Application.Repositories
{
    public interface IUserReadRepository : IReadRepository<User>
    {
    }
}
