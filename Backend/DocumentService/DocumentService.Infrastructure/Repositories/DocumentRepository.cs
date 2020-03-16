using System.Collections.Generic;
using System.Threading.Tasks;
using DocumentService.Domain.Models;
using DocumentService.Infrastructure.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DocumentService.Infrastructure.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly DocumentContext _documentContext;

        public DocumentRepository(DocumentContext documentContext)
        {
            _documentContext = documentContext;
        }
        
        public async Task<IEnumerable<Document>> GetAllDocuments()
        {
            return await _documentContext.Documents.Find(d => true).ToListAsync();
        }

        public async Task<Document> GetDocument(string id)
        {
            return await _documentContext.Documents.Find(d => d.Id.Equals(new ObjectId(id))).FirstOrDefaultAsync();
        }

        public async Task Create(Document document)
        {
            await _documentContext.Documents.InsertOneAsync(document);           
        }

        public async Task<bool> Update(Document document)
        {
            var updateResult =
                await _documentContext.Documents.ReplaceOneAsync(filter: g => g.Id == document.Id,
                    replacement: document);
            return updateResult.IsAcknowledged
                   && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> Delete(string name)
        {
            var filter = Builders<Document>.Filter.Eq(m => m.Name, name);
            var deleteResult = await _documentContext
                .Documents
                .DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged
                   && deleteResult.DeletedCount > 0;
        }
    }
}