using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace POS.Sto
{
    public partial class GRD_Start : BaseForm
    {
        BL.Date_Validate dtv = new BL.Date_Validate();
        BL.DAML daml = new BL.DAML();
        public GRD_Start()
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


            DataTable dtgrp = daml.SELECT_QUIRY_only_retrn_dt("select group_id,group_name from groups where group_pid is null ");



            cmb_grp.DataSource = dtgrp;
            cmb_grp.DisplayMember = "group_name";
            cmb_grp.ValueMember = "group_id";
            cmb_grp.SelectedIndex = -1;
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
            if (cmb_ftwh.SelectedIndex == -1)
            {
                MessageBox.Show("يجب اختيار مخزن للجرد", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmb_ftwh.Focus();
                return;

            }

            string condg = cmb_grp.SelectedIndex == -1 ? "" : " and b.item_group='" + cmb_grp.SelectedValue + "' ";
            string condsg = cmb_sgrp.SelectedIndex == -1 ? "" : " and b.sgroup='" + cmb_sgrp.SelectedValue + "' ";
            WaitForm wf = new WaitForm("جاري ابتداء اجراءات الجرد .. يرجى الانتظار ...");

            DataTable dt = daml.SELECT_QUIRY_only_retrn_dt("select * from ttk_hdr where ttbranch='" +BL.CLS_Session.brno + "' and ttwhno='" + cmb_ftwh.SelectedValue + "'");

            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("هناك جرد سابق يجب اكماله او حذفة", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
               
                wf.MdiParent = MdiParent;
                wf.Show();
                Application.DoEvents();
                try
                {
                    // DataTable dtwh = daml.SELECT_QUIRY_only_retrn_dt("select a.br_no,a.wh_no,b.item_name,a.item_no,a.qty+a.openbal,b.item_curcost,b.item_barcode,b.item_unit,b.unit2,b.uq2,b.item_price  from whbins a,items b where a.item_no=b.item_no and a.br_no='" + BL.CLS_Session.brno + "' and a.wh_no='" + cmb_ftwh.SelectedValue + "' " + condg + condsg + "");
                    DataTable dtwh = daml.SELECT_QUIRY_only_retrn_dt("select isnull(a.br_no,'" + BL.CLS_Session.brno + "') br_no,isnull(a.wh_no,'" + cmb_ftwh.SelectedValue + "') wh_no,SUBSTRING(b.item_name, 1, 70) item_name,isnull(a.item_no,b.item_no) item_no,isnull((a.qty+a.openbal),0),(case when a.lcost> 0 then a.lcost else b.item_curcost end) item_curcost ,b.item_barcode,b.item_unit,b.unit2,b.uq2,b.item_price,b.item_cost  from whbins a right join items b on a.item_no=b.item_no and a.br_no='" + BL.CLS_Session.brno + "' and a.wh_no='" + cmb_ftwh.SelectedValue + "'  " + condg + condsg + "");
                    if (dtwh.Rows.Count == 0)
                    {
                        MessageBox.Show("لايوجد اصناف للجرد يرجى عمل بناء الارصدة والتكاليف", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    int exeh = daml.Insert_Update_Delete_retrn_int("insert into ttk_hdr ([ttbranch],[ttwhno],[ttmdate],[tthdate],[ttcntr]) VALUES('" + BL.CLS_Session.brno + "','" + cmb_ftwh.SelectedValue + "','" + dtv.convert_to_yyyyDDdd(txt_mdate.Text) + "','" + dtv.convert_to_yyyyDDdd(txt_hdate.Text) + "'," + dtwh.Rows.Count + ")", false);

                    for (int i = 0; i < dtwh.Rows.Count; i++)
                    {
                        if (!dtwh.Rows[i][11].ToString().Equals("0"))
                        {
                            string StrQuery = " MERGE ttk_dtl as t"
                                            + " USING (select '" + dtwh.Rows[i][0].ToString() + "' as ttbranch, '" + dtwh.Rows[i][1].ToString() + "' as whno,'" + dtwh.Rows[i][2].ToString() + "' as name,' ' as binno,0 as ttqty,0 as ttstatus," + (i + 1) + " as srl_no,' ' as mclass, '" + dtwh.Rows[i][3].ToString() + "' as itemno," + dtwh.Rows[i][4].ToString() + " as mqty,' ' as expdate," + dtwh.Rows[i][5].ToString() + " as lcost,0 as fcost,'" + dtwh.Rows[i][6].ToString() + "' as barcode," + dtwh.Rows[i][7].ToString() + " as pack0," + dtwh.Rows[i][8].ToString() + " as pack1," + dtwh.Rows[i][9].ToString() + " as pkqty1,0 as nobin," + dtwh.Rows[i][10].ToString() + " as price,'' note) as s"
                                            + " ON T.itemno=S.itemno and t.whno=s.whno and T.ttbranch=S.ttbranch"
                                            + " WHEN MATCHED THEN"
                                            + " UPDATE SET T.mqty = S.mqty"
                                            + " WHEN NOT MATCHED THEN"
                                            + " INSERT VALUES(s.ttbranch, s.whno, s.name,s.binno,s.ttqty,s.ttstatus,s.srl_no,s.mclass,s.itemno,s.mqty,s.expdate,s.lcost,s.fcost,s.barcode,s.pack0,s.pack1,s.pkqty1,s.nobin,s.price,s.note);";

                            //SqlConnection conn = new SqlConnection();

                            /*
                            using (SqlCommand comm = new SqlCommand(StrQuery, con))
                            {
                               // con.Open();
                                comm.ExecuteNonQuery();
                               // con.Close();
                       
                            }
                             */
                            try
                            {
                                daml.Insert_Update_Delete_retrn_int(StrQuery, false);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString());
                            }
                            finally
                            {

                            }
                            //   progressBar1.Value = i;
                            //   pfr.progressBar1.Value = i;

                        }

                    }
                    wf.Close();
                    MessageBox.Show("تم ابتداء الجرد بنجاح", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    wf.Close();
                }
                finally
                {

                }

            }
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            string condg = cmb_grp.SelectedIndex == -1 ? "" : " and b.item_group='" + cmb_grp.SelectedValue + "' ";
            string condsg = cmb_sgrp.SelectedIndex == -1 ? "" : " and b.sgroup='" + cmb_sgrp.SelectedValue + "' ";

            DataTable dt = daml.SELECT_QUIRY_only_retrn_dt("select * from ttk_hdr where ttbranch='" + BL.CLS_Session.brno + "' and ttwhno='" + cmb_ftwh.SelectedValue + "'");

            if (dt.Rows.Count > 0)
            {
               //// DataTable dtwh = daml.SELECT_QUIRY_only_retrn_dt("select a.br_no,a.wh_no,b.item_name,a.item_no,a.qty,a.lcost,b.item_barcode  from whbins a,items b where a.item_no=b.item_no and a.br_no='" + BL.CLS_Session.brno + "' and a.wh_no='" + cmb_ftwh.SelectedValue + "'");
                DataTable dtwh = daml.SELECT_QUIRY_only_retrn_dt("select isnull(a.br_no,'" + BL.CLS_Session.brno + "') br_no,isnull(a.wh_no,'" + cmb_ftwh.SelectedValue + "') wh_no,b.item_name,isnull(a.item_no,b.item_no) item_no,isnull((a.qty+a.openbal),0),(case when a.lcost> 0 then a.lcost else b.item_curcost end) item_curcost ,b.item_barcode,b.item_unit,b.unit2,b.uq2,b.item_price,b.item_cost  from whbins a right join items b on a.item_no=b.item_no and a.br_no='" + BL.CLS_Session.brno + "' and a.wh_no='" + cmb_ftwh.SelectedValue + "'  " + condg + condsg + "");
               
               // int exeh = daml.Insert_Update_Delete_retrn_int("insert into ttk_hdr ([ttbranch],[ttwhno],[ttmdate],[tthdate],[ttcntr]) VALUES('" + BL.CLS_Session.brno + "','" + cmb_ftwh.SelectedValue + "','" + dtv.convert_to_yyyyDDdd(txt_mdate.Text) + "','" + dtv.convert_to_yyyyDDdd(txt_hdate.Text) + "'," + dtwh.Rows.Count + ")", false);

                for (int i = 0; i < dtwh.Rows.Count; i++)
                {
                    if (!dtwh.Rows[i][11].ToString().Equals("0"))
                    {
                        string StrQuery = " MERGE ttk_dtl as t"
                                        + " USING (select '" + dtwh.Rows[i][0].ToString() + "' as ttbranch, '" + dtwh.Rows[i][1].ToString() + "' as whno,'" + dtwh.Rows[i][2].ToString() + "' as name,' ' as binno,0 as ttqty,0 as ttstatus," + (i + 1) + " as srl_no,' ' as mclass, '" + dtwh.Rows[i][3].ToString() + "' as itemno," + dtwh.Rows[i][4].ToString() + " as mqty,' ' as expdate," + dtwh.Rows[i][5].ToString() + " as lcost,0 as fcost,'" + dtwh.Rows[i][6].ToString() + "' as barcode," + dtwh.Rows[i][7].ToString() + " as pack0," + dtwh.Rows[i][8].ToString() + " as pack1," + dtwh.Rows[i][9].ToString() + " as pkqty1,0 as nobin," + dtwh.Rows[i][10].ToString() + " as price,'' as note) as s"
                            // + " USING (select '" + dtwh.Rows[i][0].ToString() + "' as ttbranch, '" + dtwh.Rows[i][1].ToString() + "' as whno,'" + dtwh.Rows[i][2].ToString() + "' as name,' ' as binno,0 as ttqty,0 as ttstatus," + (i + 1) + " as srl_no,' ' as mclass, '" + dtwh.Rows[i][3].ToString() + "' as itemno," + dtwh.Rows[i][4].ToString() + " as mqty,' ' as expdate," + dtwh.Rows[i][5].ToString() + " as lcost,0 as fcost,'" + dtwh.Rows[i][6].ToString() + "' as barcode,0 as pack0,0 as pack1,0 as pkqty1,0 as nobin) as s"
                            //+ " ON T.itemno=S.itemno"
                                        + " ON ltrim(rtrim(t.itemno))=ltrim(rtrim(s.itemno)) and t.whno=s.whno"
                                        + " WHEN MATCHED THEN"
                                        + " UPDATE SET t.mqty = s.mqty,t.price=s.price,t.lcost=s.lcost"
                                        + " WHEN NOT MATCHED THEN"
                            //  + " INSERT VALUES(s.ttbranch, s.whno, s.name,s.binno,s.ttqty,s.ttstatus,s.srl_no,s.mclass,s.itemno,s.mqty,s.expdate,s.lcost,s.fcost,s.barcode,s.pack0,s.pack1,s.pkqty1,s.nobin);";
                                        + " INSERT VALUES(s.ttbranch, s.whno, s.name,s.binno,s.ttqty,s.ttstatus,s.srl_no,s.mclass,s.itemno,s.mqty,s.expdate,s.lcost,s.fcost,s.barcode,s.pack0,s.pack1,s.pkqty1,s.nobin,s.price,s.note);";

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
                            MessageBox.Show(ex.ToString());
                        }
                        finally
                        {

                        }
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

        private void txt_mdate_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void cmb_grp_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmb_sgrp_Enter(sender, e);
            cmb_sgrp.SelectedIndex = -1;
        }

        private void cmb_sgrp_Enter(object sender, EventArgs e)
        {
            try
            {
                cmb_sgrp.DataSource = daml.SELECT_QUIRY_only_retrn_dt("select group_id,group_name from groups where group_pid='" + cmb_grp.SelectedValue + "' and group_pid is not null");
                // metroComboBox3.DataSource = dt2;
                //comboBox1.DataSource = dataSet1.Tables["Suppliers"];
                // metroComboBox3.DisplayMember = "a";
                //  comboBox1.ValueMember = comboBox1.Text;
                cmb_sgrp.DisplayMember = "group_name";
                cmb_sgrp.ValueMember = "group_id";

            }
            catch { }
        }

        private void cmb_sgrp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                cmb_sgrp.SelectedIndex = -1;
            }
        }
    }
}
