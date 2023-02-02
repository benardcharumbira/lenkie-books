using LenkieBooks.Data;
using LenkieBooks.Interfaces;
using LenkieBooks.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace LenkieBooks.Services;

public class BookService : IBookService
{
    private readonly BookContext _context;

    public BookService(BookContext context)
    {
        _context = context;
    }

    public async Task<List<Book>> GetBooks()
    {
        return await _context.Books.ToListAsync();
    }

    public async Task<ActionResult<Book>> GetBook(int id)
    {
        return await _context.Books.FindAsync(id);
    }

    public async Task<bool> AddBook(Book book)
    {
        _context.Books.Add(book);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<Book> DeleteBook(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book != null)
        {
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }
        return book;
    }

    public async Task<List<Book>> SearchBook(string name)
    {
        var books = new List<Book>();
        if (!name.IsNullOrEmpty())
        {
            books = await _context.Books.Where(x => x.Name.ToLower().Contains(name.ToLower()))
                .ToListAsync();
        }
        return books;
    }
}