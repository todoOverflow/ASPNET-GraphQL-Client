using System.Net.Http.Headers;
using GraphQL.Client.Abstractions;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;

namespace ASPNET_GraphQL_Client.API.Services
{
    public abstract class GraphqlClientBase
    {
        private const string Stringkey = "hasura";
        private readonly IConfiguration _configuration;
        public readonly GraphQLHttpClient GraphQlHttpClient ;
        public GraphqlClientBase(IConfiguration configuration)
        {
            _configuration = configuration;
            if(GraphQlHttpClient == null)
            {
                GraphQlHttpClient = GetGraphQlApiClient();
            }
        }

        public GraphQLHttpClient GetGraphQlApiClient()
        {

            var endpoint = _configuration.GetConnectionString(Stringkey);

            var httpClientOption = new GraphQLHttpClientOptions
            {
                EndPoint = new Uri(endpoint)
            };

            return new GraphQLHttpClient(httpClientOption, new NewtonsoftJsonSerializer());
        }
    }

    public class GraphQLHttpRequestWithHeadersSupport : GraphQLHttpRequest
    {
        public Dictionary<string, string> Headers { get; set; }
        public override HttpRequestMessage ToHttpRequestMessage(GraphQLHttpClientOptions options, IGraphQLJsonSerializer serializer)
        {
            var r = base.ToHttpRequestMessage(options, serializer);
            foreach (var header in Headers)
            {
                r.Headers.Add(header.Key,header.Value);
            }

            return r;
        }
    }

    public class GraphQLHttpRequestWithAuthSupport : GraphQLHttpRequest {
        public AuthenticationHeaderValue? Authentication { get; set; }

        public override HttpRequestMessage ToHttpRequestMessage(GraphQLHttpClientOptions options, IGraphQLJsonSerializer serializer) {
            var r = base.ToHttpRequestMessage(options, serializer);
            r.Headers.Authorization = Authentication;
            return r;
        }
    }
}
