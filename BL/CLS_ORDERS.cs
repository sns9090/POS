using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Data.SqlClient;
namespace POS.BL
{
    class CLS_ORDERS
    {
        public DataTable GET_ALL_COMPANY()
        {
            DAL.DataAccessLayer DAL = new DAL.DataAccessLayer();
            DataTable Dt = new DataTable();
            Dt = DAL.SelectData("GET_ALL_COMPANY", null);
            DAL.Close();
            return Dt;
        }
        public DataTable GET_LAST_ORDER_ID()
        {
            DAL.DataAccessLayer DAL = new DAL.DataAccessLayer();
            DataTable Dt = new DataTable();
            Dt = DAL.SelectData("GET_LAST_ORDER_ID", null);
            DAL.Close();
            return Dt;
        }

        public DataTable GET_LAST_ORDER_FOR_PRINT()
        {
            DAL.DataAccessLayer DAL = new DAL.DataAccessLayer();
            DataTable Dt = new DataTable();
            Dt = DAL.SelectData("GET_LAST_ORDER_FOR_PRINT", null);
            DAL.Close();
            return Dt;
        }


        public void ADD_ORDER(int ID_ORDER, DateTime DATE_ORDER, int CUSTOMER_ID,
                              string DESCRIPTION_ORDER, string SALESMAN, string COMPANY, string CARTYPE, int MODEL, string CARNUMBER)
        {
            DAL.DataAccessLayer DAL = new DAL.DataAccessLayer();
            DAL.Open();
            SqlParameter[] param = new SqlParameter[9];
            param[0] = new SqlParameter("@ID_ORDER", SqlDbType.Int);
            param[0].Value = ID_ORDER;

            param[1] = new SqlParameter("@DATE_ORDER", SqlDbType.DateTime);
            param[1].Value = DATE_ORDER;

            param[2] = new SqlParameter("@CUSTOMER_ID", SqlDbType.Int);
            param[2].Value = CUSTOMER_ID;

            param[3] = new SqlParameter("@DESCRIPTION_ORDER", SqlDbType.VarChar, 250);
            param[3].Value = DESCRIPTION_ORDER;

            param[4] = new SqlParameter("@SALESMAN", SqlDbType.VarChar,50);
            param[4].Value = SALESMAN;

            param[5] = new SqlParameter("@COMPANY", SqlDbType.VarChar, 50);
            param[5].Value = COMPANY;

            param[6] = new SqlParameter("@CARTYPE", SqlDbType.VarChar, 50);
            param[6].Value = CARTYPE;

            param[7] = new SqlParameter("@MODEL", SqlDbType.Int);
            param[7].Value = MODEL;

            param[8] = new SqlParameter("@CARNUMBER", SqlDbType.VarChar, 50);
            param[8].Value = CARNUMBER;
            DAL.ExecuteCommand("ADD_ORDER", param);
            DAL.Close();

        }



        public void ADD_ORDER_DETAILS(string ID_PRODUCT, int ID_ORDER, int QTE,
                      string PRICE, double DISCOUNT, string AMOUNT, string TOTAL_AMOUNT)
        {
            DAL.DataAccessLayer DAL = new DAL.DataAccessLayer();
            DAL.Open();
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@ID_PRODUCT", SqlDbType.VarChar, 30);
            param[0].Value = ID_PRODUCT;

            param[1] = new SqlParameter("@ID_ORDER", SqlDbType.Int);
            param[1].Value = ID_ORDER;

            param[2] = new SqlParameter("@QTE", SqlDbType.Int);
            param[2].Value = QTE;

            param[3] = new SqlParameter("@PRICE", SqlDbType.VarChar, 50);
            param[3].Value = PRICE;

            param[4] = new SqlParameter("@DISCOUNT", SqlDbType.Float);
            param[4].Value = DISCOUNT;

            param[5] = new SqlParameter("@AMOUNT", SqlDbType.VarChar, 50);
            param[5].Value = AMOUNT;

            param[6] = new SqlParameter("@TOTAL_AMOUNT", SqlDbType.VarChar, 50);
            param[6].Value = TOTAL_AMOUNT;

            DAL.ExecuteCommand("ADD_ORDER_DETAILS", param);
            DAL.Close();

        }




        public DataTable VerifyQty(string ID_PRODUCT, int Qty_Entered)
        {
            DAL.DataAccessLayer DAL = new DAL.DataAccessLayer();
            DataTable Dt = new DataTable();
            SqlParameter[] param = new SqlParameter[2];

            param[0] = new SqlParameter("@ID_PRODUCT", SqlDbType.VarChar, 30);
            param[0].Value = ID_PRODUCT;

            param[1] = new SqlParameter("@Qty_Entered", SqlDbType.Int);
            param[1].Value = Qty_Entered;

            Dt = DAL.SelectData("VerifyQty", param);
            DAL.Close();
            return Dt;
        }


        public DataTable GetOrderDetails(int ID_ORDER)
        {
            DAL.DataAccessLayer DAL = new DAL.DataAccessLayer();
            DataTable Dt = new DataTable();
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@ID_ORDER", SqlDbType.Int);
            param[0].Value = ID_ORDER;


            Dt = DAL.SelectData("GetOrderDetails", param);
            DAL.Close();
            return Dt;
        }


        public DataTable SearchOrders(string Criterion)
        {
            DAL.DataAccessLayer DAL = new DAL.DataAccessLayer();
            DataTable Dt = new DataTable();
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@Criterion", SqlDbType.VarChar,50);
            param[0].Value = Criterion;


            Dt = DAL.SelectData("SearchOrders", param);
            DAL.Close();
            return Dt;
        }

    }
}
