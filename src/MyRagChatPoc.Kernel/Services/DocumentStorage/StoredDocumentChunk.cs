using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRagChatPoc.Kernel.Services.DocumentStorage
{
    public class StoredDocumentChunk
    {
        public string DocumentId { get; set; }
        public string ChunkId { get; set; }
        public string Key { get; set; }
        public string Text { get; set; }
    }
}