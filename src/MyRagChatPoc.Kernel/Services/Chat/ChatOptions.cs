using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRagChatPoc.Kernel.Services.Chat
{
    public class ChatOptions
    {
        public const string SectionName = "Chat";

        public class ChatCompletionsOptions 
        {
            public string? ModelId { get; set; }
            public string? ApiKey { get; set; }
            public string? Endpoint { get; set; }
        }

        public ChatCompletionsOptions Completions { get; set; }
    }
}