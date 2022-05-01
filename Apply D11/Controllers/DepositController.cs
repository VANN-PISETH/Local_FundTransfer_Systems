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
    public class DepositController : Controller
    {
        // GET: Deposit
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Deposit()
        {
            return View();
        }
        private SqlConnection con;
        private void Connection()
        {
            string Connection = ConfigurationManager.ConnectionStrings["FTDEP"].ToString();
            con = new SqlConnection(Connection);
        }
        public JsonResult InsertTxn(Transaction txn)
        {
            if (txn.Total !=0 && txn.ExChange !=0)
            {
                ReturnStatus returnStatus = new ReturnStatus();
                DateTime TD = DateTime.Now;
                txn.TxnDate = TD;
                txn.TxnType = "Deposit";
                txn.FeePay = "Customer";
                txn.status = "Unapproved";
                Connection();
                SqlCommand cmd = new SqlCommand("sp_InsertTxn", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AccID", txn.AccID);
                cmd.Parameters.Add("@UserID", SqlDbType.VarChar).Value = txn.UserID;
                cmd.Parameters.Add("@TxnType", SqlDbType.VarChar).Value = txn.TxnType;
                cmd.Parameters.Add("@TxnDate", SqlDbType.DateTime).Value = txn.TxnDate;
                cmd.Parameters.Add("@Fee", SqlDbType.Decimal).Value = txn.Fee;
                cmd.Parameters.Add("@ExRate", SqlDbType.Decimal).Value = txn.ExChange;
                cmd.Parameters.Add("@CCY", SqlDbType.VarChar).Value = txn.CCY;
                cmd.Parameters.Add("@TxnAmt", SqlDbType.Decimal).Value = txn.TxnAmount;
                cmd.Parameters.Add("@Total", SqlDbType.Decimal).Value = txn.Total;
                cmd.Parameters.AddWithValue("@FeePay", txn.FeePay);
                cmd.Parameters.Add("@Status", SqlDbType.VarChar).Value = txn.status;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return Json(0);
            }
            else
            {
                return Json(1);
            }
        }

        public JsonResult GetValues(SenderCheck sen)
        {
            Connection();
            //  sen.CusName = "Tanjiro";
            //  sen.Gender = "Male";
            //  sen.Phone = "99999998";
            //  SqlCommand cmd = new SqlCommand("select A.AccID , C.Phone,C.Gender,C.Address,A.Balance from tb_Account as A join tb_Customer as C on A.CusID=C.CusID where C.CusName = '" + sen.CusName+"'", con);
            SqlCommand cmd = new SqlCommand("sp_ShowIF_Cus", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AccNum", sen.AccNum);
            SqlDataReader reader;
            con.Open();
            reader = cmd.ExecuteReader();
            if (reader != null)
            {
                while (reader.Read())
                {
                    // sen.AccID = reader.GetValue(0).ToString();
                    sen.CusName = reader.GetValue(0).ToString();
                    sen.Gender = reader.GetValue(2).ToString();
                    sen.Phone = reader.GetValue(1).ToString();
                    sen.Address = reader.GetValue(3).ToString();
                    sen.Balance = reader.GetValue(4).ToString();
                }
            }
            con.Close();
            var json = JsonConvert.SerializeObject(sen);
            return Json(json, JsonRequestBehavior.AllowGet);
        }


        public JsonResult CheckAccNum(SenderCheck Acc)
        {
            Connection();
            SqlCommand cmd = new SqlCommand("select * from tb_Account where AccNum = '" + Acc.AccNum + "'", con);
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


        public JsonResult CheckUserID(SenderCheck UID)
        {
            Connection();
            SqlCommand cmd = new SqlCommand("select * from tb_User where UserID = '" + UID.UserID + "'", con);
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
       
        public JsonResult Sender_Balance(BalanceCal bal)
        {

          
                Connection();
                SqlCommand cmd = new SqlCommand("update tb_Account set Balance=(Balance+@Total) where AccNum = '" + bal.SenderAccNum + "'", con);
                cmd.Parameters.AddWithValue("@Total", bal.TotalAmt);
                con.Open();
                cmd.ExecuteNonQuery();


                cmd = new SqlCommand("update tb_Account set Balance=(Balance-@Fee) where AccNum = '" + bal.SenderAccNum + "'", con);
                cmd.Parameters.AddWithValue("@Fee", bal.Fee);
                cmd.ExecuteNonQuery();
                con.Close();
            
            return Json("");
        }

        public JsonResult CalTotal(CalTotalAmt Tol)
        {

                Tol.TotalAmt = (Tol.Amt * Tol.ExRate) + Tol.Fee;
            
            var json = JsonConvert.SerializeObject(Tol);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FeeCharge(CalTotalAmt Fee)
        {
            if (Fee.Amt >= 1 && Fee.Amt < 5000)
            {
                Fee.Fee = 100;
            }
            else if (Fee.Amt >= 5000 && Fee.Amt < 10000)
            {
                Fee.Fee = 200;
            }
            else if (Fee.Amt >= 10000)
            {
                Fee.Fee = 500;
            }
            return Json(JsonConvert.SerializeObject(Fee), JsonRequestBehavior.AllowGet);
        }
    }
}
