using System.ComponentModel.DataAnnotations;
using System.Transactions;

namespace aspnetcore.ntier.DAL.Entities
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int User_Id { get; set; }
        public int Other_Side_User_Id { get; set; }
        public string Symbol { get; set; }
        public string Transaction_Time { get; set; }
        public string Price { get; set; }
        public string Qty { get; set; }
        public TransactionSide Side { get; set; }
        public User User { get; set; }
        public Transaction()
        {
            Transaction_Time = DateTime.UtcNow.ToString("yyyy-MM-ddThh:mm:ssZ");
        }
    }

    public enum TransactionSide
    {
        Seller = 1,
        Buyer =2,
    }
}
