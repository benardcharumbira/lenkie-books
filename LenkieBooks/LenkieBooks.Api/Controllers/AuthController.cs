using LenkieBooks.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace LenkieBooks.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    public AuthController(IUserService userService)
    {
        _userService = userService;
    }
}