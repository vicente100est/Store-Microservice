using Store.Api.Search.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.Api.Search.Interfaces
{
    public interface IOrdersService
    {
        Task<(bool IsSuccess, IEnumerable<Order> Orders, string ErrorMessage)> GetOrdersAsync(int customerId);
    }
}
