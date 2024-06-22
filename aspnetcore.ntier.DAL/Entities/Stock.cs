using System.ComponentModel.DataAnnotations;

namespace aspnetcore.ntier.DAL.Entities
{
    public class Stock
    {
        [Key]
        public int Id { get; set; }
        public string Alpaca_Asset_Id {  get; set; }
        [Required]
        public int UserId { get; set; }
        public string Exchange { get; set; }
        public string Symbol { get; set; }
        public string? Name { get; set; }
        public string Cost_Basis { get; set; }
        public string Qty { get; set; }
        public StockStatus Status { get; set; }
        public User User { get; set; }
    }

    public enum StockStatus
    {
        Fixed = 1,
        For_Sale = 2,
        For_Purchase = 3,
    }


}
