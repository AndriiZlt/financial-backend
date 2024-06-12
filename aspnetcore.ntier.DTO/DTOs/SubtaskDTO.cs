

namespace aspnetcore.ntier.DTO.DTOs;

public class SubtaskDTO
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }
    public string DateCreated { get; set; } 
    public string? DateCompleted { get; set; }
    public int TaskId { get; set; }
    public int? UserId { get; set; }

}
