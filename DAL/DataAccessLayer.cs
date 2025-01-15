using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace POS.DAL
{
    class DataAccessLayer
    {
        SqlConnection sqlconnection;
       //// public static string lang1;
        //This Constructor Inisialize the connection object
        public DataAccessLayer()
        {
            sqlconnection = BL.DAML.con;//

           // sqlconnection = new SqlConnection(@"server=SUPER-PC\INFOSOFT12;database=Product_DB;Integrated Security=True");
            //Data Source=TARIQ-PC;Initial Catalog=a;Integrated Security=True;Pooling=False
            ////////////////string mode = Properties.Settings.Default.Mode;
            ////////////////if (mode == "SQL")
            ////////////////{
            ////////////////    sqlconnection = new SqlConnection(@"Server=" + Properties.Settings.Default.Server + "; Database=" +
            ////////////////                                        Properties.Settings.Default.Database + "; Integrated Security=false; User ID=" +
            ////////////////                                        Properties.Settings.Default.ID + "; Password=" + Properties.Settings.Default.Password + "");
            ////////////////}
            ////////////////else
            ////////////////{
            ////////////////    sqlconnection = new SqlConnection(@"Server=" + Properties.Settings.Default.Server + "; Database=" + Properties.Settings.Default.Database + "; Integrated Security=true");
            ////////////////}
        }

        //Method to open the connection
        public void Open()
        {
            if (sqlconnection.State != ConnectionState.Open)
            {
                sqlconnection.Open();
            }
        }

        //Method to close the connection
        public void Close()
        {
            if (sqlconnection.State == ConnectionState.Open)
            {
                sqlconnection.Close();
            }
        }

        //Method To Read Data From Database
        public DataTable SelectData(string stored_procedure, SqlParameter[] param)
        {
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = stored_procedure;
            sqlcmd.Connection = sqlconnection;

            if (param != null)
            {
                for (int i = 0; i < param.Length; i++)
                {
                    sqlcmd.Parameters.Add(param[i]);
                }
            }
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        //Method to Insert, Update, and Delete Data From Database
        public void ExecuteCommand(string stored_procedure, SqlParameter[] param)
        {
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = stored_procedure;
            sqlcmd.Connection = sqlconnection;
            if (param != null)
            {
                sqlcmd.Parameters.AddRange(param);
            }
            sqlcmd.ExecuteNonQuery();
        }
    }
}
