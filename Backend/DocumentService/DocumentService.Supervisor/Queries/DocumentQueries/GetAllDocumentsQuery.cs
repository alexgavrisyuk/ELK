using System.Collections.Generic;
using DocumentService.Supervisor.Models.Response;
using MediatR;

namespace DocumentService.Supervisor.Queries.DocumentQueries
{
    public class GetAllDocumentsQuery : IRequest<IEnumerable<DocumentResponseModel>>
    {
        
    }
}