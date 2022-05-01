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
    public class ShowCustomerController : Controller
    {
        // GET: ShowCustomer
        public ActionResult Index()
        {
            return View();
        }
        private SqlConnection con;
        private void Connection()
        {
            string Connection = ConfigurationManager.ConnectionStrings["FTDEP"].ToString();
            con = new SqlConnection(Connection);
        }
        public ActionResult Information()
        {
            return View();
        }

        public JsonResult ShowInformation(Customer Cus)
        {
            Connection();
            //  sen.CusName = "Tanjiro";
            //  sen.Gender = "Male";
            //  sen.Phone = "99999998";
            //  SqlCommand cmd = new SqlCommand("select A.AccID , C.Phone,C.Gender,C.Address,A.Balance from tb_Account as A join tb_Customer as C on A.CusID=C.CusID where C.CusName = '" + sen.CusName+"'", con);
            SqlCommand cmd = new SqlCommand("sp_AllShowIF_Cus", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AccNum", Cus.AccNum);
            SqlDataReader reader;
            con.Open();
            reader = cmd.ExecuteReader();
            if (reader != null)
            { 
                while (reader.Read())
                {
                    // sen.AccID = reader.GetValue(0).ToString();
                   // Cus.AccNum = reader.GetValue(0).ToString();
                    Cus.CusName = reader.GetValue(0).ToString();
                    Cus.AccType = reader.GetValue(5).ToString();
                    Cus.Gender = reader.GetValue(2).ToString();
                    Cus.CCY = reader.GetValue(6).ToString();
                    Cus.Phone = reader.GetValue(1).ToString();
                    Cus.Address = reader.GetValue(3).ToString();
                    Cus.Balance = reader.GetValue(4).ToString();

                }
            }
            con.Close();
            var json = JsonConvert.SerializeObject(Cus);
            return Json(json, JsonRequestBehavior.AllowGet);
        }


        public JsonResult CheckAccNum(Customer Cus)
        {
            Connection();
            SqlCommand cmd = new SqlCommand("select * from tb_Account where AccNum = '" + Cus.AccNum + "'", con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                return Json(0);
            }
            else
            {
                return Json(1);
            }
            con.Close();

        }

    }
}