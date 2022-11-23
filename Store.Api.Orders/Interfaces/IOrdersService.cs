using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.Api.Orders.Interfaces
{
    public interface IOrdersService
    {
        Task<(bool IsSuccess, IEnumerable<Models.Order> Orders, string ErrorMessage)> GetOrdersAsync(int customerId);
    }
}
