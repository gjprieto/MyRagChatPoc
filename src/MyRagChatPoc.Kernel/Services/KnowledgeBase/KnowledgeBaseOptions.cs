using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRagChatPoc.Kernel.Services.KnowledgeBase
{
    public class KnowledgeBaseOptions
    {
        public const string SectionName = "KnowledgeBase";

        public class KnowledgeBaseEmbeddingsOptions
        {
            public string? ModelId { get; set; }
            public string? ApiKey { get; set; }
            public string? Endpoint { get; set; }
        }

        public class KnowledgeBaseSearchOptions
        {
            public string? ApiKey { get; set; }
            public string? Endpoint { get; set; }
        }

        public KnowledgeBaseEmbeddingsOptions Embeddings { get; set; }
        public KnowledgeBaseSearchOptions Search { get; set; }
    }
}