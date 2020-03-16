using System.Collections.Generic;
using DocumentService.Supervisor.Models.Response;
using MediatR;

namespace DocumentService.Supervisor.Commands.DocumentCommands
{
    public class UpdateDocumentCommand : IRequest<bool>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int TypeId { get; set; }
        public ICollection<CommentRequestModel> Items { get; set; }
    }

    public class CommentRequestModel
    {
        public string Description { get; set; }
        
        public string Content { get; set; }
    }
}