using aspnetcore.ntier.DAL.Repositories.IRepositories;
using aspnetcore.ntier.DAL.Entities;
using aspnetcore.ntier.DAL.DataContext;
using Microsoft.EntityFrameworkCore;
using System.Threading;


namespace aspnetcore.ntier.DAL.Repositories
{
    public class BoardRepository : GenericRepository<BoardItem>, IBoardRepository
    {

        private readonly AspNetCoreNTierDbContext _aspNetCoreNTierDbContext;
        public BoardRepository(AspNetCoreNTierDbContext aspNetCoreNTierDbContext) : base(aspNetCoreNTierDbContext)
        {

            _aspNetCoreNTierDbContext = aspNetCoreNTierDbContext;
        }
        public async Task<List<BoardItem>> GetBoardListAsync()
        {
            return await _aspNetCoreNTierDbContext.Set<BoardItem>().AsNoTracking().Where(t => t.Status != StockStatus.Fixed).ToListAsync();
        }


        public async Task<BoardItem> AddAsync(BoardItem boardItem)
        {

            await _aspNetCoreNTierDbContext.AddAsync(boardItem);
            await _aspNetCoreNTierDbContext.SaveChangesAsync();
            return boardItem;
        }

        public async Task<int> DeleteAsync(BoardItem boardItem)
        {
            _ = _aspNetCoreNTierDbContext.Remove(boardItem);
            return await _aspNetCoreNTierDbContext.SaveChangesAsync();
        }

    }
}

