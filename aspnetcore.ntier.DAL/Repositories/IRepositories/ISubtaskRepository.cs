using aspnetcore.ntier.DAL.Entities;
using System.Linq.Expressions;

namespace aspnetcore.ntier.DAL.Repositories.IRepositories
{
    public interface ISubtaskRepository:IGenericRepository<Subtask>
    {
        Task<Subtask> GetAsync(Expression<Func<Subtask, bool>> filter = null, CancellationToken cancellationToken = default);

        Task<List<Subtask>> GetListAsync(int userId);

        Task<Subtask> AddAsync(Subtask task);

        Task<int> DeleteAsync(Subtask task);

        Task<Subtask> UpdateStatusTaskAsync(Subtask task);

        Task<Subtask> UpdateTaskAsync(Subtask task);

    }
}