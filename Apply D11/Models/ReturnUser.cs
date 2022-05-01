using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Apply_D11.Models
{
    public class ReturnUser
    {
        public int ErrCode { get; set; }
        public string ErrMsg { get; set; }
        public List<Transaction> ListTxn { get; set; }
    }
}