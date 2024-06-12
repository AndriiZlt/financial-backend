using aspnetcore.ntier.DAL.Entities;
using System.Linq.Expressions;

namespace aspnetcore.ntier.DAL.Repositories.IRepositories
{
    public interface ITaskRepository 
    {
        Task<Taskk> GetAsync(Expression<Func<Taskk, bool>> filter = null, CancellationToken cancellationToken = default);

        Task<List<Taskk>> GetListAsync(int userId);

        Task<Taskk> AddAsync(Taskk task);

        Task<int> DeleteAsync(Taskk task);

        Task<Taskk> UpdateStatusTaskAsync(Taskk task);

        Task<Taskk> UpdateTaskAsync(Taskk task);

    }
}