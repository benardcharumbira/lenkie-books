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
    private readonly LibraryContext _context;
    private readonly ILogger<BookService> _logger;

    public BookService(LibraryContext context, ILogger<BookService> logger)
    {
        _context = context;
        _logger = logger;
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
        try
        {
            var book = await _context.Books.FindAsync(bookRequest.BookId);
            if (book is not { StockCount: > 0 })
            {
                var existingRental = await _context.BookRentals
                    .FirstOrDefaultAsync(x => x.UserId == bookRequest.UserId
                                   && x.BookId == bookRequest.BookId
                                   && !x.IsReturned);
                
                var hasActiveReminder = await _context.BookAvailabilityReminders
                    .AnyAsync(x => x.UserId == bookRequest.UserId
                                              && x.BookId == bookRequest.BookId
                                              && x.IsActive);

                if (existingRental != null && !hasActiveReminder)
                {
                    _context.BookAvailabilityReminders.Add(new BookAvailabilityReminder()
                    {
                        BookId = bookRequest.BookId,
                        UserId = bookRequest.UserId,
                        AvailabilityDate = existingRental.DueDate.AddDays(1),
                        IsActive = true
                    });
                    await _context.SaveChangesAsync();
                }

                return new BookResponse()
                {
                    Message = "Book not found or stock is finished, we will let you know when it is available."
                };
            }

            _context.BookReservations.Add(new BookReservation()
            {
                BookId = bookRequest.BookId,
                ReservationDate = DateTime.Now,
                UserId = bookRequest.UserId
            });
            book.StockCount -= 1;
            await _context.SaveChangesAsync();
            return new BookResponse()
            {
                Message = "Reservation successful, please note reservation will be cancelled in 24hrs.",
                IsRequestApproved = true
            };
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Failed to reserve book for user: {bookRequest.UserId}");
            return new BookResponse()
            {
                Message = "Failed to reserve book."
            };
        }
    }

    public async Task<BookResponse> BorrowBook(BookRequest bookRequest)
    {
        try
        {
            var book = await _context.Books.FindAsync(bookRequest.BookId);
            if (book is not { StockCount: > 0 })
                return new BookResponse()
                {
                    Message = "Book not found or stock is finished, please contact librarian."
                };

            var activeReservation = await _context.BookReservations
                .FirstOrDefaultAsync(x => x.BookId == bookRequest.BookId
                                          && x.UserId == bookRequest.UserId);

            var existingRental = await _context.BookRentals
                .AnyAsync(x => x.BookId == bookRequest.BookId
                               && x.UserId == bookRequest.UserId
                               && !x.IsReturned);

            if (activeReservation == null)
                book.StockCount -= 1;


            if (existingRental)
                return new BookResponse()
                {
                    Message = "You have already borrowed this book"
                };

            _context.BookRentals.Add(new BookRental()
            {
                BookId = bookRequest.BookId,
                DateRetrieved = DateTime.Now,
                DueDate = DateTime.Now.AddDays(30),
                UserId = bookRequest.UserId
            });

            await _context.SaveChangesAsync();
            return new BookResponse()
            {
                Message = "Request successful, please come during our operating hours to collect your book.",
                IsRequestApproved = true
            };
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Failed to request book rental by user: {bookRequest.UserId}");
            return new BookResponse()
            {
                Message = "Failed to request book rental."
            };
        }
    }

    public async Task SendAvailabilityReminders()
    {
        var reminders = await _context.BookAvailabilityReminders
            .Where(x => x.AvailabilityDate < DateTime.Now)
            .ToListAsync();

        foreach (var reminder in reminders)
        {
            // Use service like SendGrid to send email reminder to user
            // Message: Hello, the book you enquired about is now available
        }
    }
    
    public async Task SendBookReturnReminder()
    {
        var reminders = await _context.BookRentals
            .Where(x => x.DueDate < DateTime.Now)
            .ToListAsync();

        foreach (var reminder in reminders)
        {
            // Use service like SendGrid to send email reminder to user
            // Message: Hello, the book you borrowed is due
        }
    }
}