using aspnetcore.ntier.DAL.Repositories.IRepositories;
using aspnetcore.ntier.DAL.Entities;
using aspnetcore.ntier.DAL.DataContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace aspnetcore.ntier.DAL.Repositories
{
    public class BoardRepository : GenericRepository<BoardStock>, IBoardRepository
    {

        private readonly AspNetCoreNTierDbContext _aspNetCoreNTierDbContext;
        public BoardRepository(AspNetCoreNTierDbContext aspNetCoreNTierDbContext) : base(aspNetCoreNTierDbContext)
        {
            _aspNetCoreNTierDbContext = aspNetCoreNTierDbContext;
        }
        public async Task<List<BoardStock>> GetListAsync()
        {
           /* return await _aspNetCoreNTierDbContext.Set<Subtask>().ToListAsync();*/
            return await _aspNetCoreNTierDbContext.Set<BoardStock>().ToListAsync();
        }

/*        public async Task<BoardStock> GetAsync(Expression<Func<BoardStock, bool>> filter = null, CancellationToken cancellationToken = default)
        {
            return await _aspNetCoreNTierDbContext.Set<Subtask>().AsNoTracking().FirstOrDefaultAsync(filter, cancellationToken);
        }*/

        public async Task<BoardStock> AddAsync(BoardStock stock)
        {

            await _aspNetCoreNTierDbContext.AddAsync(stock);
            await _aspNetCoreNTierDbContext.SaveChangesAsync();
            return stock;
        }

        public async Task<int> DeleteAsync(BoardStock stock)
        {
            _ = _aspNetCoreNTierDbContext.Remove(stock);
            return await _aspNetCoreNTierDbContext.SaveChangesAsync();
        }

        /*public async Task<Subtask> UpdateStatusTaskAsync(Subtask task)
        {
            _ = _aspNetCoreNTierDbContext.Update(task);

            await _aspNetCoreNTierDbContext.SaveChangesAsync();
            return task;
        }

        public async Task<Subtask> UpdateTaskAsync(Subtask task)
        {
            _ = _aspNetCoreNTierDbContext.Update(task);

            await _aspNetCoreNTierDbContext.SaveChangesAsync();
            return task;
        }*/

    }
}

