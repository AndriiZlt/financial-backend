using System.ComponentModel.DataAnnotations;

namespace aspnetcore.ntier.DTO.DTOs;

public class TaskDTO
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }
    public string DateCreated { get; set; }
    public string? DateDue { get; set; }
    public string? DateCompleted { get; set; }
    public int? UserId { get; set; }

}
