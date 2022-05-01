using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Apply_D11.Models
{
    public class Transaction
    {
        public int TxnID { get; set; }
        public string AccID { get; set; }
        public string UserID { get; set; }
        public string TxnType { get; set; }
        public DateTime TxnDate { get; set; }
        public decimal Fee { get; set; }
        public decimal ExChange { get; set; }
        public string CCY { get; set; }      
        public decimal TxnAmount { get; set; }
        public decimal Total { get; set; }
        public string FeePay { get; set; }
        public string status { get; set; }
        public decimal SenderBalance { get; set; }
        public string DateTxn { get; set; }// in controller Autherize
        public decimal RecieverBalance { get; set; }
        
    }
}