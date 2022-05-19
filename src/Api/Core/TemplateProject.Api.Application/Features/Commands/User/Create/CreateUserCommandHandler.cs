using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateProject.Api.Application.Repositories;
using TemplateProject.Common.Infrastructure.Exceptions;
using TemplateProject.Common.Models.CommandModels;
using TemplateProject.Api.Domain.Entities;
using TemplateProject.Common.Infrastructure;
using TemplateProject.Common.Events.User;
using TemplateProject.Common;

namespace TemplateProject.Api.Application.Features.Commands.User.Create
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandModel, Guid>
    {
        private readonly IUserWriteRepository _userWriteRepository;
        private readonly IUserReadRepository _userReadRepository;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(IUserWriteRepository userWriteRepository, IUserReadRepository userReadRepository, IMapper mapper)
        {
            _userWriteRepository = userWriteRepository;
            _userReadRepository = userReadRepository;
            _mapper = mapper;
        }

        async Task<Guid> IRequestHandler<CreateUserCommandModel, Guid>.Handle(CreateUserCommandModel request, CancellationToken cancellationToken)
        {
            var existUser = await _userReadRepository.GetSingleAsync(user => user.EmailAddress == request.EmailAddress);

            if (existUser is not null)
                throw new DatabaseValidationException("User already exists!");

            var newUser = _mapper.Map<Domain.Entities.User>(request);

            var rows = await _userWriteRepository.AddAsync(newUser);

            if (rows > 0)
            {
                var @event = new UserEmailChangedEvent()
                {
                    OldEmailAdress = null,
                    NewEmailAdress = newUser.EmailAddress,
                };
                QueueFactory.SendMessageToExchange(exchangeName: Constants.UserExchangeName,
                                                   exchangeType: Constants.DefaultExchangeType,
                                                   queueName: Constants.UserEmailChangedQueueName,
                                                   obj: @event);
            }

            return newUser.Id;
        }
    }
}
