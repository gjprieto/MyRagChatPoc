using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRagChatPoc.Kernel.Services.KnowledgeBase
{
    public class MemoryItem
    {
        public double Score { get; set; }
        public string Key { get; set; }
        public string Text { get; set; }
    }
}