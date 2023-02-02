namespace LenkieBooks.Models.Request;

public class AddBookRequest
{
    public string Name { get; set; }
    public string Author { get; set; }
    public string PublicationYear { get; set; }
    public int StockCount { get; set; }
}