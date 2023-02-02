using LenkieBooks.Models;
using LenkieBooks.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace LenkieBooks.Interfaces;

public interface IBookService
{
    /// <summary>
    /// Returns a list of books with their respective statuses
    /// </summary>
    /// <returns></returns>
    Task<List<Book>> GetBooks();


    /// <summary>
    /// Retrieves a single book by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<ActionResult<Book>> GetBook(int id);
    
    /// <summary>
    /// Adds a new book to a list of existing books
    /// </summary>
    /// <param name="addBookRequest"></param>
    /// <returns></returns>
    Task<bool> AddBook(AddBookRequest addBookRequest);

    /// <summary>
    /// Remove a book from the library
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<Book> DeleteBook(int id);

    /// <summary>
    /// Search for book by name
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    Task<List<Book>> SearchBook(string name);

    /// <summary>
    /// Set a 24hr reservation slot for a specific book
    /// </summary>
    /// <param name="bookRequest"></param>
    /// <returns></returns>
    Task<BookResponse> ReserveBook(BookRequest bookRequest);
}