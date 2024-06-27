

using aspnetcore.ntier.DAL.Entities;

namespace aspnetcore.ntier.DTO.DTOs;

public class TransactionToAddDTO
{
    public int User_Id { get; set; }
    public int Other_Side_User_Id { get; set; }
    public string Symbol { get; set; }
    public string Price { get; set; }
    public string Qty { get; set; }
    public TransactionSide Side { get; set; }
}

