using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace Books_Add_and_Modify_ADO_NET
{
    public static class BooksDB
    {
        public static SqlConnection GetConnection()
        {
            //Get Connection string from App.Config
            string strConn = ConfigurationManager.ConnectionStrings["ProductMaintenanceInClass.Properties.Settings.MMABOOKSConnectionString"].ConnectionString;
            SqlConnection Conn = new SqlConnection(strConn);
            //return connection string as strConn
            return Conn;
        }
    }
}
