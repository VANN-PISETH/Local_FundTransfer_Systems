using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Apply_D11.Models
{
    public class Customer
    {
        public int AccID { get; set; }
        public int CusID { get; set; }
        public string AccNum { get; set; }
        public string AccType { get; set; }
        public string CCY { get; set; }
        public string Balance { get; set; }
        public string CusName { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        //public string Email { get; set; }
    }
}