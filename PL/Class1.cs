using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;



namespace POS
{
    class Class1
    {
         //public void MyMethod(string xxx,string yyy,DataTable dt)
        //{
            
           
        //    //DataSet ds3 = new DataSet();
        //    //da3.Fill(ds3, "0");
        //    //DataTable dt =new DataTable()
        //   // da3.Fill(dt);
        //    //return dt;
                 
            
            
        //}


        //static DataTable GetTable(string xxx, string yyy)
        //{
        //    SqlConnection con3 = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);

        //    SqlDataAdapter da3 = new SqlDataAdapter("select * from users where username ='" + xxx + "' and password='" + yyy + "'", con3);
        //    DataTable dt=new DataTable();
        //    da3.Fill(dt);

        //    if (dt.Rows.Count == 1)
        //    {
        //        MessageBox.Show("ok");
        //        return dt;
        //    }
        //    else
        //    {
        //        MessageBox.Show("no");
        //    }
        //}
        public DataTable GetDataTable(string xxx, string yyy)
        {
            SqlConnection con3 = BL.DAML.con;//
       
            SqlDataAdapter da3 = new SqlDataAdapter("select * from users where username ='" + xxx + "' and password='" + yyy + "'", con3);
            DataTable dt = new DataTable();
            da3.Fill(dt);
            

            if (dt.Rows.Count == 1)
            {
                MessageBox.Show("ok");
                
            }
            else
            {
                MessageBox.Show("no");
            }
            return dt;
        }
    }
}
