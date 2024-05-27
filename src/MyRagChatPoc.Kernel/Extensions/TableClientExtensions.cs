using Azure.Data.Tables;
using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRagChatPoc.Kernel.Extensions
{
    internal static class TableClientExtensions
    {
        internal static async Task<List<Response<IReadOnlyList<Response>>>> BatchManipulateEntitiesAsync<T>(this TableClient tableClient, IEnumerable<T> entities, TableTransactionActionType tableTransactionActionType) where T : class, ITableEntity, new()
        {
            var groups = entities.GroupBy(x => x.PartitionKey);
            var responses = new List<Response<IReadOnlyList<Response>>>();

            foreach (var group in groups)
            {
                List<TableTransactionAction> actions;

                var items = group.AsEnumerable();

                while (items.Any())
                {
                    var batch = items.Take(100);
                    items = items.Skip(100);

                    actions = new List<TableTransactionAction>();
                    actions.AddRange(batch.Select(e => new TableTransactionAction(tableTransactionActionType, e)));
                    var response = await tableClient.SubmitTransactionAsync(actions).ConfigureAwait(false);
                    responses.Add(response);
                }
            }

            return responses;
        }

        internal static async Task DeleteAllEntitiesByPartitionKeyAsync(this TableClient tableClient, string partitionKey)
        {
            var props = new List<string>() { "PartitionKey", "RowKey" };
            var filter = $"PartitionKey eq '{partitionKey}'";

            AsyncPageable<TableEntity> entities = tableClient.QueryAsync<TableEntity>(select: props, filter: filter, maxPerPage: 1000);

            await entities.AsPages().ForEachAwaitAsync(async page => {
                await tableClient.BatchManipulateEntitiesAsync(page.Values, TableTransactionActionType.Delete).ConfigureAwait(false);
            });
        }
    }
}
