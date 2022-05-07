using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateProject.Common.Models.CommandModels;

namespace TemplateProject.Api.Application.Features.Commands.User.Create
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommandModel>
    {
        public CreateUserCommandValidator()
        {
        }
    }
}
