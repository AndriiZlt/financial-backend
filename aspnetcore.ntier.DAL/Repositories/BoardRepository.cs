using aspnetcore.ntier.DAL.Repositories.IRepositories;
using aspnetcore.ntier.DAL.Entities;
using aspnetcore.ntier.DAL.DataContext;
using Microsoft.EntityFrameworkCore;


namespace aspnetcore.ntier.DAL.Repositories
{
    public class BoardRepository : GenericRepository<BoardItem>, IBoardRepository
    {

        private readonly AspNetCoreNTierDbContext _aspNetCoreNTierDbContext;
        public BoardRepository(AspNetCoreNTierDbContext aspNetCoreNTierDbContext) : base(aspNetCoreNTierDbContext)
        {
            _aspNetCoreNTierDbContext = aspNetCoreNTierDbContext;
        }
        public async Task<List<Stock>> GetListAsync()
        {
            /* return await _aspNetCoreNTierDbContext.Set<Subtask>().ToListAsync();*/
            return await _aspNetCoreNTierDbContext.Set<Stock>().Where(t => t.Status != StockStatus.Fixed).ToListAsync();
        }

/*        public async Task<BoardStock> GetAsync(Expression<Func<BoardStock, bool>> filter = null, CancellationToken cancellationToken = default)
        {
            return await _aspNetCoreNTierDbContext.Set<Subtask>().AsNoTracking().FirstOrDefaultAsync(filter, cancellationToken);
        }*/

        public async Task<BoardItem> AddAsync(BoardItem stock)
        {

            await _aspNetCoreNTierDbContext.AddAsync(stock);
            await _aspNetCoreNTierDbContext.SaveChangesAsync();
            return stock;
        }

        public async Task<int> DeleteAsync(BoardItem stock)
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

