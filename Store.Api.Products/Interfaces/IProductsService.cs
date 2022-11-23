using Store.Api.Products.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.Api.Products.Interfaces
{
    public interface IProductsService
    {
        Task<(bool IsSuccess, IEnumerable<Product> Products, string ErrorMessage)> GetProductsAsync();
        Task<(bool IsSuccess, Product Product, string ErrorMessage)> GetProductAsync(int id);
    }
}