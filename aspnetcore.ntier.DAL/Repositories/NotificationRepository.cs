using aspnetcore.ntier.DAL.Repositories.IRepositories;
using aspnetcore.ntier.DAL.Entities;
using aspnetcore.ntier.DAL.DataContext;
using Microsoft.EntityFrameworkCore;


namespace aspnetcore.ntier.DAL.Repositories
{
    public class NotificationRepository : INotificationRepository
    {

        private readonly AspNetCoreNTierDbContext _aspNetCoreNTierDbContext;
        public NotificationRepository(AspNetCoreNTierDbContext aspNetCoreNTierDbContext) 
        {
            _aspNetCoreNTierDbContext = aspNetCoreNTierDbContext;
        }

        public async Task<List<Notification>> GetListAsync(int userId)
        {
            return await _aspNetCoreNTierDbContext.Set<Notification>().AsNoTracking().Where(t => t.User_Id == userId ).ToListAsync();
        }


        public async Task<Notification> AddAsync(Notification notification)
        {
            await _aspNetCoreNTierDbContext.AddAsync(notification);

            await _aspNetCoreNTierDbContext.SaveChangesAsync();

            return notification;
        }

    }

}

