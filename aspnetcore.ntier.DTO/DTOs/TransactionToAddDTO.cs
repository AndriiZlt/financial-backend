


namespace aspnetcore.ntier.DTO.DTOs;

public class TransactionToAddDTO
{
    public int Seller_Stock_Id { get; set; }
    public int? Buyer_Stock_Id { get; set; }
    public int Seller_User_Id { get; set; }
    public int Buyer_User_Id { get; set; }
    public int Board_Item_Id { get; set; } 
    public string Symbol { get; set; }
    public string Cost_Basis { get; set; }
    public string Qty { get; set; }
    public string Total_Price { get; set; }
    public string? Name { get; set; }
}

