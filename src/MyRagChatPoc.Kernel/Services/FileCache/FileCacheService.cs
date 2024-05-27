using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRagChatPoc.Kernel.Services.FileCache
{
    public class FileCacheService : IFileCacheService
    {
        public Dictionary<string, Stream> Files { get; set; } = new Dictionary<string, Stream>();

        public Stream? GetFile(string key)
        {
            if (string.IsNullOrEmpty(key) || !Files.ContainsKey(key)) return null;
            return Files[key];
        }

        public void RemoveFile(string key)
        {
            if (string.IsNullOrEmpty(key) || !Files.ContainsKey(key)) return;
            var stream = Files[key];

            if (stream != null)
            {
                Files[key].Close();
                Files.Remove(key);

                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
        }

        public void StoreFile(string key, Stream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);
            Files.Add(key, stream);
        }
    }
}