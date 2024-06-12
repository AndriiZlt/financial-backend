using aspnetcore.ntier.DAL.Repositories.IRepositories;
using aspnetcore.ntier.DAL.Entities;
using aspnetcore.ntier.DAL.DataContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace aspnetcore.ntier.DAL.Repositories
{
    public class TaskRepository : ITaskRepository
    {

        private readonly AspNetCoreNTierDbContext _aspNetCoreNTierDbContext;
        public TaskRepository(AspNetCoreNTierDbContext aspNetCoreNTierDbContext) 
        {
            _aspNetCoreNTierDbContext = aspNetCoreNTierDbContext;
        }
        public async Task<List<Taskk>> GetListAsync(int userId)
        {
            /*return await _aspNetCoreNTierDbContext.Set<Taskk>().ToListAsync();*/
            return await _aspNetCoreNTierDbContext.Set<Taskk>().Where(t=>t.UserId == userId).ToListAsync();
        }

        public async Task<Taskk> GetAsync(Expression<Func<Taskk, bool>> filter = null, CancellationToken cancellationToken = default)
        {
            return await _aspNetCoreNTierDbContext.Set<Taskk>().AsNoTracking().FirstOrDefaultAsync(filter, cancellationToken);
        }

        public async Task<Taskk> AddAsync(Taskk task)
        {
            await _aspNetCoreNTierDbContext.AddAsync(task);
            await _aspNetCoreNTierDbContext.SaveChangesAsync();
            return task;
        }

        public async Task<int> DeleteAsync(Taskk task)
        {
            _ = _aspNetCoreNTierDbContext.Remove(task);
            return await _aspNetCoreNTierDbContext.SaveChangesAsync();
        }

        public async Task<Taskk> UpdateStatusTaskAsync(Taskk task)
        {
            _ = _aspNetCoreNTierDbContext.Update(task);

            await _aspNetCoreNTierDbContext.SaveChangesAsync();
            return task;
        }

        public async Task<Taskk> UpdateTaskAsync(Taskk task)
        {
            _ = _aspNetCoreNTierDbContext.Update(task);

            await _aspNetCoreNTierDbContext.SaveChangesAsync();
            return task;
        }

    }
}

