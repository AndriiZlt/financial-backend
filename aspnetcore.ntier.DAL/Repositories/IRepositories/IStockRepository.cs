using aspnetcore.ntier.DAL.Entities;
using System.Linq.Expressions;

namespace aspnetcore.ntier.DAL.Repositories.IRepositories
{
    public interface IStockRepository 
    {
        Task<Stock> GetAsync(Expression<Func<Stock, bool>> filter = null, CancellationToken cancellationToken = default);

        Task<List<Stock>> GetListAsync(int userId);

        Task<List<Stock>> GetBoardListAsync();

        Task<Stock> AddAsync(Stock stock);

        Task<int> DeleteAsync(Stock stock);

        Task<Stock> UpdateStatusAsync(Stock stock);

        Task<Stock> BuyStockAsync(Stock stock);

    }
}