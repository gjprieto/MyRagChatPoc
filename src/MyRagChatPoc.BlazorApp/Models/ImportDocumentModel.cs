namespace MyRagChatPoc.BlazorApp.Models
{
    public class ImportDocumentModel
    {
        public string Id { get; set; }
        public string Filename { get; set; }
        public Stream? Blob { get; set; }
        public ICollection<string> Tags { get; set; } = new List<string>();
    }
}