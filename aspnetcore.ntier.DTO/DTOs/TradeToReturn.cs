
namespace aspnetcore.ntier.DTO.DTOs
{
    public class TradeToReturn
    {

        public string symbol { get; set; }

        public Trade? trade { get; set; }


    }

    public class Trade
    {
        public string[] C { get; set; }
        public int I { get; set; }
        public float P { get; set; }
        public int S { get; set; }
        public string T { get; set; }
        public string X { get; set; }
        public string Z { get; set; }
    }


}

/*"symbol": "GOOG",
  "trade": {
    "c": [
      "@"
    ],
    "i": 2401,
    "p": 176.355,
    "s": 114,
    "t": "2024-05-24T19:59:55.7210164Z",
    "x": "V",
    "z": "C"
  }
}*/
