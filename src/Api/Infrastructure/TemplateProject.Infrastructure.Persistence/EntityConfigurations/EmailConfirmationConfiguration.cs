using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TemplateProject.Api.Domain.Entities;

namespace TemplateProject.Infrastructure.Persistence.EntityConfigurations
{
    public class EmailConfirmationConfiguration : BaseEntityConfiguration<EmailConfirmation>
    {
        public override void Configure(EntityTypeBuilder<EmailConfirmation> builder)
        {
            base.Configure(builder);
        }
    }
}
