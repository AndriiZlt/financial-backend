using aspnetcore.ntier.DAL.Repositories.IRepositories;
using aspnetcore.ntier.DAL.Entities;
using aspnetcore.ntier.DAL.DataContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace aspnetcore.ntier.DAL.Repositories
{
    public class StockRepository : IStockRepository
    {

        private readonly AspNetCoreNTierDbContext _aspNetCoreNTierDbContext;
        public StockRepository(AspNetCoreNTierDbContext aspNetCoreNTierDbContext) 
        {
            _aspNetCoreNTierDbContext = aspNetCoreNTierDbContext;
        }
        public async Task<List<Stock>> GetListAsync(int userId)
        {
            return await _aspNetCoreNTierDbContext.Set<Stock>().Where(t=>t.UserId == userId).ToListAsync();
        }

        public async Task<List<Stock>> GetBoardListAsync()
        {
            return await _aspNetCoreNTierDbContext.Set<Stock>().Where(t => t.Status != StockStatus.Fixed).ToListAsync();
        }
        public async Task<List<Stock>> GetListAsync()
        {
            return await _aspNetCoreNTierDbContext.Set<Stock>().Where(t => t.Status != StockStatus.Fixed).ToListAsync();
        }

        public async Task<Stock> GetAsync(Expression<Func<Stock, bool>> filter = null, CancellationToken cancellationToken = default)
        {
            return await _aspNetCoreNTierDbContext.Set<Stock>().AsNoTracking().FirstOrDefaultAsync(filter, cancellationToken);
        }

        public async Task<Stock> AddAsync(Stock stock)
        {
            await _aspNetCoreNTierDbContext.AddAsync(stock);
            await _aspNetCoreNTierDbContext.SaveChangesAsync();
            return stock;
        }

        public async Task<int> DeleteAsync(Stock task)
        {
            _ = _aspNetCoreNTierDbContext.Remove(task);
            return await _aspNetCoreNTierDbContext.SaveChangesAsync();
        }

        public async Task<Stock> UpdateStatusTaskAsync(Stock stock)
        {
            _ = _aspNetCoreNTierDbContext.Update(stock);

            await _aspNetCoreNTierDbContext.SaveChangesAsync();
            return stock;
        }

        public async Task<Stock> UpdateStockAsync(Stock stock)
        {
            _ = _aspNetCoreNTierDbContext.Update(stock);

            await _aspNetCoreNTierDbContext.SaveChangesAsync();
            return stock;
        }

    }
}

