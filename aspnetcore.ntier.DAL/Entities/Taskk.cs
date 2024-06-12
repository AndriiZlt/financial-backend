
using System.ComponentModel.DataAnnotations;

namespace aspnetcore.ntier.DAL.Entities
{
    public class Taskk
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        public User User { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status {  get; set; }
        public string DateCreated { get; set; }
        public string? DateDue {  get; set; }
        public string? DateCompleted { get; set; }

        public ICollection<Subtask> Subtasks { get; set; }

        public Taskk()
        {
            Status = "undone";
            DateCreated = DateTime.UtcNow.ToString("yyyy-MM-ddThh:mm:ssZ");
        }
    }
}
