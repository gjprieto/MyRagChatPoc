using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRagChatPoc.Kernel.Services.ChunkGeneration
{
    public interface IChunkGenerationService
    {
        IEnumerable<string> GetChunks(string text, int maxTokensPerLine = 40, int maxTokensPerParagraph = 150, string? chunkHeader = null);
    }
}