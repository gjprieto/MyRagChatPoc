using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyRagChatPoc.Kernel.Tools;
using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel.Connectors.AzureAISearch;
using Microsoft.SemanticKernel.Memory;
using MyRagChatPoc.Kernel.Services.KnowledgeBase;

namespace MyRagChatPoc.Kernel.Services.Chat
{
    public static class IServiceCollectionExtensions
    {
        private static IChatCompletionService GetChatCompletionService(string modelId, string apiKey, string endpoint)
        {
            IChatCompletionService chatGenCode;

            if (endpoint.Contains("openai.azure.com", StringComparison.InvariantCultureIgnoreCase))
            {
                chatGenCode = new AzureOpenAIChatCompletionService(modelId, endpoint, apiKey);
            }
            else
            {
                var httpClient = new System.Net.Http.HttpClient(new CustomHttpMessageHandler(endpoint));
                chatGenCode = new OpenAIChatCompletionService(modelId, apiKey, httpClient: httpClient);
            }

            return chatGenCode;
        }

        public static IServiceCollection AddChatService(this IServiceCollection services) 
        {
            services.AddOptions<ChatOptions>().BindConfiguration(ChatOptions.SectionName);

            services.AddSingleton<IChatCompletionService>((provider) => {
                var options = provider.GetRequiredService<IOptions<ChatOptions>>();
                var config = options.Value;

                return GetChatCompletionService(config.Completions.ModelId, config.Completions.ApiKey, config.Completions.Endpoint);
            });

            services.AddSingleton<IChatService, ChatService>();

            return services;
        }
    }
}