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
            return await _aspNetCoreNTierDbContext.Set<Transaction>().AsNoTracking().Where(t => t.Seller_User_Id == userId || t.Buyer_User_Id == userId).ToListAsync();
        }


        public async Task<Transaction> AddAsync(Transaction transaction)
        {
            await _aspNetCoreNTierDbContext.AddAsync(transaction);

            await _aspNetCoreNTierDbContext.SaveChangesAsync();
            return transaction;
        }

    }
}

