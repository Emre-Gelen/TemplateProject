using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateProject.Api.Application.Repositories;
using TemplateProject.Common;
using TemplateProject.Common.Events.User;
using TemplateProject.Common.Infrastructure;
using TemplateProject.Common.Infrastructure.Exceptions;
using TemplateProject.Common.Models.CommandModels;

namespace TemplateProject.Api.Application.Features.Commands.User.Update
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommandModel, Guid>
    {
        private readonly IUserWriteRepository _userWriteRepository;
        private readonly IUserReadRepository _userReadRepository;
        private readonly IMapper _mapper;

        public UpdateUserCommandHandler(IUserWriteRepository userWriteRepository, IUserReadRepository userReadRepository, IMapper mapper)
        {
            _userWriteRepository = userWriteRepository;
            _userReadRepository = userReadRepository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(UpdateUserCommandModel request, CancellationToken cancellationToken)
        {
            var dbUser = await _userReadRepository.GetByIdAsync(request.Id);

            if (dbUser is null)
                throw new DatabaseValidationException("User not found!");

            var dbEmilAdress = dbUser.EmailAddress;
            var emailChanged = string.CompareOrdinal(dbEmilAdress, request.EmailAddress) != 0;

            _mapper.Map(request, dbUser);

            var rows = await _userWriteRepository.UpdateAsync(dbUser);

            if (rows > 0 && emailChanged)
            {
                var @event = new UserEmailChangedEvent()
                {
                    OldEmailAdress = dbEmilAdress,
                    NewEmailAdress = dbUser.EmailAddress,
                };
                QueueFactory.SendMessageToExchange(exchangeName: Constants.UserExchangeName,
                                                   exchangeType: Constants.DefaultExchangeType,
                                                   queueName: Constants.UserEmailChangedQueueName,
                                                   obj: @event);
                dbUser.EmailConfirmed = false;
                await _userWriteRepository.UpdateAsync(dbUser);
            }

            return dbUser.Id;
        }
    }
}
