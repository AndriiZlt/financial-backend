using aspnetcore.ntier.DAL.Entities;
using System.Linq.Expressions;

namespace aspnetcore.ntier.DAL.Repositories.IRepositories
{
    public interface IBoardRepository:IGenericRepository<BoardItem>
    {
        /*Task<BoardStock> GetAsync(Expression<Func<Subtask, bool>> filter = null, CancellationToken cancellationToken = default);*/

        Task<List<BoardItem>> GetBoardListAsync();

        Task<BoardItem> AddAsync(BoardItem stock);

        Task<int> DeleteAsync(BoardItem stock);

/*        Task<Subtask> UpdateStatusTaskAsync(Subtask task);

        Task<Subtask> UpdateTaskAsync(Subtask task);*/

    }
}