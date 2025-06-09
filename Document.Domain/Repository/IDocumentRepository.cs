using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document.Domain.Repository
{
    public interface IDocumentRepository
    {
        Task<Domain.Entities.Document> GetDocumentByIdAsync(int id);
        Task<IEnumerable<Domain.Entities.Document>> GetAllDocumentsAsync();
        Task AddDocumentAsync(Domain.Entities.Document document);
        Task UpdateDocumentAsync(Domain.Entities.Document document);
        Task DeleteDocumentAsync(int id);
    } 
}
