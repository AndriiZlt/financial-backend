using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aspnetcore.ntier.DAL.Entities
{
    public class Asset
    {
        public string id {  get; set; }
        public string exchange { get; set; }
        public string symbol { get; set; }
        public string name { get; set; }
        public string active { get; set; }
        public bool tradable { get; set; }
        public bool marginable { get; set; }
        public int maintenance_margin_requirement { get; set; }
        public bool shortable { get; set; }
        public bool easy_to_borrow { get; set; }
        public bool fractionable { get; set; }
        public float min_order_size { get; set; }
        public float min_trade_increment { get; set; }
        public float price_increment { get; set; }

    }
}
