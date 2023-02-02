using LenkieBooks.Data;
using LenkieBooks.Interfaces;
using LenkieBooks.Models;
using LenkieBooks.Models.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LenkieBooks.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class LibraryController : ControllerBase
{
    private readonly IBookService _bookService;

    public LibraryController(IBookService bookService)
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
    public async Task<bool> AddBook(AddBookRequest addBookRequest)
    {
        var newBook = await _bookService.AddBook(addBookRequest);
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
    
    [HttpGet("search/{name}")]
    public async Task<List<Book>> SearchBook(string name)
    {
        return await _bookService.SearchBook(name);
    }
    
    [HttpPost("reserve")]
    public async Task<BookResponse> ReserveBook(BookRequest bookRequest)
    {
        return await _bookService.ReserveBook(bookRequest);;
    }

}