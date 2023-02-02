using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LenkieBooks.Models;

public class BookStock
{
    [Key]
    public int BookStockId { get; set; }
    public Book Book { get; set; }
    public int BookId { get; set; }
    public int StockCount { get; set; }
}