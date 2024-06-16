
using aspnetcore.ntier.DAL.Entities;

namespace aspnetcore.ntier.DTO.DTOs;

public class BoardItemDTO
{
    public int Id { get; set; }
    public int Stock_Id { get; set; }
    public int? User_Id { get; set; }
    public string? Symbol { get; set; }
    public string? Name { get; set; }
    public string? Cost_Basis { get; set; }
    public string? Qty { get; set; }
    public string? Side { get; set; }
    public Stock? Stock { get; set; }
}
