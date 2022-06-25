using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateProject.Api.Application.Repositories;
using TemplateProject.Common.Infrastructure.Exceptions;
using TemplateProject.Common.Models.CommandModels;

namespace TemplateProject.Api.Application.Features.Commands.User.ConfirmEmail
{
    public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommandModel, bool>
    {
        private readonly IEmailConfirmationWriteRepository _emailConfirmationWriteRepository;
        private readonly IEmailConfirmationReadRepository _emailConfirmationReadRepository;
        private readonly IUserWriteRepository _userWriteRepository;
        private readonly IUserReadRepository _userReadRepository;

        public ConfirmEmailCommandHandler(IEmailConfirmationWriteRepository emailConfirmationWriteRepository, IEmailConfirmationReadRepository emailConfirmationReadRepository, IUserWriteRepository userWriteRepository, IUserReadRepository userReadRepository) : this(emailConfirmationWriteRepository, emailConfirmationReadRepository)
        {
            _userWriteRepository = userWriteRepository;
            _userReadRepository = userReadRepository;
        }

        public async Task<bool> Handle(ConfirmEmailCommandModel request, CancellationToken cancellationToken)
        {
            var confirmation = await _emailConfirmationReadRepository.GetByIdAsync(request.ConfirmationId);
            if (confirmation is null)
                throw new DatabaseValidationException("Confirmation not found!");

            var dbUser = await _userReadRepository.GetSingleAsync(user => user.EmailAddress == confirmation.NewEmailAdress && !user.EmailConfirmed);

            if (dbUser is null)
                throw new DatabaseValidationException("Not confirmed user not found with this email!");

            dbUser.EmailConfirmed = true;
            await _userWriteRepository.UpdateAsync(dbUser);

            return true;
        }
    }
}
