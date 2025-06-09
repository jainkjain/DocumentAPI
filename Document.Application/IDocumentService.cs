using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document.Application
{
    public interface IDocumentService
    {
        Task<Domain.Entities.Document> GetDocumentByIdAsync(int id);
        Task CreateDocumentAsync(Domain.Entities.Document document);
        Task UpdateDocumentAsync(Domain.Entities.Document document);
        Task DeleteDocumentAsync(int id);
        Task<string> UploadFile(MemoryStream ms, string fileName);
        bool DeleteFile(string filePath);
    }
}
