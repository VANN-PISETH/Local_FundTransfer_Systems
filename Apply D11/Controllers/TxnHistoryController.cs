using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using Apply_D11.Models;
using Newtonsoft.Json;
using Microsoft.SqlServer.Server;

namespace Apply_D11.Controllers
{
    public class TxnHistoryController : Controller
    {
        // GET: TxnHistory
        public ActionResult Index()
        {
            return View();

        }
        public ActionResult ShowHistory()
        {
            return View();
        }
        private void Connnection()
        {
            string Connection = ConfigurationManager.ConnectionStrings["FTDEP"].ToString();
            con = new SqlConnection(Connection);
        }

        private SqlConnection con;
        public JsonResult showtxn(Transaction txn)
        {
            Connnection();
            List<Transaction> list = new List<Transaction>();
            string query = string.Format("select TxnID,UserID,AccID,TxnType,FORMAT(SA.TxnDate,'dd/MM/yyyy') as 'Date',Total,CCY,status from tb_testTxn as SA ");
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(
                        new Transaction
                        {
                            TxnID = int.Parse(reader["TxnID"].ToString()),
                            UserID = reader["UserID"].ToString(),
                            AccID = reader["AccID"].ToString(),
                            TxnType = reader["TxnType"].ToString(),
                            //TxnDate = DateTime.Parse(reader["Date"].ToString()),
                            DateTxn = reader["Date"].ToString(),
                            //   Fee = decimal.Parse(reader["Fee"].ToString()),
                            //  ExChange = decimal.Parse(reader["ExRate"].ToString()),
                            //  CCY = reader["CCY"].ToString(),
                            //  TxnAmount = decimal.Parse(reader["TxnAmt"].ToString()),                          
                            Total = decimal.Parse(reader["Total"].ToString()),
                            CCY = reader["CCY"].ToString(),
                            status = reader["status"].ToString(),
                            //Fee = decimal.Parse(reader["Fee"].ToString())
                        }
                    );
                }
            }
            return Json(list, JsonRequestBehavior.AllowGet);

        }
    }


}
