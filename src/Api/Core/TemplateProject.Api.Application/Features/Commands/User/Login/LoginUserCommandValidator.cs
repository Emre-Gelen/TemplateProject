using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateProject.Common.Models.CommandModels;

namespace TemplateProject.Api.Application.Features.Commands.User.Login
{
    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommandModel>
    {
        public LoginUserCommandValidator()
        {
            RuleFor(rule => rule.EmailAddress)
                .NotNull()
                .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible)
                .WithMessage("{PropertyName} not a valid email address!");

            RuleFor(rule => rule.Password)
                .NotNull()
                .MinimumLength(8)
                .WithMessage("{PropertyName} should at least be {MinLength} charachters");
        }
    }
}
