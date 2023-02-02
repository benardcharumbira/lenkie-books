using LenkieBooks.Data;
using LenkieBooks.Interfaces;
using LenkieBooks.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LenkieBooks.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LibraryController : ControllerBase
{
    private readonly IBookService _bookService;

    public LibraryController(IBookService bookService, BookContext context)
    {
        _bookService = bookService;
    }
    
    [HttpGet("books")]
    public async Task<List<Book>> GetBooks()
    {
        return await _bookService.GetBooks();
    }

    [HttpGet("book/{id}")]
    public async Task<ActionResult<Book>> GetBook(int id)
    {
        var book = await _bookService.GetBook(id);

        if (book == null)
        {
            return NotFound();
        }

        return book;
    }
    
    [HttpPost("book")]
    public async Task<bool> AddBook(Book book)
    {
        var newBook = await _bookService.AddBook(book);
        return newBook;
    }
    
    [HttpDelete("book/{id}")]
    public async Task<ActionResult<Book>> DeleteBook(int id)
    {
        var book = await _bookService.DeleteBook(id);
        if (book == null)
        {
            return NotFound();
        }
        return book;
    }
    
    [HttpGet("book/search/{name}")]
    public async Task<List<Book>> SearchBook(string name)
    {
        return await _bookService.SearchBook(name);
    }
    
}