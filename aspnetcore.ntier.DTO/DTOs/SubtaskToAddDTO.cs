namespace aspnetcore.ntier.DTO.DTOs;

public class SubtaskToAddDTO
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int TaskId { get; set; }
    public int? UserId { get; set; }
}
