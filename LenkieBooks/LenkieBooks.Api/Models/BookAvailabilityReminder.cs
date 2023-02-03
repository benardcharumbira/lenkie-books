using System.ComponentModel.DataAnnotations;
using LenkieBooks.Models.Request;

namespace LenkieBooks.Models;

public class BookAvailabilityReminder
{
    [Key]
    public int BookAvailabilityReminderId { get; set; }
    public User User { get; set; }
    public string UserId { get; set; }
    public Book Book { get; set; }
    public int BookId { get; set; }
    public DateTime AvailabilityDate { get; set; }
    public bool IsActive { get; set; }
}