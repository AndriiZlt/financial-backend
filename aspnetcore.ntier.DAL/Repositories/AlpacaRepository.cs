using aspnetcore.ntier.DAL.Repositories.IRepositories;
using aspnetcore.ntier.DAL.Entities;
using aspnetcore.ntier.DAL.DataContext;
using Microsoft.EntityFrameworkCore;


namespace aspnetcore.ntier.DAL.Repositories
{
    public class AlpacaRepository : IAlpacaRepository
    {

        private readonly AspNetCoreNTierDbContext _aspNetCoreNTierDbContext;
        public AlpacaRepository(AspNetCoreNTierDbContext aspNetCoreNTierDbContext) 
        {
            _aspNetCoreNTierDbContext = aspNetCoreNTierDbContext;
        }
        public async Task<List<AlpacaTransaction>> GetListAsync(int userId)
        {
            return await _aspNetCoreNTierDbContext.Set<AlpacaTransaction>().AsNoTracking().Where(t => t.User_Id == userId).ToListAsync();
        }


        public async Task<AlpacaTransaction> AddAsync(AlpacaTransaction transaction)
        {
            await _aspNetCoreNTierDbContext.AddAsync(transaction);
            
            await _aspNetCoreNTierDbContext.SaveChangesAsync();
            return transaction;
        }

    }

}

