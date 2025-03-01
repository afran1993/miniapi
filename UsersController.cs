using Microsoft.AspNetCore.Mvc;
namespace MiniApi.Controllers;

[ApiController]
[Route("users")]
public class UsersController : ControllerBase
{ 
    private static readonly List<User> Users = new()
    {
        new() { Id = 1, Name = "Mario Rossi" },
        new() { Id = 2, Name = "Luca Bianchi" }
    };

    [HttpGet]
    public IActionResult GetUsers()
    {
        return Ok(Users);
    }
}
