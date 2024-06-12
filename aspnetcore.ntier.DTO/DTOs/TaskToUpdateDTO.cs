
namespace aspnetcore.ntier.DTO.DTOs;

public class TaskToUpdateDTO
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }
    public string? DateDue { get; set; }
    public string? DateCompleted { get; set; }
    public int? UserId { get; set; }

}
