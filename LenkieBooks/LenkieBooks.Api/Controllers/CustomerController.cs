using LenkieBooks.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace LenkieBooks.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly IUserService _userService;
    public CustomerController(IUserService userService)
    {
        _userService = userService;
    }
}