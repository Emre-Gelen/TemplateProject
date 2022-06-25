using TemplateProject.Common.Bases;

namespace TemplateProject.Api.Domain.Entities
{
    public class EmailConfirmation : BaseEntity
    {
        public string NewEmailAdress{ get; set; }
        public bool EmailConfirmed { get; set; }
    }
}
