using LenkieBooks.Models;
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
    /// <param name="book"></param>
    /// <returns></returns>
    Task<bool> AddBook(Book book);

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
}