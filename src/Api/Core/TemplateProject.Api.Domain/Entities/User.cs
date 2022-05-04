﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateProject.Common.Bases;

namespace TemplateProject.Api.Domain.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAdress { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool EmailConfirmed { get; set; }
    }
}
