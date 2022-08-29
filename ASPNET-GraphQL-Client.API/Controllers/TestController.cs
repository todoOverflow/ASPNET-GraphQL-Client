using ASPNET_GraphQL_Client.API.Models;
using ASPNET_GraphQL_Client.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace ASPNET_GraphQL_Client.API.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{

    private readonly IUserService _userService;

    public TestController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("GetUsers")]
    public async Task<IEnumerable<UserDto>> GetUsers()
    {
        return await _userService.GetUsers();
    }
}
