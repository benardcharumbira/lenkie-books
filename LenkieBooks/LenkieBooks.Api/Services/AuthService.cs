using LenkieBooks.Data;
using LenkieBooks.Interfaces;
using LenkieBooks.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LenkieBooks.Services;

public class AuthService : IAuthService
{
    private readonly LibraryContext _context;

    public AuthService(LibraryContext context)
    {
        _context = context;
    }
    
    // public async Task<bool> AddCustomer()
    // {
    //     return await _context.Customers.ToListAsync();
    // }

    public async Task<ActionResult<Customer>> DeleteCustomer(int id)
    {
        var customer = await _context.Customers.FindAsync(id);
        if (customer != null)
        {
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
        }
        return customer;
    }
}