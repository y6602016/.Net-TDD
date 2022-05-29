using Microsoft.AspNetCore.Mvc;

namespace CloudCustomers.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersConrtroller : ControllerBase
{
    private readonly ILogger<UsersConrtroller> _logger;

    public UsersConrtroller(ILogger<UsersConrtroller> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetUsers")]
    public async Task<IActionResult> Get()
    {
        return null;
    }
}
