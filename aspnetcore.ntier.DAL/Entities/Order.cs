using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aspnetcore.ntier.DAL.Entities
{
    public class Order
    {
        public string Symbol { get; set; }
        public string Qty { get; set; }
        public string Side { get; set; }
        public string Type { get; set; }
        public string Limit_price { get; set; }
        public string Time_in_force { get; set; }

        public Order() 
        {
            Side = "buy";
            Type= "limit";
            Time_in_force = "gtc";
        }

    }
}
