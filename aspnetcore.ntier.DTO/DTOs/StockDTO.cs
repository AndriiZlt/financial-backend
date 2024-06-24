using aspnetcore.ntier.DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace aspnetcore.ntier.DTO.DTOs;

public class StockDTO
{
    public int Id { get; set; }
    public string Alpaca_Asset_Id { get; set; }
    public int User_Id { get; set; }
    public string Exchange { get; set; }
    public string Symbol { get; set; }
    public string? Name { get; set; }
    public string Cost_Basis { get; set; }
    public string Qty { get; set; }
    public StockStatus Status { get; set; }

}
