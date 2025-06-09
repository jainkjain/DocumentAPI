using Document.Domain.Repository;
using Document.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Document.Infrastructure.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly ApplicationDbContext _context;

        public DocumentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get a single document by ID
        public async Task<Domain.Entities.Document?> GetDocumentByIdAsync(int id)
        {
            return await _context.Documents
                .Where(d => d.Id == id)
                .FirstOrDefaultAsync();
        }

        // Get all documents
        public async Task<IEnumerable<Domain.Entities.Document>> GetAllDocumentsAsync()
        {
            return await _context.Documents.ToListAsync();
        } 

        // Add a new document to the database
        public async Task AddDocumentAsync(Domain.Entities.Document document)
        {
            await _context.Documents.AddAsync(document);
            await _context.SaveChangesAsync();
        }

        // Update an existing document
        public async Task UpdateDocumentAsync(Domain.Entities.Document document)
        {
            _context.Documents.Update(document);
            await _context.SaveChangesAsync();
        }

        // Delete a document by ID
        public async Task DeleteDocumentAsync(int id)
        {
            var document = await _context.Documents.FindAsync(id);
            if (document != null)
            {
                _context.Documents.Remove(document);
                await _context.SaveChangesAsync();
            }
        } 
    }
}
