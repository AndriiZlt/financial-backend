using aspnetcore.ntier.DAL.Entities;
using System.Linq.Expressions;

namespace aspnetcore.ntier.DAL.Repositories.IRepositories
{
    public interface IBoardRepository:IGenericRepository<BoardStock>
    {
        /*Task<BoardStock> GetAsync(Expression<Func<Subtask, bool>> filter = null, CancellationToken cancellationToken = default);*/

        Task<List<BoardStock>> GetListAsync();

        Task<BoardStock> AddAsync(BoardStock stock);

        Task<int> DeleteAsync(BoardStock stock);

/*        Task<Subtask> UpdateStatusTaskAsync(Subtask task);

        Task<Subtask> UpdateTaskAsync(Subtask task);*/

    }
}