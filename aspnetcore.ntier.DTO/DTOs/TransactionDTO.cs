
using aspnetcore.ntier.DAL.Entities;

namespace aspnetcore.ntier.DTO.DTOs
{
    public class TransactionDTO
    {
        public int Id { get; set; }
        public int Stock_Id { get; set; }
        public int Seller_User_Id { get; set; }
        public int Buyer_User_Id { get; set; }
        public string Symbol { get; set; }
        public string Transaction_Time { get; set; }
        public string Price { get; set; }
        public string Qty { get; set; }
        public string? Name { get; set; }

    }
}
