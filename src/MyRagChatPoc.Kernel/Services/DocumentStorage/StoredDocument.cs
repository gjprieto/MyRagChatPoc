using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MyRagChatPoc.Kernel.Services.DocumentStorage
{
    public class StoredDocument
    {
        public string Id { get; set; }
        public string IndexId { get; set; }
        public string Filename { get; set; }
        public DateTime UploadedOn { get; set; }

        [IgnoreDataMember]
        public IEnumerable<StoredDocumentChunk> Chunks { get; set; }
    }
}