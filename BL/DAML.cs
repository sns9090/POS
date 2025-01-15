using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DAML
/// </summary>
namespace POS.BL
{
    public class DAML
    {
        public static string var1;
        public static string var2;
        public static string var3;
        public static SqlConnection con;
        public static SqlConnection con_asyn;
        public static SqlConnection con_ts;
        //public static string cmp_name;
        //public static string UserID;
      // SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString + ";Connection Timeout=60");



     //   SqlConnection con = new SqlConnection("Data Source=" + BL.CLS_Session.sqlserver + ";Initial Catalog=" + BL.CLS_Session.sqldb + ";User id=" + BL.CLS_Session.sqluser + ";Password=" + BL.CLS_Session.sqluserpass + ";Connection Timeout=60");
        SqlDataAdapter da;
        SqlCommand cmd;
        DataTable dt;

        public DAML()
        {
            //
            // TODO: Add constructor logic here
            //
            if (!File.Exists(Environment.CurrentDirectory + @"\local.txt"))
            {
                con = new SqlConnection("Data Source=" + BL.CLS_Session.sqlserver + ";Initial Catalog=" + BL.CLS_Session.sqldb + ";User id=" + BL.CLS_Session.sqluser + ";Password=" + BL.CLS_Session.sqluserpass + ";Connection Timeout=" + BL.CLS_Session.sqlcontimout + "");

                // con = new SqlConnection(@"Server=(LocalDB)\MSSQLLocalDB;Integrated Security=true;AttachDbFileName=|DataDirectory|\DBL\db01y2020.mdf");

                // con_asyn = new SqlConnection("Data Source=" + BL.CLS_Session.sqlserver + ";Initial Catalog=" + BL.CLS_Session.sqldb + ";User id=" + BL.CLS_Session.sqluser + ";Password=" + BL.CLS_Session.sqluserpass + ";Connection Timeout=" + BL.CLS_Session.sqlcontimout + ";Asynchronous Processing=true");
                con_asyn = new SqlConnection("Data Source=" + BL.CLS_Session.sqlserver + ";Initial Catalog=" + BL.CLS_Session.sqldb + ";User id=" + BL.CLS_Session.sqluser + ";Password=" + BL.CLS_Session.sqluserpass + ";Connection Timeout=" + BL.CLS_Session.sqlcontimout + "");
            }
            else
            {
                con = new SqlConnection(@"Data Source=" + BL.CLS_Session.sqlserver + @";AttachDbFilename=|DataDirectory|\DB\" + BL.CLS_Session.sqldb + ".mdf;User id=" + BL.CLS_Session.sqluser + ";Password=" + BL.CLS_Session.sqluserpass + ";Connection Timeout=" + BL.CLS_Session.sqlcontimout + "");
                con_asyn = new SqlConnection(@"Data Source=" + BL.CLS_Session.sqlserver + @";AttachDbFilename=|DataDirectory|\DB\" + BL.CLS_Session.sqldb + ".mdf;User id=" + BL.CLS_Session.sqluser + ";Password=" + BL.CLS_Session.sqluserpass + ";Connection Timeout=" + BL.CLS_Session.sqlcontimout + "");
            }

           // con_ts = new SqlConnection(@"Data Source=tree-soft.com,1499;Initial Catalog=db99y2099;User id=sa;Password=Sa@2099;Connection Timeout=" + BL.CLS_Session.sqlcontimout + "");
            con_ts = new SqlConnection(@"Data Source=sql5094.site4now.net;Initial Catalog=db_aa0123_2099;User id=db_aa0123_2099_admin;Password=Sa@123456;Connection Timeout=" + BL.CLS_Session.sqlcontimout + "");
       
        }

        //public void SqlCon_Msg()
        //{

        //    con.InfoMessage += OnInfoMessageGenerated;


        //}

        //public void OnInfoMessageGenerated(object sender, SqlInfoMessageEventArgs args)
        //{
        //    var3 = args.Message;

        //}

        //Method to open the connection
        public void SqlCon_Open()
        {
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
        }

        //Method to close the connection
        public void SqlCon_Close()
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }

        //Method to sql select query
        public DataTable SELECT_QUIRY_only_retrn_dt(string quiry)
        {
            SqlCon_Close();
            dt = new DataTable();
           // da = new SqlDataAdapter(quiry, con);
            cmd = new SqlCommand(quiry);
          //  cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 0;
            cmd.Connection = con;
            da = new SqlDataAdapter(cmd);
            da.Fill(dt);
           // SqlCon_Open();
            return dt;
        }

        public DataTable CMD_SELECT_QUIRY_only_retrn_dt(string quiry)
        {
            SqlCon_Close();
            //DataTable dt = new DataTable();
            cmd = new SqlCommand(quiry);
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 0;
            cmd.Connection = con;
           // SqlDataAdapter sda = new SqlDataAdapter(cmd);
            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            return dt;          
        }

        //Method to sql Insert Update Delete query
        public int Insert_Update_Delete_retrn_int(string quiry, bool st_pro)
        {
            int rowaffect = 0;
            cmd = new SqlCommand(quiry, con);
            if (st_pro == true)
            { 
                cmd.CommandType = CommandType.StoredProcedure;
            }
            cmd.CommandTimeout = 0;
            SqlCon_Open();
            rowaffect = cmd.ExecuteNonQuery();
           // SqlCon_Close();
            return rowaffect;
        }

        public void Insert_Update_Delete_Only(string quiry, bool is_sp)
        {
          //  int rowaffect = 0;
            cmd = new SqlCommand(quiry, con);
            if (is_sp == true)
            {
                cmd.CommandType = CommandType.StoredProcedure;
            }
            cmd.CommandTimeout = 0;
            SqlCon_Open();
          //  rowaffect = cmd.ExecuteNonQuery();
            cmd.ExecuteNonQuery();
          //  SqlCon_Close();
          //  return rowaffect;
        }

        public void ExecuteCommand_sp_with_param(string stored_procedure, SqlParameter[] param)
        {
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = stored_procedure;
            cmd.Connection = con;
            if (param != null)
            {
                cmd.Parameters.AddRange(param);
            }
            cmd.CommandTimeout = 0;
            SqlCon_Open();
            cmd.ExecuteNonQuery();
            SqlCon_Close();
        }

        public void ExecuteCommand_with_param(string cmdtext, SqlParameter[] param)
        {
             cmd = new SqlCommand();
            // sqlcmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = cmdtext;
            cmd.Connection = con;
            if (param != null)
            {
                cmd.Parameters.AddRange(param);
            }
            cmd.CommandTimeout = 0;
            SqlCon_Open();
            cmd.ExecuteNonQuery();
            SqlCon_Close();
        }

        public void Exec_Query_only(string quiry)
        {           
            cmd = new SqlCommand(quiry, con);
            //cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;
            SqlCon_Open();
            cmd.ExecuteNonQuery();
            SqlCon_Close();
        }


        //public int up_de_ExecuteCommand(string quiry, string para)
        //{

        //    int rowaffect = 0;


        //    var command = new SqlCommand(quiry, con);
        //    SqlParameter param = new SqlParameter();
        //    param.ParameterName = "@firstname";
        //    param.Value = para;
        //    command.Parameters.Add(param);
        //    rowaffect = command.ExecuteNonQuery();


        //    return rowaffect;


        //}

        //public void up1_de1_ExecuteCommand(string quiry)
        //{
        //    try
        //    {
        //        SqlCommand cmd = new SqlCommand(quiry, con);
        //        con.Open();
        //        cmd.ExecuteNonQuery();
        //    }
        //    catch
        //    {
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //}


        //public int udExecuteCommand(string quiry)
        //{
        //    int rowsAffected=0;
        //    try
        //    {
        //        //SqlCommand sqlcmd = new SqlCommand();
        //        //// sqlcmd.CommandType = CommandType.StoredProcedure;
        //        //sqlcmd.CommandText = quiry;
        //        //sqlcmd.Connection = con;
        //        ////if (param != null)
        //        ////{
        //        ////    sqlcmd.Parameters.AddRange(param);
        //        ////}
        //        //con.Open();
        //        //sqlcmd.ExecuteNonQuery();
        //        SqlCommand cmd = new SqlCommand(quiry,con);
        //        con.Open();
        //        // sqlcmd.ExecuteNonQuery();
        //        //conn.Open();
        //        rowsAffected = cmd.ExecuteNonQuery();          
        //    }
        //    catch
        //    {
        //    }
        //    finally
        //    {
        //        if (con != null)
        //        {
        //            con.Close(); 
        //        }
        //        //con.Close();
        //    }
        //    return rowsAffected;
        //}

        /*
        public DataTable SelectData(string stored_procedure, SqlParameter[] param)
        {
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = stored_procedure;
            sqlcmd.Connection = con;

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
            sqlcmd.Connection = con;
            if (param != null)
            {
                sqlcmd.Parameters.AddRange(param);
            }
            sqlcmd.ExecuteNonQuery();
        }
        */
    }
}
