using MediatR;

namespace TemplateProject.Common.Models.CommandModels
{
    public class ConfirmEmailCommandModel : IRequest<bool>
    {
        public Guid ConfirmationId { get; set; }
    }
}
