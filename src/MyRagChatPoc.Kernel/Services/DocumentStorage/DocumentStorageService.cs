using Azure;
using Azure.Data.Tables;
using Azure.Storage.Blobs;
using MyRagChatPoc.Kernel.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRagChatPoc.Kernel.Services.DocumentStorage
{
    public class DocumentStorageService : IDocumentStorageService
    {
        public const string DocumentTableName = "Documents";

        private readonly BlobServiceClient _blobService;
        private readonly TableServiceClient _tableService;

        public string IndexId { get; }

        internal DocumentStorageService(string indexId, BlobServiceClient blobService, TableServiceClient tableService)
        {
            IndexId = indexId;
            _blobService = blobService;
            _tableService = tableService;
        }

        private class MemoryDocumentTableEntity : StoredDocument, ITableEntity
        {
            public string PartitionKey { get; set; }
            public string RowKey { get; set; }
            public DateTimeOffset? Timestamp { get; set; }
            public ETag ETag { get; set; }
        }

        private BlobContainerClient GetContainerClient()
        {
            var container = _blobService.GetBlobContainerClient(IndexId.ToLower());
            container.CreateIfNotExists();

            return container;
        }

        private TableClient GetTableClient()
        {
            var table = _tableService.GetTableClient(DocumentTableName);
            table.CreateIfNotExists();

            return table;
        }

        public async Task<IEnumerable<StoredDocument>> GetDocumentsAsync()
        {
            var table = GetTableClient();

            await Task.CompletedTask;
            var queryResultsFilter = table.Query<MemoryDocumentTableEntity>(filter: $"PartitionKey eq '{IndexId}'");

            return queryResultsFilter.ToList();
        }

        public async Task<string> UploadDocumentAsync(string path, Stream document)
        {
            var container = GetContainerClient();
            var table = GetTableClient();

            document.Seek(0, SeekOrigin.Begin);
            await container.UploadBlobAsync(path, document);

            var docEntity = new MemoryDocumentTableEntity
            {
                PartitionKey = IndexId,
                RowKey = Guid.NewGuid().ToString(),
                IndexId = IndexId,
                Filename = path,
                UploadedOn = DateTime.UtcNow,
                Timestamp = DateTime.UtcNow
            };

            table.AddEntity(docEntity);

            return path;
        }

        public Task DeleteDocumentAsync(string path)
        {
            throw new NotImplementedException();
        }

        public Task<Stream> DownloadDocumentAsync(string path)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync()
        {
            var container = GetContainerClient();
            var table = GetTableClient();

            await container.DeleteIfExistsAsync();
            await table.DeleteAllEntitiesByPartitionKeyAsync(IndexId);
        }
    }
}