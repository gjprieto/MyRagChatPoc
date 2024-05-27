using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRagChatPoc.Kernel.Services.Chat
{
    public interface IChatService
    {
        event EventHandler<string> OnContentReceived;

        Task SendMessageAsync(string message);
    }
}