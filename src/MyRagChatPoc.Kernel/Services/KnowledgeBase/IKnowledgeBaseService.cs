using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRagChatPoc.Kernel.Services.KnowledgeBase
{
    public interface IKnowledgeBaseService
    {
        Task<IReadOnlyDictionary<string, string>> ImportTextAsync(string filename, string text);
        Task<IReadOnlyDictionary<string, string>> ImportPdfAsync(string filename, Stream pdf);
        Task<IEnumerable<MemoryItem>> SearchAsync(string query, int limit = 10, double minRelevanceScore = 0.75);
    }
}