using LenkieBooks.Data;
using LenkieBooks.Interfaces;
using LenkieBooks.Models;
using LenkieBooks.Models.Request;
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

    public async Task<bool> AddBook(AddBookRequest addBookRequest)
    {
        var book = new Book()
        {
            Name = addBookRequest.Name,
            Author = addBookRequest.Author,
            PublicationYear = addBookRequest.PublicationYear,
            StockCount = addBookRequest.StockCount
        };

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

    public async Task<BookResponse> ReserveBook(BookRequest bookRequest)
    {
        var book = await _context.Books.FindAsync(bookRequest.BookId);
        if (book is not { StockCount: > 0 })
            return new BookResponse()
            {
                Message = "Book not found or stock is finished, please contact librarian."
            };
        _context.BookReservations.Add(new BookReservation()
        {
            BookId = bookRequest.BookId,
            ReservationDate = DateTime.Now,
            UserId = bookRequest.UserId
        });
        book.StockCount -= book.StockCount;
        await _context.SaveChangesAsync();
        return new BookResponse()
        {
            Message = "Reservation successful, please note reservation will be cancelled in 24hrs.",
            IsRequestApproved = true
        };
    }
    
    public async Task<BookResponse> BorrowBook(BookRequest bookRequest)
    {
        var book = await _context.Books.FindAsync(bookRequest.BookId);
        if (book is not { StockCount: > 0 })
            return new BookResponse()
            {
                Message = "Book not found or stock is finished, please contact librarian."
            };

        var activeReservations = await _context.BookReservations
            .Where(x => x.BookId == bookRequest.BookId)
            .ToListAsync();
        
        
        // WHAT SHOULD WE CHECK!

        // foreach (var activeReservation in activeReservations)
        // {
        //     
        // }
        //
        // if (activeReservation != null || activeReservation?.UserId == bookRequest.UserId)
        // {
        //     _context.BookRentals.Add(new BookRental()
        //     {
        //         BookId = bookRequest.BookId,
        //         DateRetrieved = DateTime.Now,
        //         DueDate = DateTime.Now.AddDays(30)
        //     });
        // }
        //
        // if(activeReservation?.UserId != bookRequest.UserId)
        //     book.StockCount -= book.StockCount;
        
        await _context.SaveChangesAsync();
        return new BookResponse()
        {
            Message = "Request successful, please come during our operating hours to collect your book.",
            IsRequestApproved = true
        };
    }
}