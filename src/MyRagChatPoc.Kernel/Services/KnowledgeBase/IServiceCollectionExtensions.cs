using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel.Memory;
using Microsoft.SemanticKernel.Connectors.AzureAISearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyRagChatPoc.Kernel.Services.TextExtraction;
using MyRagChatPoc.Kernel.Services.ChunkGeneration;
using Microsoft.Extensions.Options;

namespace MyRagChatPoc.Kernel.Services.KnowledgeBase
{
#pragma warning disable SKEXP0010 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed
#pragma warning disable SKEXP0001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
#pragma warning disable SKEXP0020 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddKnowledgeBaseService(this IServiceCollection services) 
        {
            services.AddOptions<KnowledgeBaseOptions>().BindConfiguration(KnowledgeBaseOptions.SectionName);

            services.AddSingleton<ISemanticTextMemory>((provider) => {
                var options = provider.GetRequiredService<IOptions<KnowledgeBaseOptions>>();
                var config = options.Value;

                return new MemoryBuilder()
                    .WithAzureOpenAITextEmbeddingGeneration(config.Embeddings.ModelId, config.Embeddings.Endpoint, config.Embeddings.ApiKey)
                    .WithMemoryStore(new AzureAISearchMemoryStore(config.Search.Endpoint, config.Search.ApiKey))
                    .Build();
            });

            services.AddSingleton<ITextExtractorService, TextExtractorService>()
                .AddSingleton<IChunkGenerationService, ChunkGenerationService>()
                .AddSingleton<IKnowledgeBaseService, KnowledgeBaseService>();

            return services;
        }
    }
#pragma warning restore SKEXP0020 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
#pragma warning restore SKEXP0001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
#pragma warning restore SKEXP0010 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
}