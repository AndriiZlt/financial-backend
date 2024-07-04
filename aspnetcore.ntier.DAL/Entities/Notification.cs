using System.ComponentModel.DataAnnotations;

namespace aspnetcore.ntier.DAL.Entities
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int User_Id { get; set; }
        public NotificationType Type { get; set; } 
        public string Text { get; set; } = string.Empty;
        public string Date_Created { get; set; }
        public NotificationStatus Status { get; set; }
        public User User { get; set; }

        public Notification()
        {
            Date_Created = DateTime.UtcNow.ToString("yyyy-MM-ddThh:mm:ssZ");
            Status = NotificationStatus.Unread;
            Type = NotificationType.General;
        }
    }

    public enum NotificationStatus
    {
        Unread =1,
        Read = 2, 
    }

    public enum NotificationType
    {
        General = 1,
        Sell_Transaction = 2,
        Buy_Transaction = 3,
        Message = 4,
    }

}
