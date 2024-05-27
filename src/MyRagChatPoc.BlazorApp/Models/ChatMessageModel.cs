namespace MyRagChatPoc.BlazorApp.Models
{
    public enum ChatMessageType
    {
        Assistant = 0,
        User = 1
    }

    public class ChatMessageModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public ChatMessageType Type { get; set; } = ChatMessageType.Assistant;
        public DateTime Date { get; set; }
        public string Message { get; set; }
    }
}