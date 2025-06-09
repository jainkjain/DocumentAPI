using Document.Application;
using Microsoft.AspNetCore.Mvc;

namespace Document.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _documentService;

        public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        // Create (Upload document metadata)         
        [HttpPost("create")]
        public async Task<IActionResult> CreateDocument(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            var content = new MemoryStream();
            await file.CopyToAsync(content);

            string fullPath = await _documentService.UploadFile(content, file.FileName);

            var document = new Document.Domain.Entities.Document
            {
                Name = file.FileName,
                Description = file.FileName + "_" + file.Length,
                FilePath = fullPath, // Path from the storage service
                FileSize = file.Length,
                FileType = file.FileName.Split(".").LastOrDefault() ?? null,
                CreatedAt = DateTime.UtcNow
            };

            await _documentService.CreateDocumentAsync(document);
            return CreatedAtAction(nameof(GetDocumentById), new { id = document.Id }, document);
        }

        // Read (Get document metadata by ID)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDocumentById(int id)
        {
            var document = await _documentService.GetDocumentByIdAsync(id);
            if (document == null)
            {
                return NotFound();
            }
            return Ok(document);
        }

        // Update (Modify document metadata)
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDocument(int id, [FromBody] IFormFile file)
        {
            var document = await _documentService.GetDocumentByIdAsync(id);
            if (document == null)
            {
                return BadRequest();
            }

            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            if (_documentService.DeleteFile(document.FilePath))
            {
                return NotFound();
            }

            var content = new MemoryStream();
            await file.CopyToAsync(content);

            string fullPath = await _documentService.UploadFile(content, file.FileName);

            document.Name = file.FileName;
            document.Description = file.FileName + "_" + file.Length;
            document.FileSize = file.Length;
            document.FileType = file.FileName.Split(".").LastOrDefault() ?? null;
            document.FilePath = fullPath; // Path from the storage service

            await _documentService.UpdateDocumentAsync(document);
            return NoContent();
        }

        // Delete (Delete document by id)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocument(int id)
        {
            await _documentService.DeleteDocumentAsync(id);
            return NoContent();
        }
    }
}
