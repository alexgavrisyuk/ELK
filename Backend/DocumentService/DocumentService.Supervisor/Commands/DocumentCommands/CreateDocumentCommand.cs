using System.Collections.Generic;
using DocumentService.Supervisor.Models.Response;
using MediatR;

namespace DocumentService.Supervisor.Commands.DocumentCommands
{
    public class CreateDocumentCommand : IRequest<DocumentResponseModel>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int TypeId { get; set; }
    }
}