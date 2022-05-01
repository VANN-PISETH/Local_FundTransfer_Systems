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

namespace Apply_D11.Controllers
{
    public class ProcessController : Controller
    {
        ReturnStatus returnStatus = new ReturnStatus();
        //connection to database
        private SqlConnection con;
       // FTDEP db = new FTDEP();
        private void Connection()
        {
            string Connection = ConfigurationManager.ConnectionStrings["FTDEP"].ToString();
            con = new SqlConnection(Connection);
        }
        //SqlCommand cmd = new SqlCommand();
        // GET: Process
        public ActionResult Index()
        {
            return View();
        }
      
        public ActionResult FundTransfer()
        {
          //  ShowTotal(Tol);
            return View();
        }
        [HttpPost]
        public ActionResult FundTransfer(Transaction txn)
        {

            return View();
        }
        //[HttpGet]
        // public ActionResult FundTransfer(SenderCheck sen)
        // {
        //     CheckAcc(sen);

        //     //Radio(txn,frm);
        //     return View();
        // }

        //private string Radio(Transaction txn, FormCollection frm)
        //{
        //    string Fee = frm["rdoFee"].ToString();
        //    return txn.FeePay = Fee;
        //}

        public JsonResult InsertTxn (Transaction txn)
        {
            if(txn.SenderBalance > txn.Total && txn.Total !=0 && txn.RecieverBalance !=0 && txn.ExChange !=0)
            {
                ReturnStatus returnStatus = new ReturnStatus();
                DateTime TD = DateTime.Now;
                txn.TxnDate = TD;
                txn.TxnType = "Transfer";
                txn.status = "Unapproved";
                Connection();
                SqlCommand cmd = new SqlCommand("sp_InsertTxn", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AccID", txn.AccID);
                cmd.Parameters.Add("@UserID", SqlDbType.VarChar).Value = txn.UserID;
                cmd.Parameters.Add("@TxnType", SqlDbType.VarChar).Value = txn.TxnType;
                // cmd.Parameters.Add("@TxnDate", SqlDbType.DateTime).Value = TD.ToString("yyyy-MM-dd");
                cmd.Parameters.Add("@TxnDate", SqlDbType.DateTime).Value = txn.TxnDate;
                cmd.Parameters.Add("@Fee", SqlDbType.Decimal).Value = txn.Fee;
                cmd.Parameters.Add("@ExRate", SqlDbType.Decimal).Value = txn.ExChange;
                cmd.Parameters.Add("@CCY", SqlDbType.VarChar).Value = txn.CCY;
                cmd.Parameters.Add("@TxnAmt", SqlDbType.Decimal).Value = txn.TxnAmount;
                cmd.Parameters.Add("@Total", SqlDbType.Decimal).Value = txn.Total;
                cmd.Parameters.AddWithValue("@FeePay", txn.FeePay);
                cmd.Parameters.Add("@Status", SqlDbType.VarChar).Value = txn.status;
                con.Open();
                //int i = cmd.ExecuteNonQuery();
                cmd.ExecuteNonQuery();
                con.Close();
                return Json(0);
            }
            else 
            {
                return Json(1);
            }


            //if (i > 0)
            //{
            //    returnStatus.ErrCode = 0;
            //    returnStatus.ErrMsg = "Successfully";
            //}
            //else
            //{
            //    returnStatus.ErrCode = 99999;
            //    returnStatus.ErrMsg = "UserCode is used";
            //}
            //return View(returnStatus);
            return Json(2);

        }

        //private void CheckAcc(SenderCheck sen)
        //{
        //    Connection();
        //     sen.CusName = "Tanjiro";
        //    //  sen.Gender = "Male";
        //    //  sen.Phone = "99999998";

        //    SqlCommand cmd = new SqlCommand("sp_Showtb_Cus", con);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.AddWithValue("@CusName", sen.CusName);
        //    SqlDataReader reader;
        //    con.Open();
        //    reader = cmd.ExecuteReader();
        //    while (reader.Read())
        //    {
        //        sen.CusID = reader.GetValue(0).ToString();
        //        sen.Gender = reader.GetValue(2).ToString();
        //        sen.Phone = reader.GetValue(3).ToString();
        //        sen.Address = reader.GetValue(4).ToString();
        //    }
        //    con.Close();
        //}
        //public JsonResult GetValues2(SenderCheck sen)
        //{
        //    Connection();
        //    // sen.CusName = "Tanjiro";
        //    //  sen.Gender = "Male";
        //    //  sen.Phone = "99999998";

        //    SqlCommand cmd = new SqlCommand("sp_Showtb_Cus", con);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.AddWithValue("@CusName", sen.CusName);
        //    SqlDataReader reader;
        //    con.Open();
        //    reader = cmd.ExecuteReader();
        //    try
        //    {
        //        while (reader.Read())
        //        {
        //            sen.CusID = reader.GetValue(0).ToString();
        //            sen.Gender = reader.GetValue(2).ToString();
        //            sen.Phone = reader.GetValue(3).ToString();
        //            sen.Address = reader.GetValue(4).ToString();
        //        }
        //        con.Close();

        //        var json = JsonConvert.SerializeObject(sen);
        //        return Json(json, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    con.Close();

        //}
        public JsonResult GetValues(SenderCheck sen)
        {
            Connection();
          
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
            return Json(json,JsonRequestBehavior.AllowGet);
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
        //public JsonResult Reciever_Balance (BalanceCal bal2)
        //{
        //    Connection();
        //    SqlCommand cmd = new SqlCommand("update tb_Account set Balance=(Balance+@Amt) where AccNum = '" + bal2.ReceiverAccNum + "'", con);
        //    cmd.Parameters.AddWithValue("@Amt", bal2.Amt);
        //    con.Open();
        //    cmd.ExecuteNonQuery();
        //    con.Close();
        //    return Json("Reciever");
        //}

        public JsonResult Sender_Balance (BalanceCal bal)
        {
           

            if (bal.TotalAmt > bal.SenderBalance && bal.SenderBalance !=0)
            {
                return Json(1);

            }
            else if(bal.TotalAmt < bal.SenderBalance)
            {

                Connection();
                SqlCommand cmd = new SqlCommand("update tb_Account set Balance=(Balance+@Amt) where AccNum = '" + bal.ReceiverAccNum + "'", con);
                cmd.Parameters.AddWithValue("@Amt", bal.Amt);
                con.Open();
                cmd.ExecuteNonQuery();



                cmd = new SqlCommand("update tb_Account set Balance=(Balance-@TotalAmt) where AccNum = '" + bal.SenderAccNum + "'", con);
                cmd.Parameters.AddWithValue("@TotalAmt", bal.TotalAmt);
                cmd.ExecuteNonQuery();
                con.Close();
            }   
            return Json("");
        }

        public JsonResult CalTotal (CalTotalAmt Tol)
        {
            // Tol.TotalAmt = (Tol.Amt * Tol.ExRate) + Tol.Fee;
            if (Tol.Rdoreciever == "Reciever")
            {
                Connection();
                SqlCommand cmd = new SqlCommand("update tb_Account set Balance=(Balance-@Fee) where AccNum = '" + Tol.ReceiverAccNumber + "'", con);
                cmd.Parameters.AddWithValue("@Fee", Tol.Fee);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Tol.TotalAmt = (Tol.Amt * Tol.ExRate);
            }
            else if (Tol.Rdosender == "Sender")
            {
                Tol.TotalAmt = (Tol.Amt * Tol.ExRate) + Tol.Fee;
            }
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