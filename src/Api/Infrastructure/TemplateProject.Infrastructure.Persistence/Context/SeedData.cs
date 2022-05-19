using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateProject.Api.Domain.Entities;
using TemplateProject.Common.Encryption;

namespace TemplateProject.Infrastructure.Persistence.Context
{
    internal class SeedData
    {
        private static List<User> GetUsers()
        {
            var result = new Faker<User>("tr")
                .RuleFor(prop => prop.Id, prop => Guid.NewGuid())
                .RuleFor(prop => prop.CreateDate, prop => prop.Date.Between(DateTime.Now.AddDays(-250), DateTime.Now))
                .RuleFor(prop => prop.FirstName, prop => prop.Person.FirstName)
                .RuleFor(prop => prop.LastName, prop => prop.Person.LastName)
                .RuleFor(prop => prop.EmailAddress, prop => prop.Internet.Email())
                .RuleFor(prop => prop.UserName, prop => prop.Internet.UserName())
                .RuleFor(prop => prop.Password, prop => PasswordEncryption.Encrypt(prop.Internet.Password()))
                .RuleFor(prop => prop.EmailConfirmed, prop => prop.PickRandom(true, false))
                .Generate(100);
            return result;
        }
        public async Task SeedAsync(IConfiguration configuration)
        {
            var dbContextBuilder = new DbContextOptionsBuilder();
            dbContextBuilder.UseSqlServer(configuration["TemplateProjectDbConnectionString"]);

            var context = new TemplateProjectContext(dbContextBuilder.Options);

            var users = GetUsers();

            await context.Users.AddRangeAsync(users);

            context.SaveChanges();
        }
    }
}
