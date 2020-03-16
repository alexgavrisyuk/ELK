using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DocumentService.Infrastructure.Interfaces;
using DocumentService.Supervisor.Models.Response;
using DocumentService.Supervisor.Queries.DocumentQueries;
using Mapster;
using MediatR;

namespace DocumentService.Supervisor.QueriesHandler
{
    public class DocumentQueriesHandler :
        IRequestHandler<GetAllDocumentsQuery, IEnumerable<DocumentResponseModel>>,
        IRequestHandler<GetDocumentByNameQuery, DocumentResponseModel>
    {
        private readonly IDocumentRepository _documentRepository;

        public DocumentQueriesHandler(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        public async Task<IEnumerable<DocumentResponseModel>> Handle(GetAllDocumentsQuery request,
            CancellationToken cancellationToken)
        {
            var documents = await _documentRepository.GetAllDocuments();
            var responseModels = documents.Adapt<ICollection<DocumentResponseModel>>();
            return responseModels;
        }

        public async Task<DocumentResponseModel> Handle(GetDocumentByNameQuery request, CancellationToken cancellationToken)
        {
            var document = await _documentRepository.GetDocument(request.Name);
            var responseModel = document.Adapt<DocumentResponseModel>();
            return responseModel;
        }
    }
}