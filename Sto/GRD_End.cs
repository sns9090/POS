using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Data.SqlClient;
using System.Threading;

namespace POS.Sto
{
    public partial class GRD_End : BaseForm
    {
        SqlConnection con = BL.DAML.con;
        int ref_max;
        BL.Date_Validate dtv = new BL.Date_Validate();
        BL.DAML daml = new BL.DAML();
        SqlConnection consyn = BL.DAML.con_asyn;
        DataTable dttbld;
        public GRD_End()
        {
            InitializeComponent();
        }

        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData)
        {
            if (msg.WParam.ToInt32() == (int)Keys.Enter)
            {
                SendKeys.Send("{Tab}");
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void GRD_Start_Load(object sender, EventArgs e)
        {
            txt_mdate.Text = DateTime.Now.ToString("dd-MM-yyyy", new CultureInfo("en-US", false));
            txt_hdate.Text = DateTime.Now.ToString("dd-MM-yyyy", new CultureInfo("ar-SA", false));

            string sl = BL.CLS_Session.stkey.Replace(" ", "','").Remove(0, 2) + "'";
            // MessageBox.Show(sl);
            DataTable dtslctr = daml.SELECT_QUIRY_only_retrn_dt("select wh_no,wh_name from whouses where wh_brno + wh_no in(" + sl + ") and wh_brno='" + BL.CLS_Session.brno + "'");



            cmb_ftwh.DataSource = dtslctr;
            cmb_ftwh.DisplayMember = "wh_name";
            cmb_ftwh.ValueMember = "wh_no";
            cmb_ftwh.SelectedIndex = -1;
        }

        private void txt_mdate_Leave(object sender, EventArgs e)
        {
            try
            {
                string datestr = txt_mdate.Text.ToString().Replace("-", "").Trim();
                if (datestr.Length == 0)
                {
                    txt_mdate.Text = DateTime.Now.ToString("dd", new CultureInfo("en-US", false)) + DateTime.Now.ToString("MM", new CultureInfo("en-US", false)) + DateTime.Now.ToString("yyyy", new CultureInfo("en-US", false));
                    txt_hdate.Text = convert_to_hdate(txt_hdate.Text.ToString());
                }
                else
                {
                    if (datestr.Length == 2)
                    {
                        txt_mdate.Text = datestr + DateTime.Now.ToString("MM", new CultureInfo("en-US", false)) + DateTime.Now.ToString("yyyy", new CultureInfo("en-US", false));
                        txt_hdate.Text = convert_to_hdate(txt_mdate.Text.ToString());
                    }
                    else
                    {
                        if (datestr.Length == 4)
                        {
                            txt_mdate.Text = datestr + DateTime.Now.ToString("yyyy", new CultureInfo("en-US", false));
                            txt_hdate.Text = convert_to_hdate(txt_mdate.Text.ToString());
                        }
                        else
                        {
                            txt_hdate.Text = convert_to_hdate(txt_mdate.Text.ToString());
                        }
                    }
                }
            }
            catch { }
        }

        private void txt_hdate_Leave(object sender, EventArgs e)
        {
            try
            {
                string datestr2 = txt_hdate.Text.ToString().Replace("-", "").Trim();
                if (datestr2.Length == 0)
                {
                    txt_hdate.Text = DateTime.Now.ToString("dd", new CultureInfo("ar-SA", false)) + DateTime.Now.ToString("MM", new CultureInfo("ar-SA", false)) + DateTime.Now.ToString("yyyy", new CultureInfo("ar-SA", false));
                    txt_mdate.Text = convert_to_mdate(txt_hdate.Text.ToString());
                }
                else
                {
                    if (datestr2.Length == 2)
                    {
                        txt_hdate.Text = datestr2 + DateTime.Now.ToString("MM", new CultureInfo("ar-SA", false)) + DateTime.Now.ToString("yyyy", new CultureInfo("ar-SA", false));
                        txt_mdate.Text = convert_to_mdate(txt_hdate.Text.ToString());
                    }
                    else
                    {
                        if (datestr2.Length == 4)
                        {
                            txt_hdate.Text = datestr2 + DateTime.Now.ToString("yyyy", new CultureInfo("ar-SA", false));
                            txt_mdate.Text = convert_to_mdate(txt_hdate.Text.ToString());
                        }
                        else
                        {
                            txt_mdate.Text = convert_to_mdate(txt_hdate.Text.ToString());
                        }
                    }
                }
            }
            catch { }
        }

        private void txt_mdate_Validating(object sender, CancelEventArgs e)
        {
            DateTime rs;

            CultureInfo ci = new CultureInfo("en-US", false);

            if (!DateTime.TryParseExact(this.txt_mdate.Text, "dd-MM-yyyy", ci, DateTimeStyles.None, out rs))
            {

                MessageBox.Show("Date Error", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
                txt_mdate.Focus();

            }
            if (Convert.ToInt32(dtv.convert_to_yyyyDDdd(txt_mdate.Text)) < Convert.ToInt32(dtv.convert_to_yyyyDDdd(BL.CLS_Session.start_dt)) || Convert.ToInt32(dtv.convert_to_yyyyDDdd(txt_mdate.Text)) > Convert.ToInt32(dtv.convert_to_yyyyDDdd(BL.CLS_Session.end_dt)))
            {

                MessageBox.Show("التاريخ المدخل خارج السنة", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;

            }
        }

        private void txt_hdate_Validating(object sender, CancelEventArgs e)
        {
            DateTime rs;

            CultureInfo ci = new CultureInfo("ar-SA", false);

            if (!DateTime.TryParseExact(this.txt_hdate.Text, "dd-MM-yyyy", ci, DateTimeStyles.None, out rs))
            {

                MessageBox.Show("Date Error", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
                txt_hdate.Focus();

            }
            if (Convert.ToInt32(dtv.convert_to_yyyyDDdd(txt_hdate.Text)) < Convert.ToInt32(dtv.convert_to_yyyyDDdd(dtv.convert_to_hdate(BL.CLS_Session.start_dt))) || Convert.ToInt32(dtv.convert_to_yyyyDDdd(txt_hdate.Text)) > Convert.ToInt32(dtv.convert_to_yyyyDDdd(dtv.convert_to_hdate(BL.CLS_Session.end_dt))))
            {

                MessageBox.Show("التاريخ المدخل خارج السنة", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;

            }
        }

        private string convert_to_mdate(string input)
        {
            input = (txt_hdate.Text.Replace("-", "").Substring(4, 4)) + "-" + txt_hdate.Text.Replace("-", "").Substring(2, 2) + "-" + txt_hdate.Text.Replace("-", "").Substring(0, 2);

            DateTime dt = Convert.ToDateTime(input, new CultureInfo("ar-SA", false));
            //  DateTime dt = Convert.ToDateTime(txt_mdate.Text.ToString("yyyy-MM-dd"), new CultureInfo("en-US"));
            // txt_hdate.Text = dt.ToString("ddMMyyyy", new CultureInfo("ar-SA"));

            input = dt.ToString("dd-MM-yyyy", new CultureInfo("en-US", false));

            return input;

        }

        private string convert_to_hdate(string input)
        {
            input = (txt_mdate.Text.Replace("-", "").Trim().Substring(4, 4)) + "-" + txt_mdate.Text.Replace("-", "").Substring(2, 2) + "-" + txt_mdate.Text.Replace("-", "").Substring(0, 2);

            DateTime dt = Convert.ToDateTime(input, new CultureInfo("en-US", false));
            //  DateTime dt = Convert.ToDateTime(txt_mdate.Text.ToString("yyyy-MM-dd"), new CultureInfo("en-US"));
            // txt_hdate.Text = dt.ToString("ddMMyyyy", new CultureInfo("ar-SA"));

            input = dt.ToString("dd-MM-yyyy", new CultureInfo("ar-SA", false));

            return input;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string cond = checkBox1.Checked ? " and ttqty > 0 " : "";
            if (cmb_ftwh.SelectedIndex == -1)
            {
                MessageBox.Show("يجب اختيار مخزن للجرد", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmb_ftwh.Focus();
                return;

            }
            if (string.IsNullOrEmpty(txt_desc.Text))
            {
                MessageBox.Show("يجب اخال وصف", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_desc.Focus();
                return;

            }

            DataTable dt = daml.SELECT_QUIRY_only_retrn_dt("select * from ttk_hdr where ttbranch='" + BL.CLS_Session.brno + "' and ttwhno='" + cmb_ftwh.SelectedValue + "'");

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("لا يوجد جرد لتسويته", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    DialogResult result = MessageBox.Show("لا يمكن التراجع عن تسوية الجرد .. هل تريد المتابعة", "Confirmation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        string mdate, hdate;
                        mdate = txt_mdate.Text.Replace("-", "").Substring(4, 4) + txt_mdate.Text.Replace("-", "").Substring(2, 2) + txt_mdate.Text.Replace("-", "").Substring(0, 2);
                        hdate = txt_hdate.Text.Replace("-", "").Substring(4, 4) + txt_hdate.Text.Replace("-", "").Substring(2, 2) + txt_hdate.Text.Replace("-", "").Substring(0, 2);

                        DataTable dt2 = daml.SELECT_QUIRY_only_retrn_dt("select isnull(max(ref),0) from sto_hdr where trtype='35' and branch='" + BL.CLS_Session.brno + "'");

                        DataTable dt_tthdr = daml.SELECT_QUIRY_only_retrn_dt("select round(sum(((ttqty-mqty)*lcost)),4) from ttk_dtl where ttbranch='" + BL.CLS_Session.brno + "' and whno='" + cmb_ftwh.SelectedValue + "' " + cond + "");
                        DataTable dt_ttdtl = daml.SELECT_QUIRY_only_retrn_dt("select * from ttk_dtl where ttbranch='" + BL.CLS_Session.brno + "' and whno='" + cmb_ftwh.SelectedValue + "' " + cond + " order by srl_no");
                        ref_max = Convert.ToInt32(dt2.Rows[0][0]) + 1;

                        int exexcuteds = daml.Insert_Update_Delete_retrn_int("INSERT INTO sto_hdr(branch,trtype,ref,trmdate, trhdate,text,remarks,entries,amnttl,costttl,usrid,supno,whno,towhno,posted) "
                                      + " VALUES('" + BL.CLS_Session.brno + "','35'," + ref_max + ",'" + mdate + "','" + hdate + "','" + txt_desc.Text + "',' '," + (dt_ttdtl.Rows.Count ) + "," + dt_tthdr.Rows[0][0] + "," + 0 + ",'" + BL.CLS_Session.UserName + "',' ','" + cmb_ftwh.SelectedValue + "','" + cmb_ftwh.SelectedValue + "',1)", false);
                        progressBar1.Visible = true;
                        progressBar1.Minimum = 0;
                        progressBar1.Maximum = dt_ttdtl.Rows.Count;
                        foreach (DataRow row in dt_ttdtl.Rows)
                        {
                            // SqlDataAdapter daa = new SqlDataAdapter("select max(ref) as eee from DB.dbo.sales_hdr", con2);
                            /*
                            SqlDataAdapter daa = new SqlDataAdapter("select max(ref) as eee from sales_hdr", con2);
                            DataSet dss = new DataSet();
                            daa.Fill(dss, "0");
            */
                           
                                // using (SqlCommand cmd = new SqlCommand("INSERT INTO DB.dbo.sales_dtl (ref, barcod, name, unit, price) VALUES(@r1,@c1,@c2,@c3,@c4)", con2))
                                using (SqlCommand cmd = new SqlCommand("INSERT INTO sto_dtl(branch,trtype,ref,trmdate, trhdate, itemno, qty, lcost, pack, pkqty,folio,whno,towhno,barcode,lprice) VALUES(@a1,@a3,@a4,@a5,@a6,@a7,@a8,@a9,@a10,@a11,@a12,@a14,@a15,@a16,@a17)", con))
                                {
                                    cmd.Parameters.AddWithValue("@a1", BL.CLS_Session.brno);
                                    //   cmd.Parameters.AddWithValue("@a2", cmb_salctr.SelectedValue);
                                    cmd.Parameters.AddWithValue("@a3", "35");
                                    cmd.Parameters.AddWithValue("@a4",  ref_max );
                                    // cmd.Parameters.AddWithValue("@a4", row.Cells[2].Value);
                                    cmd.Parameters.AddWithValue("@a5", mdate);
                                    // cmd.Parameters.AddWithValue("@a5", (string.IsNullOrEmpty(row.Cells[3].Value.ToString())? 0 : Convert.ToDouble(row.Cells[3].Value)));
                                    cmd.Parameters.AddWithValue("@a6", hdate);
                                    cmd.Parameters.AddWithValue("@a7", row[8]);
                                    cmd.Parameters.AddWithValue("@a8",Convert.ToDouble(row[4]) - Convert.ToDouble(row[9]));
                                    cmd.Parameters.AddWithValue("@a9", row[11]);
                                    cmd.Parameters.AddWithValue("@a10", row[14]);
                                    cmd.Parameters.AddWithValue("@a11", 1 );
                                    cmd.Parameters.AddWithValue("@a12", row[6]);
                                    //  cmd.Parameters.AddWithValue("@a13", Convert.ToDouble(row.Cells[6].Value));
                                    cmd.Parameters.AddWithValue("@a14", cmb_ftwh.SelectedValue);
                                    cmd.Parameters.AddWithValue("@a15", cmb_ftwh.SelectedValue);
                                    cmd.Parameters.AddWithValue("@a16", row[13]);
                                    cmd.Parameters.AddWithValue("@a17", row[11]);
                                    //  cmd.Parameters.AddWithValue("@c7", Convert.ToInt32(lblcashir.Text));
                                    //if (row.Cells[7].Value != null)
                                    // { cmd.Parameters.AddWithValue("@c8", row.Cells[7].Value); }
                                    // cmd.Parameters.AddWithValue("@c9", comp_id);
                                    // cmd.Parameters.AddWithValue("@c10", row.Cells[1].Value);
                                    //con.Open();
                                    if (con.State != ConnectionState.Open)
                                    {
                                        con.Open();
                                    }
                                    cmd.ExecuteNonQuery();
                                    //con.Close();
                                    //if (con.State == ConnectionState.Open)
                                    //{
                                    //    con.Close();
                                    //}
                                    // lblref.Text = Convert.ToString(dss.Tables[0].Rows[0][0]);
                                    progressBar1.Increment(1);
                                
                            }
                        }
                        //DataTable dtwh = daml.SELECT_QUIRY_only_retrn_dt("select a.br_no,a.wh_no,b.item_name,a.item_no,a.qty,a.lcost,b.item_barcode,b.item_unit,b.unit2,b.uq2,b.item_price  from whbins a,items b where a.item_no=b.item_no and a.br_no='" + BL.CLS_Session.brno + "' and a.wh_no='" + cmb_ftwh.SelectedValue + "'");

                        //int exeh = daml.Insert_Update_Delete_retrn_int("insert into ttk_hdr ([ttbranch],[ttwhno],[ttmdate],[tthdate],[ttcntr]) VALUES('" + BL.CLS_Session.brno + "','" + cmb_ftwh.SelectedValue + "','" + dtv.convert_to_yyyyDDdd(txt_mdate.Text) + "','" + dtv.convert_to_yyyyDDdd(txt_hdate.Text) + "'," + dtwh.Rows.Count + ")", false);

                        //for (int i = 0; i < dtwh.Rows.Count; i++)
                        //{
                        //    string StrQuery = " MERGE ttk_dtl as t"
                        //                    + " USING (select '" + dtwh.Rows[i][0].ToString() + "' as ttbranch, '" + dtwh.Rows[i][1].ToString() + "' as whno,'" + dtwh.Rows[i][2].ToString() + "' as name,' ' as binno,0 as ttqty,0 as ttstatus," + (i + 1) + " as srl_no,' ' as mclass, '" + dtwh.Rows[i][3].ToString() + "' as itemno," + dtwh.Rows[i][4].ToString() + " as mqty,' ' as expdate," + dtwh.Rows[i][5].ToString() + " as lcost,0 as fcost,'" + dtwh.Rows[i][6].ToString() + "' as barcode," + dtwh.Rows[i][7].ToString() + " as pack0," + dtwh.Rows[i][8].ToString() + " as pack1," + dtwh.Rows[i][9].ToString() + " as pkqty1,0 as nobin," + dtwh.Rows[i][10].ToString() + " as price) as s"
                        //                    + " ON T.itemno=S.itemno"
                        //                    + " WHEN MATCHED THEN"
                        //                    + " UPDATE SET T.mqty = S.mqty"
                        //                    + " WHEN NOT MATCHED THEN"
                        //                    + " INSERT VALUES(s.ttbranch, s.whno, s.name,s.binno,s.ttqty,s.ttstatus,s.srl_no,s.mclass,s.itemno,s.mqty,s.expdate,s.lcost,s.fcost,s.barcode,s.pack0,s.pack1,s.pkqty1,s.nobin,s.price);";
                        //    try
                        //    {
                        //        //SqlConnection conn = new SqlConnection();

                        //        /*
                        //        using (SqlCommand comm = new SqlCommand(StrQuery, con))
                        //        {
                        //           // con.Open();
                        //            comm.ExecuteNonQuery();
                        //           // con.Close();

                        //        }
                        //         */

                        //        daml.Insert_Update_Delete_retrn_int(StrQuery, false);

                        //     //   progressBar1.Value = i;
                        //     //   pfr.progressBar1.Value = i;

                        dttbld = daml.SELECT_QUIRY_only_retrn_dt("select itemno item_no,0 item_qty from ttk_dtl where ttbranch='" + BL.CLS_Session.brno + "' and whno='" + cmb_ftwh.SelectedValue + "' " + cond + " group by itemno");
                        //// daml.Exec_Query_only("bld_whbins_cost_all_by_tran @items_tran_tb=" + dttbld + "");
                        //using (SqlCommand cmd = new SqlCommand("bld_whbins_cost_all_by_tran"))
                        //{

                        //    cmd.CommandType = CommandType.StoredProcedure;
                        //    cmd.Connection = con;
                        //    cmd.Parameters.AddWithValue("@items_tran_tb", dttbld);
                        //    con.Open();
                        //    cmd.ExecuteNonQuery();
                        //    con.Close();

                        //    //   MessageBox.Show("Items Updated : " + cmd.Parameters["@NO_items_updated"].Value, "عدد الاصناف اللتي تم تحديثها", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                        //}


                        //daml.Exec_Query_only("delete from ttk_hdr where ttbranch='" + BL.CLS_Session.brno + "' and ttwhno='" + cmb_ftwh.SelectedValue + "'");
                        MessageBox.Show("تم تسوية الجرد بنجاح" + Environment.NewLine + " حركة رقم " + ref_max.ToString(), "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        btn_start.Enabled = false;
                        button2.Enabled = false;

                        Thread t = new Thread(() => thread1());
                        t.Start();

                    }
                    else if (result == DialogResult.No)
                    {
                        //...
                    }
                    else
                    {
                        //...
                    }



                }
                catch (Exception ex)
                {
                     MessageBox.Show(ex.Message);
                }

            }
        }

        public void thread1()
        {
            try
            {
                // SqlConnection con2 = BL.DAML.con_asyn;// new SqlConnection(GetConnectionString());
                // con2 = BL.DAML.con;
               //// DataTable dttbld = daml.SELECT_QUIRY_only_retrn_dt("select itemno item_no,0 item_qty from ttk_dtl where ttbranch='" + BL.CLS_Session.brno + "' and whno='" + cmb_ftwh.SelectedValue + "' group by itemno");
                // daml.Exec_Query_only("bld_whbins_cost_all_by_tran @items_tran_tb=" + dttbld + "");
                using (SqlCommand cmd = new SqlCommand("bld_whbins_cost_all_by_tran"))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = consyn;
                    cmd.Parameters.AddWithValue("@items_tran_tb", dttbld);
                    if (consyn.State == ConnectionState.Closed)
                        consyn.Open();
                    cmd.ExecuteNonQuery();
                    // cmd.BeginExecuteNonQuery();
                    //  await cmd.ExecuteNonQueryAsync();
                    //IAsyncResult result = cmd.BeginExecuteNonQuery();
                    //while (!result.IsCompleted)
                    //{
                    //}
                    //MessageBox.Show("Command complete. Affected "+cmd.EndExecuteNonQuery(result)+" rows.");
                    // cmd.ExecuteNonQuery();
                    consyn.Close();

                    //   MessageBox.Show("Items Updated : " + cmd.Parameters["@NO_items_updated"].Value, "عدد الاصناف اللتي تم تحديثها", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
            catch { }

        }
        

        private void btn_update_Click(object sender, EventArgs e)
        {
            DataTable dt = daml.SELECT_QUIRY_only_retrn_dt("select * from ttk_hdr where ttbranch='" + BL.CLS_Session.brno + "' and ttwhno='" + cmb_ftwh.SelectedValue + "'");

            if (dt.Rows.Count > 0)
            {
                DataTable dtwh = daml.SELECT_QUIRY_only_retrn_dt("select a.br_no,a.wh_no,b.item_name,a.item_no,a.qty,a.lcost,b.item_barcode  from whbins a,items b where a.item_no=b.item_no and a.br_no='" + BL.CLS_Session.brno + "' and a.wh_no='" + cmb_ftwh.SelectedValue + "'");

               // int exeh = daml.Insert_Update_Delete_retrn_int("insert into ttk_hdr ([ttbranch],[ttwhno],[ttmdate],[tthdate],[ttcntr]) VALUES('" + BL.CLS_Session.brno + "','" + cmb_ftwh.SelectedValue + "','" + dtv.convert_to_yyyyDDdd(txt_mdate.Text) + "','" + dtv.convert_to_yyyyDDdd(txt_hdate.Text) + "'," + dtwh.Rows.Count + ")", false);

                for (int i = 0; i < dtwh.Rows.Count; i++)
                {
                    string StrQuery = " MERGE ttk_dtl as t"
                                    + " USING (select '" + dtwh.Rows[i][0].ToString() + "' as ttbranch, '" + dtwh.Rows[i][1].ToString() + "' as whno,'" + dtwh.Rows[i][2].ToString() + "' as name,' ' as binno,0 as ttqty,0 as ttstatus," + (i + 1) + " as srl_no,' ' as mclass, '" + dtwh.Rows[i][3].ToString() + "' as itemno," + dtwh.Rows[i][4].ToString() + " as mqty,' ' as expdate," + dtwh.Rows[i][5].ToString() + " as lcost,0 as fcost,'" + dtwh.Rows[i][6].ToString() + "' as barcode,0 as pack0,0 as pack1,0 as pkqty1,0 as nobin) as s"
                                    + " ON T.itemno=S.itemno"
                                    + " WHEN MATCHED THEN"
                                    + " UPDATE SET T.mqty = S.mqty"
                                    + " WHEN NOT MATCHED THEN"
                                    + " INSERT VALUES(s.ttbranch, s.whno, s.name,s.binno,s.ttqty,s.ttstatus,s.srl_no,s.mclass,s.itemno,s.mqty,s.expdate,s.lcost,s.fcost,s.barcode,s.pack0,s.pack1,s.pkqty1,s.nobin);";
                    try
                    {
                        //SqlConnection conn = new SqlConnection();

                        /*
                        using (SqlCommand comm = new SqlCommand(StrQuery, con))
                        {
                           // con.Open();
                            comm.ExecuteNonQuery();
                           // con.Close();
                       
                        }
                         */

                        daml.Insert_Update_Delete_retrn_int(StrQuery, false);

                        //   progressBar1.Value = i;
                        //   pfr.progressBar1.Value = i;


                    }
                    catch (Exception ex)
                    {
                        // MessageBox.Show(ex.ToString());
                    }
                    finally
                    {

                    }
                }

                MessageBox.Show("تم تحديث الاصناف", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
             
            }
            else
            {
                MessageBox.Show("لا يوجد جرد لتحديثه", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (cmb_ftwh.SelectedIndex == -1)
            {
                MessageBox.Show("يجب اختيار المخزن اولا", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmb_ftwh.Focus();
                return;

            }
               DataTable dt = daml.SELECT_QUIRY_only_retrn_dt("select * from ttk_hdr where ttbranch='" + BL.CLS_Session.brno + "' and ttwhno='" + cmb_ftwh.SelectedValue + "'");

               if (dt.Rows.Count > 0)
               {
                   DialogResult result = MessageBox.Show("هل تريد حذف الجرد الحالي", "Confirmation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                   if (result == DialogResult.Yes)
                   {

                   daml.Exec_Query_only("delete from ttk_hdr where ttbranch='" + BL.CLS_Session.brno + "' and ttwhno='" + cmb_ftwh.SelectedValue + "'");
                   MessageBox.Show("تم حذف الجرد الموجود ", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                   }
                   else if (result == DialogResult.No)
                   {
                       //...
                   }
                   else
                   {
                       //...
                   }
               }
               else
               {
                   MessageBox.Show("لا يوجد جرد لحذفه", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
               }
          
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void cmb_ftwh_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmb_ftwh_Enter(object sender, EventArgs e)
        {
          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (cmb_ftwh.SelectedIndex == -1)
            {
                MessageBox.Show("يجب اختيار مخزن للجرد", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmb_ftwh.Focus();
                return;

            }

            string cond = checkBox1.Checked ? " and ttqty > 0" : "";
            string mdate, hdate;
            mdate = txt_mdate.Text.Replace("-", "").Substring(4, 4) + txt_mdate.Text.Replace("-", "").Substring(2, 2) + txt_mdate.Text.Replace("-", "").Substring(0, 2);
            hdate = txt_hdate.Text.Replace("-", "").Substring(4, 4) + txt_hdate.Text.Replace("-", "").Substring(2, 2) + txt_hdate.Text.Replace("-", "").Substring(0, 2);


            DataTable dt20 = daml.SELECT_QUIRY_only_retrn_dt("select * from sto_hdr where trtype='35' and branch='" + BL.CLS_Session.brno + "'  and whno='" + cmb_ftwh.SelectedValue + "'");

         //   DataTable dt2 = daml.SELECT_QUIRY_only_retrn_dt("select isnull(max(ref),0) from sto_hdr where trtype='35' and branch='" + BL.CLS_Session.brno + "'");

            DataTable dt_tthdr = daml.SELECT_QUIRY_only_retrn_dt("select round(sum(((ttqty-mqty)*lcost)),2) from ttk_dtl where ttbranch='" + BL.CLS_Session.brno + "' and whno='" + cmb_ftwh.SelectedValue + "' " + cond + "");
            DataTable dt_ttdtl = daml.SELECT_QUIRY_only_retrn_dt("select * from ttk_dtl where ttbranch='" + BL.CLS_Session.brno + "' and whno='" + cmb_ftwh.SelectedValue + "' " + cond + " order by srl_no");

            if (dt20.Rows.Count == 1)
            {
                //  ref_max = Convert.ToInt32(dt2.Rows[0][0]);
                int exexcuteds = daml.Insert_Update_Delete_retrn_int("update sto_hdr set amnttl=" + dt_tthdr.Rows[0][0] + " where branch='" + BL.CLS_Session.brno + "'  and whno='" + cmb_ftwh.SelectedValue + "' and trmdate='" + mdate + "'", false);
                progressBar1.Visible = true;
                progressBar1.Minimum = 0;
                progressBar1.Maximum = dt_ttdtl.Rows.Count;
                foreach (DataRow row in dt_ttdtl.Rows)
                {
                    using (SqlCommand cmd = new SqlCommand("update sto_dtl set  qty=" + (Convert.ToDouble(row[4]) - Convert.ToDouble(row[9])) + " where branch='" + BL.CLS_Session.brno + "'  and whno='" + cmb_ftwh.SelectedValue + "' and trmdate='" + mdate + "' and itemno='" + row[8] + "'", con))
                    {
                        // cmd.Parameters.AddWithValue("@a1", Convert.ToDouble(row[4]) - Convert.ToDouble(row[9]));
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        cmd.ExecuteNonQuery();
                    }
                    progressBar1.Increment(1);
                }
                MessageBox.Show("تم تحديث الجرد بنجاح", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                con.Close();
            }
            else {
                MessageBox.Show("لا يوجد جرد", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

           
           
        }
    }
}
