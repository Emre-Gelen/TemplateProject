using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;

namespace TemplateProject.Api.Application.Extensions
{
    public static class Registration
    {
        public static IServiceCollection AddApplicationRegistration(this IServiceCollection services)
        {
            var assemblyReferences = Assembly.GetExecutingAssembly();

            services.AddMediatR(assemblyReferences);
            services.AddAutoMapper(assemblyReferences);
            services.AddValidatorsFromAssembly(assemblyReferences);

            return services;
        }
    }
}
