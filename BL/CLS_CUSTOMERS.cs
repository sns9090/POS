using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Data;
using System.Data.SqlClient;
namespace POS.BL
{
    class CLS_CUSTOMERS
    {
        public void ADD_CUSTOMER(string First_Name, string Last_Name, string Tel,
                        string Email, byte[] Picture, string Criterion)
        {
            DAL.DataAccessLayer DAL = new DAL.DataAccessLayer();
            DAL.Open();
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@First_Name", SqlDbType.VarChar,25);
            param[0].Value = First_Name;

            param[1] = new SqlParameter("@Last_Name", SqlDbType.VarChar, 25);
            param[1].Value = Last_Name;

            param[2] = new SqlParameter("@Tel", SqlDbType.NChar, 15);
            param[2].Value = Tel;

            param[3] = new SqlParameter("@Email", SqlDbType.VarChar,25);
            param[3].Value = Email;

            param[4] = new SqlParameter("@Picture", SqlDbType.Image);
            param[4].Value = Picture;


            param[5] = new SqlParameter("@Criterion", SqlDbType.VarChar,50);
            param[5].Value = Criterion;

            DAL.ExecuteCommand("ADD_CUSTOMER", param);
            DAL.Close();

        }


        public void EDIT_CUSTOMER(string First_Name, string Last_Name, string Tel,
                string Email, byte[] Picture, string Criterion, int ID)
        {
            DAL.DataAccessLayer DAL = new DAL.DataAccessLayer();
            DAL.Open();
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@First_Name", SqlDbType.VarChar, 25);
            param[0].Value = First_Name;

            param[1] = new SqlParameter("@Last_Name", SqlDbType.VarChar, 25);
            param[1].Value = Last_Name;

            param[2] = new SqlParameter("@Tel", SqlDbType.NChar, 15);
            param[2].Value = Tel;

            param[3] = new SqlParameter("@Email", SqlDbType.VarChar, 25);
            param[3].Value = Email;

            param[4] = new SqlParameter("@Picture", SqlDbType.Image);
            param[4].Value = Picture;


            param[5] = new SqlParameter("@Criterion", SqlDbType.VarChar, 50);
            param[5].Value = Criterion;


            param[6] = new SqlParameter("@ID", SqlDbType.Int);
            param[6].Value = ID;

            DAL.ExecuteCommand("EDIT_CUSTOMER", param);
            DAL.Close();

        }


        public void DELETE_CUSTOMER(int ID)
        {
            DAL.DataAccessLayer DAL = new DAL.DataAccessLayer();
            DAL.Open();
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@ID", SqlDbType.Int);
            param[0].Value = ID;

            DAL.ExecuteCommand("DELETE_CUSTOMER", param);
            DAL.Close();

        }

        public DataTable GET_ALL_CUSTOMERS()
        {
            DAL.DataAccessLayer DAL = new DAL.DataAccessLayer();
            DataTable Dt = new DataTable();
            Dt = DAL.SelectData("GET_ALL_CUSTOMERS", null);
            DAL.Close();
            return Dt;
        }

        public DataTable Search_Customer(string Criterion)
        {
            DAL.DataAccessLayer DAL = new DAL.DataAccessLayer();
            DataTable Dt = new DataTable();
            SqlParameter[] Param=new SqlParameter[1];
            Param[0] = new SqlParameter("@Criterion", SqlDbType.VarChar, 50);
            Param[0].Value = Criterion;
            Dt = DAL.SelectData("Search_Customer", Param);
            DAL.Close();
            return Dt;
        }


    }
}
