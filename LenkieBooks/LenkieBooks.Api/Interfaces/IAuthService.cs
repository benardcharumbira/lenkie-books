using LenkieBooks.Models;
using Microsoft.AspNetCore.Mvc;

namespace LenkieBooks.Interfaces;

public interface IAuthService
{
    /// <summary>
    /// Returns a list of customers
    /// </summary>
    /// <returns></returns>
    // Task<bool> AddCustomer();

    /// <summary>
    /// Remove a customer
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<ActionResult<Customer>> DeleteCustomer(int id);
}