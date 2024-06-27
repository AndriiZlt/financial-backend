using aspnetcore.ntier.DAL.Entities;

namespace aspnetcore.ntier.DAL.Repositories.IRepositories
{
    public interface ITransactionRepository
    {

        Task<List<Transaction>> GetListAsync(int userId);
        Task<Transaction> AddAsync(Transaction transaction);

    }
}