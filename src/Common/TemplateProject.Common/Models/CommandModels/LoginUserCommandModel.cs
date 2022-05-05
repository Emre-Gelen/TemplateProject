using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateProject.Common.Models.ViewModels;

namespace TemplateProject.Common.Models.CommandModels
{
    public class LoginUserCommandModel : IRequest<LoginUserViewModel>
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }

        public LoginUserCommandModel(string emailAdress,string password)
        {
            EmailAddress = emailAdress;
            Password = password;
        }
        public LoginUserCommandModel()
        {

        }
    }
}
