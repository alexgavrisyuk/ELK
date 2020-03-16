using DocumentService.Supervisor.Models.Response;
using MediatR;

namespace DocumentService.Supervisor.Queries.DocumentQueries
{
    public class GetDocumentByNameQuery : IRequest<DocumentResponseModel>
    {
        public string Name { get; set; }
    }
}