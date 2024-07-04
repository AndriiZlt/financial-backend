
using aspnetcore.ntier.DAL.Entities;

namespace aspnetcore.ntier.DTO.DTOs
{
    public class NotificationToAddDTO
    {
        public int User_Id { get; set; }
        public NotificationType? Type { get; set; }
        public string Text { get; set; }
    }
}
