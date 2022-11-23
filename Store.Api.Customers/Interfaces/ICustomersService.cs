using Store.Api.Customers.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.Api.Customers.Interfaces
{
    public interface ICustomersService
    {
        Task<(bool IsSuccess, IEnumerable<Customer> Customers, string ErrorMessage)> GetCustomersAsync();
        Task<(bool IsSuccess, Customer Customer, string ErrorMessage)> GetCustomerAsync(int id);
    }
}
