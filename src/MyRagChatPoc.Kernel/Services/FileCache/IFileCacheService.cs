using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRagChatPoc.Kernel.Services.FileCache
{
    public interface IFileCacheService
    {
        void StoreFile(string key, Stream stream);
        Stream? GetFile(string key);
        void RemoveFile(string key);
    }
}