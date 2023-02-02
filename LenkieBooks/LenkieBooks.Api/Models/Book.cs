using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LenkieBooks.Models;

public class Book
{
    [Key]
    public int BookId { get; set; }
    public string Name { get; set; }
    public string Author { get; set; }
    public string PublicationYear { get; set; }
}