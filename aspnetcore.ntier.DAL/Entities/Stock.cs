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
        public User User { get; set; }
        public string Exchange { get; set; }
        public string Symbol { get; set; }
        public string? Name { get; set; }
        public string Cost_Basis { get; set; }
        public string Qty { get; set; }

        public string Status { get; set; }


        /*        public string active { get; set; }*/
        /*public bool marginable { get; set; }*/
        /*        public int maintenance_margin_requirement { get; set; }*/
        /*        public bool shortable { get; set; }*/
        /*        public bool easy_to_borrow { get; set; }*/
        /*        public bool fractionable { get; set; }*/
        /*        public float min_order_size { get; set; }*/
        /*        public float min_trade_increment { get; set; }*/
        /*        public float price_increment { get; set; }*/

    }
}
