using MediatR;

namespace DocumentService.Supervisor.Commands.DocumentCommands
{
    public class DeleteDocumentCommand : IRequest<bool>
    {
        public string Id { get; set; }
    }
}