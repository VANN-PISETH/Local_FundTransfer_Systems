using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Apply_D11.Models
{
    public class BalanceCal
    {
        public decimal Fee { get; set; }
        public decimal SenderBalance { get; set; }
        public string SenderAccNum { get; set; }
        public string ReceiverAccNum { get; set; }
        public decimal TotalAmt { get; set; }
        public decimal Amt { get; set; }

    }
}