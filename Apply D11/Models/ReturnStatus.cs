using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Apply_D11.Models
{
    public class ReturnStatus
    {
        public int ErrCode { get; set; }
        public string ErrMsg { get; set; }
        public List<Transaction> Users { get; set; }
    }
}