using System;
using System.Threading;
using System.Threading.Tasks;
using DocumentService.Domain.Models;
using DocumentService.Infrastructure.Interfaces;
using DocumentService.Supervisor.Commands.DocumentCommands;
using DocumentService.Supervisor.Models.Response;
using Mapster;
using MediatR;

namespace DocumentService.Supervisor.CommandsHandlers
{
    public class DocumentCommandsHandler : 
        IRequestHandler<CreateDocumentCommand, DocumentResponseModel>,
        IRequestHandler<UpdateDocumentCommand, bool>,
        IRequestHandler<DeleteDocumentCommand, bool>
    {
        private readonly IDocumentRepository _documentRepository;

        public DocumentCommandsHandler(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        public async Task<DocumentResponseModel> Handle(CreateDocumentCommand request, CancellationToken cancellationToken)
        {
            var document = new Document(request.Name, request.Description, request.TypeId);
            await _documentRepository.Create(document);

            var responseModel = document.Adapt<DocumentResponseModel>();
            return responseModel;
        }

        public async Task<bool> Handle(UpdateDocumentCommand request, CancellationToken cancellationToken)
        {
            var document = await _documentRepository.GetDocument(request.Id);
            if (document == null)
            {
                throw new Exception($"Document with id {request.Id} does not exist");
            }
            document.Update(request.Name, request.Description);
            document.Comments.Clear();
            foreach (var item in request.Items)
            {
                document.AddComment(item.Description, item.Content);
            }
            
            return await _documentRepository.Update(document);
        }

        public async Task<bool> Handle(DeleteDocumentCommand request, CancellationToken cancellationToken)
        {
            var document = await _documentRepository.GetDocument(request.Id);
            if (document == null)
            {
                throw new Exception($"Document with id {request.Id} does not exist");
            }
            
            return await _documentRepository.Delete(request.Id);            

        }
    }
}