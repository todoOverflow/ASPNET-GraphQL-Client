using ASPNET_GraphQL_Client.API.Models;

namespace ASPNET_GraphQL_Client.API.Services;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetUsers();
}