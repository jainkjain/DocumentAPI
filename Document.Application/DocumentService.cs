using Document.Domain.Repository;

namespace Document.Application
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _documentRepository;

        public DocumentService(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        // Get document by ID
        public async Task<Domain.Entities.Document> GetDocumentByIdAsync(int id)
        {
            return await _documentRepository.GetDocumentByIdAsync(id);
        }

        // Create a new document
        public async Task CreateDocumentAsync(Document.Domain.Entities.Document document)
        {
            await _documentRepository.AddDocumentAsync(document);
        }

        // Update an existing document
        public async Task UpdateDocumentAsync(Document.Domain.Entities.Document document)
        {
            await _documentRepository.UpdateDocumentAsync(document);
        }

        // Delete a document
        public async Task DeleteDocumentAsync(int id)
        {
            Domain.Entities.Document document = await GetDocumentByIdAsync(id);
            if (document == null) return;
            DeleteFile(document.FilePath);
            await _documentRepository.DeleteDocumentAsync(id);
        }

        public async Task<string> UploadFile(MemoryStream ms, string fileName)
        {
            string path = "";
            try
            {
                path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "UploadedFiles"));
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                using (var fileStream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                {
                    await ms.CopyToAsync(fileStream);
                }
                path += "\\" + fileName;
                return path;
            }
            catch (Exception ex)
            {
                throw new Exception("File Copy Failed", ex);
            }
        }

        public bool DeleteFile(string filePath)
        {
            bool deleted = false;
            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    deleted = true;
                }
                return deleted;
            }
            catch (Exception ex)
            {
                throw new Exception("File Deleted Failed", ex);
            }
        }
    }
}
