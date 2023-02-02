using LenkieBooks.Models.Request;
using Microsoft.AspNetCore.Identity;

namespace LenkieBooks.Interfaces;

public interface IJwtService
{
    AuthenticationResponse CreateToken(IdentityUser user);
}