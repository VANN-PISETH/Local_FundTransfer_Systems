using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.ComponentModel.DataAnnotations;


namespace Apply_D11.Models
{
    public class Connection
    {
        // connection to database
        private SqlConnection con;
        private void Connnection()
        {
            string Connection = ConfigurationManager.ConnectionStrings["FTDEP"].ToString();
            con = new SqlConnection(Connection);
        }
       
    }
   
}