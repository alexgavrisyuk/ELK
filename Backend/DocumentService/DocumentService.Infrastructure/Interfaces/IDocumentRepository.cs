using System.Collections.Generic;
using System.Threading.Tasks;
using DocumentService.Domain.Models;

namespace DocumentService.Infrastructure.Interfaces
{
    public interface IDocumentRepository
    {
        Task<IEnumerable<Document>> GetAllDocuments();
        Task<Document> GetDocument(string id);
        Task Create(Document document);
        Task<bool> Update(Document document);
        Task<bool> Delete(string name);
    }
}