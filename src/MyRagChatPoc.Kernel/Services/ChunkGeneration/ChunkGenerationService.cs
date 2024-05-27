using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ML.Tokenizers;
using Microsoft.SemanticKernel.Text;
using UglyToad.PdfPig.Tokenization;

namespace MyRagChatPoc.Kernel.Services.ChunkGeneration
{
#pragma warning disable SKEXP0050 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
    public class ChunkGenerationService : IChunkGenerationService
    {
        public ChunkGenerationService() 
        {
        }

        public IEnumerable<string> GetChunks(string text, int maxTokensPerLine = 75, int maxTokensPerParagraph = 500, string? chunkHeader = null)
        {
            var lines = TextChunker.SplitPlainTextLines(text, maxTokensPerLine: maxTokensPerLine);
            return TextChunker.SplitPlainTextParagraphs(lines, maxTokensPerParagraph: maxTokensPerParagraph, chunkHeader: chunkHeader);
        }
    }
#pragma warning restore SKEXP0050 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
}
