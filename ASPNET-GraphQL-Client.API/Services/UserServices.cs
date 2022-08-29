using ASPNET_GraphQL_Client.API.Models;

namespace ASPNET_GraphQL_Client.API.Services
{
    public class UserServices: GraphqlClientBase,IUserService
    {
        private const string SecretKey = "x-hasura-admin-secret";
        private const string SecretValue = "your_key";
        public UserServices(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<IEnumerable<UserDto>> GetUsers()
        {
            var query = @"query MyQuery {
                  user {
                    id,
                    firstname,
                    lastname,
                    age,
                    active
                  }
                }";

            var myRequest = new GraphQLHttpRequestWithHeadersSupport()
            {
                Query = query,
                Headers = new Dictionary<string, string>
                {
                    {SecretKey, SecretValue}
                }
            };
            var myResponse = await GraphQlHttpClient.SendQueryAsync<UserResponse>(myRequest);
            return myResponse.Data.User;
        }
    }
}
