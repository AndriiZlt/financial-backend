using aspnetcore.ntier.DAL.Entities;

namespace aspnetcore.ntier.DTO.DTOs;

public class StockToAddDTO
{
    public int? User_Id { get; set; }
    public string Exchange { get; set; }
    public string Symbol { get; set; }
    public string? Name { get; set; }
    public string Cost_Basis { get; set; }
    public string Qty { get; set; }
    public StockStatus Status { get; set; }

}
