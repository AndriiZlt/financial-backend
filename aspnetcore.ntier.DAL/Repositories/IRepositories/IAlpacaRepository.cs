using aspnetcore.ntier.DAL.Entities;

namespace aspnetcore.ntier.DAL.Repositories.IRepositories
{
    public interface IAlpacaRepository
    {
        Task<List<AlpacaTransaction>> GetListAsync(int userId);
        Task<AlpacaTransaction> AddAsync(AlpacaTransaction transaction);
    }
}