﻿using System.ComponentModel.DataAnnotations;

namespace aspnetcore.ntier.DAL.Entities
{
    public class AlpacaTransaction
    {
        [Key]
        public int Tr_Id { get; set; }
        public int User_Id { get; set; }
        public string? Activity_type { get; set; }
        public string? Cum_qty { get; set; }
        public string? Id { get; set; }
        public string? Leaves_Qty { get; set; }
        public string? Order_Id { get; set; }
        public string? Order_Status { get; set; }
        public string? Price { get; set; }
        public string? Qty { get; set; }
        public string? Side { get; set; }
        public string? Symbol { get; set; }
        public string? Transaction_Time { get; set; }
        public string? Type { get; set; }
        public User? User { get; set; }

    }

}
