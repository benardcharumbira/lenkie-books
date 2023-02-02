using System.ComponentModel.DataAnnotations;

namespace LenkieBooks.Models;

public class Customer
{
    [Key]
    public int CustomerId { get; set; }
    public User User { get; set; }
    public string UserId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
}