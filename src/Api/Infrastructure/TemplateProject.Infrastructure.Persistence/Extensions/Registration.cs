using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using TemplateProject.Infrastructure.Persistence.Context;

namespace TemplateProject.Infrastructure.Persistence.Extensions
{
    public static class Registration
    {
        public static IServiceCollection AddInfrastructureRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TemplateProjectContext>(conf =>
            {
                var connectionString = configuration["TemplateProjectDbConnectionString"].ToString();
                conf.UseSqlServer(connectionString);
            
            });

            return services;
        }
    }
}
