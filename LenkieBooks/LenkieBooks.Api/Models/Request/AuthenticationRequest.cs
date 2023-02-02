using System.ComponentModel.DataAnnotations;

namespace LenkieBooks.Models.Request;

public class AuthenticationRequest
{
    [Required]
    public string UserName { get; set; }
    [Required]
    public string Password { get; set; }
}