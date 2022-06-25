using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateProject.Common.Models.CommandModels;
public class ChangeUserPasswordCommandModel : IRequest<bool>
{
    public Guid? UserId { get; set; }
    public string OldPassword{ get; set; }
    public string NewPassword { get; set; }

    public ChangeUserPasswordCommandModel(Guid? userId, string oldPassword, string newPassword)
    {
        UserId = userId;
        OldPassword = oldPassword;
        NewPassword = newPassword;
    }
}
