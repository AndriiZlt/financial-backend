
namespace aspnetcore.ntier.DTO.DTOs
{
    public class PositionToReturn
    {
        public string Asset_Id { get; set; }
        public string Exchange { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string Avg_Entry_Price { get; set; }
        public string Change_Today { get; set; }
        public string Cost_Basis { get; set; }
        public string Current_Price { get; set; }
        public string Lastday_Price { get; set; }
        public string Market_Value { get; set; }
        public string Qty { get; set; }
        public string Qty_Available { get; set; }
        public string Side { get; set; }
        public string Unrealized_Intraday_Pl { get; set; }
        public string Unrealized_Intraday_Plpc { get; set; }
        public string Unrealized_Pl { get; set; }
        public string Unrealized_Plpc { get; set; }
    }
}
