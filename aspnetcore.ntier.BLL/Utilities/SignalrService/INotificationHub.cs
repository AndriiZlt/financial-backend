namespace aspnetcore.ntier.BLL.Utilities
{
    public interface INotificationHub
    {
        Task Register(int userId);
        Task SendNewNotification(string connectionId);
        Task SendPublicBoardUpdate(string connectionId);

    }
}