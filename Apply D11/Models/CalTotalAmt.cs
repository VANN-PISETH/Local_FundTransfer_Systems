using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Apply_D11.Models
{
    public class CalTotalAmt
    {
        public decimal Amt { get; set; }
        public decimal TotalAmt { get; set; }
        public decimal ExRate { get; set; }
        public decimal Fee { get; set; }
        public string Rdoreciever { get; set; }
        public string Rdosender { get; set; }
        public string ReceiverAccNumber { get; set; }
    }
}