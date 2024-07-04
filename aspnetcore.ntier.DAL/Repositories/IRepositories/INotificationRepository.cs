using aspnetcore.ntier.DAL.Entities;

namespace aspnetcore.ntier.DAL.Repositories.IRepositories
{
    public interface INotificationRepository
    {
        Task<List<Notification>> GetListAsync(int userId);
        Task<Notification> AddAsync(Notification notification);
    }
}