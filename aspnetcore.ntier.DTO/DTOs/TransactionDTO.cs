
using aspnetcore.ntier.DAL.Entities;

namespace aspnetcore.ntier.DTO.DTOs
{
    public class TransactionDTO
    {
        public int Id { get; set; }
        public int User_Id { get; set; }
        public int Other_Side_User_Id { get; set; }
        /*        public int Selling_User_Id { get; set; }
                public int Buying_User_Id { get; set; }*/
        /*        public int Stock_Id { get; set; }*/
        public string Symbol { get; set; }
        public string? Transaction_Time { get; set; }
        public string Price { get; set; }
        public string Qty { get; set; }
        public TransactionSide Side { get; set; }

    }
}
