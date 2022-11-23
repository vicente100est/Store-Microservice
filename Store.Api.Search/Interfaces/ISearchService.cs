using System.Threading.Tasks;

namespace Store.Api.Search.Interfaces
{
    public interface ISearchService
    {
        Task<(bool IsSuccess, dynamic SearchResults)> SearchAsync(int customerId);
    }
}