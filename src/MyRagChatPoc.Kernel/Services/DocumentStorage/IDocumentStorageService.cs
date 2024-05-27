using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRagChatPoc.Kernel.Services.DocumentStorage
{
    public interface IDocumentStorageService
    {
        Task<IEnumerable<StoredDocument>> GetDocumentsAsync();
        Task<string> UploadDocumentAsync(string path, Stream document);
        Task<Stream> DownloadDocumentAsync(string path);
        Task DeleteDocumentAsync(string path);
        Task DeleteAsync();
    }
}