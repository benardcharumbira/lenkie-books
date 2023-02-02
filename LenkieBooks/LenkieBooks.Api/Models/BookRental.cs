using System.ComponentModel.DataAnnotations;

namespace LenkieBooks.Models;

public class BookRental
{
    [Key]
    public int BookRentalId { get; set; }
    public User User { get; set; }
    public string UserId { get; set; }
    public Book Book { get; set; }
    public int BookId { get; set; }
    public DateTime DateRetrieved { get; set; }
    public DateTime DueDate { get; set; }
    public bool IsReturned { get; set; }
}