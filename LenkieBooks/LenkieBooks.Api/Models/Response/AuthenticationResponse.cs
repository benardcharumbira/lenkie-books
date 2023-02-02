namespace LenkieBooks.Models.Request;

public class AuthenticationResponse
{
    public string Token { get; set; }
    public string UserId { get; set; }
    public DateTime Expiration { get; set; } 
}