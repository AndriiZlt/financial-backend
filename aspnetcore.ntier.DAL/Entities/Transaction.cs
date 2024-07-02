using System.ComponentModel.DataAnnotations;
using System.Transactions;

namespace aspnetcore.ntier.DAL.Entities
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }
        public int Seller_Stock_Id { get; set; }
        public int Buyer_Stock_Id { get; set; }
        [Required]
        public int Seller_User_Id { get; set; }
        public int Buyer_User_Id { get; set; }
        public int Board_Item_Id {  get; set; }
        public string Symbol { get; set; }
        public string Transaction_Time { get; set; }
        public string Cost_Basis { get; set; }
        public string Qty { get; set; }
        public string Total_Price { get; set; }
        public string? Name { get; set; }
        public ICollection<User> Users { get; set; }
        public Transaction()
        {
            Transaction_Time = DateTime.UtcNow.ToString("yyyy-MM-ddThh:mm:ssZ");
        }
    }

}
