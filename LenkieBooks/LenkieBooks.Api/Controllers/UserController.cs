using LenkieBooks.Interfaces;
using LenkieBooks.Models;
using LenkieBooks.Models.Request;
using LenkieBooks.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LenkieBooks.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IJwtService _jwtService;

    public UserController(
        UserManager<IdentityUser> userManager, 
        IJwtService jwtService)
    {
        _userManager = userManager;
        _jwtService = jwtService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<User>> RegisterUser(RegisterUserRequest registerUserRequest)
    {

        var result = await _userManager.CreateAsync(
            new IdentityUser() { UserName = registerUserRequest.UserName, Email = registerUserRequest.Email },
            registerUserRequest.Password
        );

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }
        return CreatedAtAction("GetUser", new { username = registerUserRequest.UserName }, registerUserRequest);
    }
    
    [HttpGet("{username}")]
    public async Task<ActionResult<User>> GetUser(string username)
    {
        IdentityUser user = await _userManager.FindByNameAsync(username);

        if (user == null)
        {
            return NotFound();
        }

        return new User
        {
            UserName = user.UserName,
            Email = user.Email
        };
    }
    
    [HttpPost("login")]
    public async Task<ActionResult<AuthenticationResponse>> Login(AuthenticationRequest request)
    {
        var user = await _userManager.FindByNameAsync(request.UserName);

        if (user == null)
        {
            return BadRequest("Bad credentials");
        }

        var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);

        if (!isPasswordValid)
        {
            return BadRequest("Bad credentials");
        }

        var token = _jwtService.CreateToken(user);

        return Ok(token);
    }
}