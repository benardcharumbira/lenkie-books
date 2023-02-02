using LenkieBooks.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LenkieBooks.Data;

public class BookContext : IdentityDbContext<IdentityUser>
{
    public BookContext(DbContextOptions<BookContext> options) : base(options)
    {
    }

    public DbSet<Book> Books { get; set; }
    public DbSet<BookRental> BookRentals { get; set; }
    public DbSet<BookReservation> BookReservations { get; set; }
    public DbSet<Customer> Customers { get; set; }
}