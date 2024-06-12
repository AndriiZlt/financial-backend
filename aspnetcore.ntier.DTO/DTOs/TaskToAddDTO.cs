namespace aspnetcore.ntier.DTO.DTOs;

public class TaskToAddDTO
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string? DateDue { get; set; }
    public int? UserId { get; set; }

}
