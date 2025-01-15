using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Threading;

namespace POS.BL
{
    public class Thrad_Class
    {
        BL.DAML daml = new BL.DAML();
        BL.Date_Validate dtv = new BL.Date_Validate();
        SqlConnection con = BL.DAML.con;//new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);

        public void thread1(string s1,string s2,string s3)
        {
            DataTable dttbld = daml.SELECT_QUIRY_only_retrn_dt("select itemno item_no,pkqty * qty item_qty from sales_dtl where invtype='" + s1 + "' and ref=" + s2 + " and branch='" + BL.CLS_Session.brno + "' and slcenter='" + s3 + "'");
            // daml.Exec_Query_only("bld_whbins_cost_all_by_tran @items_tran_tb=" + dttbld + "");
            using (SqlCommand cmd = new SqlCommand("bld_whbins_cost_all_by_tran"))
            {

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@items_tran_tb", dttbld);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                //   MessageBox.Show("Items Updated : " + cmd.Parameters["@NO_items_updated"].Value, "عدد الاصناف اللتي تم تحديثها", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

            }
        } 



    }
}
