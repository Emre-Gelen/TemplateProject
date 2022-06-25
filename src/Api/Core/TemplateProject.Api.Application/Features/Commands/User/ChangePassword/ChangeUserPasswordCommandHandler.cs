using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TemplateProject.Api.Application.Repositories;
using TemplateProject.Common.Encryption;
using TemplateProject.Common.Infrastructure.Exceptions;
using TemplateProject.Common.Models.CommandModels;

namespace TemplateProject.Api.Application.Features.Commands.User.ChangePassword
{
    public class ChangeUserPasswordCommandHandler : IRequestHandler<ChangeUserPasswordCommandModel, bool>
    {
        private readonly IUserWriteRepository _userWriteRepository;
        private readonly IUserReadRepository _userReadRepository;

        public ChangeUserPasswordCommandHandler(IUserWriteRepository userWriteRepository, IUserReadRepository userReadRepository, IMapper mapper)
        {
            _userWriteRepository = userWriteRepository;
            _userReadRepository = userReadRepository;
        }

        public async Task<bool> Handle(ChangeUserPasswordCommandModel request, CancellationToken cancellationToken)
        {
            if (!request.UserId.HasValue)
                throw new ArgumentNullException(nameof(request.UserId));

            var dbUser = await _userReadRepository.GetByIdAsync(request.UserId.Value);

            if (dbUser is null)
                throw new DatabaseValidationException("User not found!");

            var enchryptedPassword = PasswordEncryption.Encrypt(request.OldPassword);
            if (dbUser.Password != enchryptedPassword)
                throw new DatabaseValidationException("Old password is wrong!");

            dbUser.Password = enchryptedPassword;

            await _userWriteRepository.UpdateAsync(dbUser);

            return true;
        }
    }
}
