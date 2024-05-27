using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRagChatPoc.Kernel.Tools
{
    public class CustomHttpMessageHandler : HttpClientHandler
    {
        public string BaseUri { get; }
        public string DefaultRequestUri { get; }

        public CustomHttpMessageHandler(string baseUri, string defaultRequestUri = "api.openai.com")
        {
            BaseUri = baseUri;
            DefaultRequestUri = defaultRequestUri;
        }

        private string SafelyReadContent(HttpRequestMessage request)
        {
            var stream = request.Content.ReadAsStreamAsync().Result;
            var reader = new StreamReader(stream);
            var result = reader.ReadToEnd();
            stream.Seek(0, SeekOrigin.Begin);

            return result;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.RequestUri != null && request.RequestUri.Host.Equals(DefaultRequestUri, StringComparison.OrdinalIgnoreCase))
            {
                request.RequestUri = new Uri($"{BaseUri}{request.RequestUri.PathAndQuery}");
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}