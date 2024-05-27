using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using MyRagChatPoc.Kernel.Services.KnowledgeBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRagChatPoc.Kernel.Services.Chat
{
    public class ChatService : IChatService
    {
        private readonly IChatCompletionService _chatCompletionService;
        private readonly IKnowledgeBaseService _knowledgeBaseService;
        private readonly ICollection<ChatMessageContent> _messages;

        public ChatService(IChatCompletionService chatCompletionService, IKnowledgeBaseService knowledgeBaseService) 
        {
            _chatCompletionService = chatCompletionService;
            _knowledgeBaseService = knowledgeBaseService;
            _messages = new List<ChatMessageContent>();
        }

        public event EventHandler<string> OnContentReceived;
        private void RaiseOnContentReceived(string message) => OnContentReceived?.Invoke(this, message);

        private string GetPrompt(string message, IEnumerable<string> citations = null) 
        {
            if (citations == null) return message;

            return $@"
                DOCUMENT:
                {string.Join("\n\n", citations)}

                QUESTION:
                {message}

                INSTRUCTIONS:
                Answer the user's QUESTION using the DOCUMENT text above.
                Keep your answer ground in the facts of the DOCUMENT.
                If the DOCUMENT doesn’t contain the facts to answer the QUESTION return 'Sorry, I do not have enough information'";
        }

        private ChatHistory GetChatHistory() 
        {
            return new ChatHistory(_messages);
        }

        private void SaveMessage(AuthorRole role, string message) 
        {            
            var content = new ChatMessageContent { Role = role, Items = new ChatMessageContentItemCollection() };
            
            content.InnerContent = message;
            content.Items.Add(new TextContent { Encoding = Encoding.UTF8, Text = message });

            _messages.Add(content);
        }

        public async Task SendMessageAsync(string message)
        {            
            var answer = new StringBuilder();
            var chatHistory = GetChatHistory();

            var citations = await _knowledgeBaseService.SearchAsync(message, minRelevanceScore: 0.80);
            var prompt = GetPrompt(message, citations.Select(x => x.Text));

            chatHistory.AddUserMessage(prompt);

            await foreach (var content in _chatCompletionService.GetStreamingChatMessageContentsAsync(chatHistory))
            {
                RaiseOnContentReceived(content?.Content);
                answer.Append(content?.Content);
            }

            SaveMessage(AuthorRole.User, message);
            SaveMessage(AuthorRole.Assistant, answer.ToString());
        }
    }
}