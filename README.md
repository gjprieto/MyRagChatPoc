# Configuration

You need an embeddings model and a completions model deployed, then a vector database.

```
{
  "KnowledgeBase": {
    "Embeddings": {
      "ModelId": "<EMBEDDINGS_DEPLOYMENT_ID>",
      "ApiKey": "<EMBEDDINGS_API_KEY>",
      "Endpoint": "<EMBEDDINGS_ENDPOINT>"
    },
    "Search": {
      "Endpoint": "<SEARCH_ENGINE_ENDPOINT>",
      "ApiKey": "<SEARCH_ENGINE_KEY>"
    }
  },
  "Chat": {
    "Completions": {
      "ModelId": "<COMPLETIONS_MODEL_ID>",
      "ApiKey": "<COMPLETIONS_API_KEY>",
      "Endpoint": "<COMPLETIONS_ENDPOINT>"
    }
  }
}
```