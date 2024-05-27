using Microsoft.SemanticKernel.Memory;
using Microsoft.SemanticKernel.Text;
using MyRagChatPoc.Kernel.Services.ChunkGeneration;
using MyRagChatPoc.Kernel.Services.TextExtraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UglyToad.PdfPig.Core.PdfSubpath;

namespace MyRagChatPoc.Kernel.Services.KnowledgeBase
{
#pragma warning disable SKEXP0001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
    public class KnowledgeBaseService : IKnowledgeBaseService
    {
        public const string COLLECTION_DEF_NAME = "test";

        private readonly ISemanticTextMemory _memory;
        private readonly ITextExtractorService _textExtractor;
        private readonly IChunkGenerationService _chunkGenerator;

        public KnowledgeBaseService(ISemanticTextMemory memory, ITextExtractorService textExtractor, IChunkGenerationService chunkGenerator) 
        {
            _memory = memory;
            _textExtractor = textExtractor;
            _chunkGenerator = chunkGenerator;
        }

        public async Task<IReadOnlyDictionary<string, string>> ImportTextAsync(string filename, string text)
        {
            var embeddedChunks = new Dictionary<string, string>();
            var chunks = _chunkGenerator.GetChunks(text);

            for (var i = 0; i < chunks.Count(); i++) 
            {
                var chunk = chunks.ElementAt(i);
                var key = await _memory.SaveInformationAsync(COLLECTION_DEF_NAME, chunk, $"{filename}.{i:000}");
                embeddedChunks.Add(key, chunk);
            }

            return embeddedChunks;
        }

        public async Task<IReadOnlyDictionary<string, string>> ImportPdfAsync(string filename, Stream pdf) 
        {
            var text = _textExtractor.ExtractTextFromPdf(pdf);
            return await ImportTextAsync(filename, text);
        }

        public async Task<IEnumerable<MemoryItem>> SearchAsync(string query, int limit = 10, double minRelevanceScore = 0.75) 
        {
            var results = new List<MemoryQueryResult>();
            var queryResults = _memory.SearchAsync(COLLECTION_DEF_NAME, query, limit: limit, minRelevanceScore: minRelevanceScore);

            await foreach(var queryResult in queryResults) 
            {
                results.Add(queryResult);
            }

            return results.Select(x => new MemoryItem { Score = x.Relevance, Key = x.Metadata.Id, Text = x.Metadata.Text }).OrderByDescending(x => x.Score);
        }
    }
#pragma warning restore SKEXP0001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
}