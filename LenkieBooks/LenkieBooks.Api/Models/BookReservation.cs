using System.ComponentModel.DataAnnotations;

namespace LenkieBooks.Models;

public class BookReservation
{
    [Key]
    public int BookReservationId { get; set; }
    public User User { get; set; }
    public string UserId { get; set; }
    public Book Book { get; set; }
    public int BookId { get; set; }
    public DateTime ReservationDate { get; set; }
}