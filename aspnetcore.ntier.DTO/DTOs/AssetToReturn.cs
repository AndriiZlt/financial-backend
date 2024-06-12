
namespace aspnetcore.ntier.DTO.DTOs
{
    public class AssetToReturn
    {
        public string Id { get; set; }
        public string Exchange { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public string Active { get; set; }
        public bool Tradable { get; set; }
        public bool Marginable { get; set; }
        public int Maintenance_margin_requirement { get; set; }
        public bool Shortable { get; set; }
        public bool Easy_to_borrow { get; set; }
        public bool Fractionable { get; set; }
        public float Min_order_size { get; set; }
        public float Min_trade_increment { get; set; }
        public float Price_increment { get; set; }

    }
}
