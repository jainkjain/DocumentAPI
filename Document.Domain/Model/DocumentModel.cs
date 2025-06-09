using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document.Domain.Model
{
    public class DocumentModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string FilePath { get; set; }  // File location (URL or path in storage)  
        public string FileType { get; set; }
        public long FileSize { get; set; }
    }
}
