using AutoMapper;
using MediatR;
using System.Text;
using TemplateProject.Api.Application.Repositories;
using TemplateProject.Common.Models.CommandModels;
using TemplateProject.Common.Models.ViewModels;
using Microsoft.Extensions.Configuration;
using TemplateProject.Common.Infrastructure.Exceptions;
using TemplateProject.Common.Encryption;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace TemplateProject.Api.Application.Features.Commands.User.Login
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandModel, LoginUserViewModel>
    {
        private readonly IUserWriteRepository _userWriteRepository;
        private readonly IUserReadRepository _userReadRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public LoginUserCommandHandler(IUserWriteRepository userWriteRepository, IUserReadRepository userReadRepository, IMapper mapper, IConfiguration configuration)
        {
            _userWriteRepository = userWriteRepository;
            _userReadRepository = userReadRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<LoginUserViewModel> Handle(LoginUserCommandModel request, CancellationToken cancellationToken)
        {
            var dbUser = await _userReadRepository.GetSingleAsync(user => user.EmailAdress == request.EmailAddress);

            if (dbUser == null)
                throw new DatabaseValidationException("User not found!");

            var userPassword = PasswordEncryption.Encrypt(request.Password);

            if (dbUser.Password != userPassword)
                throw new DatabaseValidationException("Password is wrong!");

            if (!dbUser.EmailConfirmed)
                throw new DatabaseValidationException("Email address is not confirmed yet!");

            var result = _mapper.Map<LoginUserViewModel>(dbUser);

            var claims = new Claim[] { 
                new Claim(ClaimTypes.NameIdentifier, dbUser.Id.ToString()),
                new Claim(ClaimTypes.Email, dbUser.EmailAdress),
                new Claim(ClaimTypes.Name, dbUser.UserName),
                new Claim(ClaimTypes.GivenName, dbUser.FirstName),
                new Claim(ClaimTypes.Surname, dbUser.LastName),
            };

            result.JWTToken = GenerateToken(claims);

            return result;
        }

        private string GenerateToken(Claim[] claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Security:SecurityKey"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expireDate = DateTime.Now.AddDays(1);

            var token = new JwtSecurityToken(claims: claims
                , expires: expireDate
                , signingCredentials: credentials
                , notBefore: DateTime.Now);

            return new JwtSecurityTokenHandler().WriteToken(token); 

        }
    }
}
