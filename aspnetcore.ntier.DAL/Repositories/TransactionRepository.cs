using aspnetcore.ntier.DAL.Repositories.IRepositories;
using aspnetcore.ntier.DAL.Entities;
using aspnetcore.ntier.DAL.DataContext;
using Microsoft.EntityFrameworkCore;


namespace aspnetcore.ntier.DAL.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {

        private readonly AspNetCoreNTierDbContext _aspNetCoreNTierDbContext;
        public TransactionRepository(AspNetCoreNTierDbContext aspNetCoreNTierDbContext) 
        {
            _aspNetCoreNTierDbContext = aspNetCoreNTierDbContext;
        }
        public async Task<List<Transaction>> GetListAsync(int userId)
        {
            /*            return await _aspNetCoreNTierDbContext.Set<Transaction>().Where(t => t.Selling_User_Id == userId || t.Buying_User_Id == userId).ToListAsync();*/
            return await _aspNetCoreNTierDbContext.Set<Transaction>().Where(t => t.User_Id == userId).ToListAsync();
        }

/*        public async Task<Transaction> GetAsync(Expression<Func<Stock, bool>> filter = null, CancellationToken cancellationToken = default)
        {
            return await _aspNetCoreNTierDbContext.Set<Stock>().AsNoTracking().FirstOrDefaultAsync(filter, cancellationToken);
        }*/

        public async Task<Transaction> AddAsync(Transaction transaction)
        {
            Console.WriteLine("Transaction in Reposiroty: {@tran }", transaction);
            await _aspNetCoreNTierDbContext.AddAsync(transaction);

            await _aspNetCoreNTierDbContext.SaveChangesAsync();
            return transaction;
        }


        /*public async Task<Stock> UpdateAsync(Stock stock)
        {
            _ = _aspNetCoreNTierDbContext.Update(stock);

            await _aspNetCoreNTierDbContext.SaveChangesAsync();
            return stock;
        }*/

        /*        public async Task<Stock> BuyStockAsync(Stock stock)
                {
                    _ = _aspNetCoreNTierDbContext.Update(stock);

                    await _aspNetCoreNTierDbContext.SaveChangesAsync();
                    return stock;
                }*/

        /*        public Task<int> ThrowErrorAsync()
                {
                    throw new NotImplementedException();
                }*/

        /*        public Task<int> DeleteAsync(Stock stock)
                {
                    throw new NotImplementedException();
                }*/

    }
}

