

namespace aspnetcore.ntier.DTO.DTOs;

public class TransactionFrontendDTO
{
        public int Selling_User_Id { get; set; }
        public int Buying_User_Id { get; set; }
        public string Symbol { get; set; }
        public string Price { get; set; }
        public string Qty { get; set; }
}

