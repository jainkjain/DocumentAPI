using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document.Domain.Entities
{
    [Table("Document")]
    public class Document
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string FilePath { get; set; }  // File location (URL or path in storage)
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public string FileType { get; set; }
        public long FileSize { get; set; } // file size in bytes
    }
}
