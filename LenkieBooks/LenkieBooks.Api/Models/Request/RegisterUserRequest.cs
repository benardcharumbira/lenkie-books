using System.ComponentModel.DataAnnotations;

namespace LenkieBooks.Models.Request;

public class RegisterUserRequest
{
    [Required]
    public string UserName { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public string Email { get; set; }
}