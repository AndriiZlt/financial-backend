
using aspnetcore.ntier.DAL.Entities;

namespace aspnetcore.ntier.DTO.DTOs
{
    public class NotificationDTO
    {
        public int Id { get; set; }
        public int User_Id { get; set; }
        public NotificationType Type { get; set; }
        public string Text { get; set; }
        public string Date_Created { get; set; }
        public NotificationStatus Status { get; set; }
    }
}
