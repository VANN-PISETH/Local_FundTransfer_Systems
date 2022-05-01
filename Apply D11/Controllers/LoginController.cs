using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Apply_D11.Models;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;



namespace Apply_D11.Controllers
{
    public class LoginController : Controller
    {
        //connection to database
        private SqlConnection con;
        private void Connnection()
        {
            string Connection = ConfigurationManager.ConnectionStrings["FTDEP"].ToString();
            con = new SqlConnection(Connection);
        }
        SqlCommand cmd = new SqlCommand();
        
        // GET: Process
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        public JsonResult Verify(Login login)
        {
            ReturnStatus returnStatus = new ReturnStatus();
            Connection Con = new Connection();
            Connnection();
            
              
                SqlCommand cmd = new SqlCommand("select * from tb_Login where Name='" + login.Name + "' and Password ='" + login.Password + "'", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
               
               if (dt.Rows.Count > 0)
               {
                return Json(0);
               }
               else
               {
                return Json(1);
               }
           
                     


               // return Redirect("~/Home/Index");
               // //return Content("1");
               //// returnStatus.ErrCode = 0;
                     
               // }
               // else
               // {
               //     con.Close();
               //     returnStatus.ErrCode=1;
               //     return View();
               // }

            
            //con.Open();
            //cmd.Connection = con;
            //cmd.CommandText = "select * from tb_Login where Name='" + username + "' and Password='" + password + "'";
            //dr = cmd.ExecuteReader();
            //if (dr.Read())
            //{
            //    con.Close();
            //    return Redirect("~/Home/Index");
            //}
            //else
            //{
            //    con.Close();
            //    return View();
            //}
            //Connnection();
            //con.Open();
            //SqlCommand cmd = new SqlCommand("select * from tb_Login where Name='" + username + "' and Password ='" + password + "'", con);
            //SqlDataAdapter sda = new SqlDataAdapter(cmd);
            //DataTable dt = new DataTable();
            //sda.Fill(dt);
            //cmd.ExecuteNonQuery();
            //if (dt.Rows.Count > 0)
            //{
            //    con.Close();
            //    return Redirect("~/Home/Index");
            //}
            //else
            //{
            //    con.Close();
            //    return View();
            //}

        }
    }
}