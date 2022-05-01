using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Configuration;
using Apply_D11.Models;
using System.Data;
using Newtonsoft.Json;

namespace Apply_D11.Controllers
{
    public class CreateCustomerController : Controller
    {

        private SqlConnection con;
        private void Connection()
        {
            string Connection = ConfigurationManager.ConnectionStrings["FTDEP"].ToString();
            con = new SqlConnection(Connection);
        }
        // GET: CreateCustomer
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateCus()
        {
            return View();
        }


        public JsonResult AddCus(Customer cus)
        {
            Connection(); 
            Random rnd = new Random();
            int num = rnd.Next(100000000);
            cus.AccNum = num.ToString();
            SqlCommand cmd = new SqlCommand("AddCustomer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AccNum", cus.AccNum);
            cmd.Parameters.Add("@AccType", SqlDbType.NVarChar).Value = cus.AccType;
            cmd.Parameters.Add("@CCY", SqlDbType.VarChar).Value = cus.CCY;
            cmd.Parameters.Add("@Balance", SqlDbType.Decimal).Value =  0;
            cmd.Parameters.Add("@CusName", SqlDbType.VarChar).Value = cus.CusName;
            cmd.Parameters.Add("@Gender", SqlDbType.VarChar).Value = cus.Gender;
            cmd.Parameters.Add("@Phone", SqlDbType.VarChar).Value = cus.Phone;
            cmd.Parameters.Add("@Address", SqlDbType.NVarChar).Value = cus.Address;
            //cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = cus.Email;          
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return Json(JsonConvert.SerializeObject(cus), JsonRequestBehavior.AllowGet);
        }
    }
}