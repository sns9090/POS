using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace POS
{
    public partial class Post_All : BaseForm
    {
        BL.DAML daml = new BL.DAML();
        BL.Date_Validate datval = new BL.Date_Validate();
        string skey,tr,sl;
        int tounpost, tounpost2;
        DataTable  dttrtyps;
        public Post_All(string key)
        {
            InitializeComponent();
            skey = key;
        }

        private void Search_by_No_Load(object sender, EventArgs e)
        {
            txt_fdate.Text = DateTime.Now.ToString("dd-MM-yyyy", new CultureInfo("en-US", false)); //BL.CLS_Session.start_dt;
            txt_tdate.Text = DateTime.Now.ToString("dd-MM-yyyy", new CultureInfo("en-US", false)); //BL.CLS_Session.end_dt;

            switch (skey)
            {
                case "acc":
                       tr = BL.CLS_Session.trkey.Replace(" ", "','").Remove(0, 2) + "'";
                    // MessageBox.Show(tr);
                      cmb_salctr.Visible = false;
                      label1.Visible = false;
                    dttrtyps = daml.SELECT_QUIRY_only_retrn_dt("select * from trtypes where tr_no in(" + tr + ") and tr_no in('01','02','03','11','12')");
                    cmb_type.DataSource = dttrtyps;
                    cmb_type.DisplayMember = BL.CLS_Session.lang.Equals("E") ? "tr_ename" : "tr_name";
                    cmb_type.ValueMember = "tr_no";
                    cmb_type.SelectedIndex = -1;
                    break;

                case "sal":
                     sl = BL.CLS_Session.slkey.Replace(" ", "','").Remove(0, 2) + "'";
                    // MessageBox.Show(tr);
                   // cmb_salctr.Visible = false;
                     dttrtyps = daml.SELECT_QUIRY_only_retrn_dt("select * from slcenters where sl_brno + sl_no in(" + sl + ")");
                    cmb_salctr.DataSource = dttrtyps;
                    cmb_salctr.DisplayMember = "sl_name";
                    cmb_salctr.ValueMember = "sl_no";

                     tr = BL.CLS_Session.trkey.Replace(" ", "','").Remove(0, 2) + "'";
                    // MessageBox.Show(tr);
                   // cmb_salctr.Visible = false;
                    dttrtyps = daml.SELECT_QUIRY_only_retrn_dt("select * from trtypes where tr_no in(" + tr + ") and tr_no in('04','05','14','15')");
                    cmb_type.DataSource = dttrtyps;
                    cmb_type.DisplayMember = BL.CLS_Session.lang.Equals("E") ? "tr_ename" : "tr_name";
                    cmb_type.ValueMember = "tr_no";
                    cmb_type.SelectedIndex = -1;
                    cmb_salctr.SelectedIndex = -1;
                    cmb_salctr.Select();
                    break;

                case "pu":
                    sl = BL.CLS_Session.pukey.Replace(" ", "','").Remove(0, 2) + "'";
                    // MessageBox.Show(tr);
                    // cmb_salctr.Visible = false;
                    label1.Text = "مركز الشراء";
                    dttrtyps = daml.SELECT_QUIRY_only_retrn_dt("select * from pucenters where pu_brno + pu_no in(" + sl + ")");
                    cmb_salctr.DataSource = dttrtyps;
                    cmb_salctr.DisplayMember = "pu_name";
                    cmb_salctr.ValueMember = "pu_no";

                    tr = BL.CLS_Session.trkey.Replace(" ", "','").Remove(0, 2) + "'";
                    // MessageBox.Show(tr);
                    // cmb_salctr.Visible = false;
                    dttrtyps = daml.SELECT_QUIRY_only_retrn_dt("select * from trtypes where tr_no in(" + tr + ") and tr_no in('06','07','16','17')");
                    cmb_type.DataSource = dttrtyps;
                    cmb_type.DisplayMember = BL.CLS_Session.lang.Equals("E") ? "tr_ename" : "tr_name";
                    cmb_type.ValueMember = "tr_no";
                    cmb_type.SelectedIndex = -1;
                    cmb_salctr.SelectedIndex = -1;
                    cmb_salctr.Select();
                    break;

                case "sto":
                    //sl = BL.CLS_Session.pukey.Replace(" ", "','").Remove(0, 2) + "'";
                    //// MessageBox.Show(tr);
                    //// cmb_salctr.Visible = false;
                    //dttrtyps = daml.SELECT_QUIRY_only_retrn_dt("select * from pucenters where pu_brno + pu_no in(" + sl + ")");
                    //cmb_salctr.DataSource = dttrtyps;
                    //cmb_salctr.DisplayMember = "pu_name";
                    //cmb_salctr.ValueMember = "pu_no";

                    tr = BL.CLS_Session.trkey.Replace(" ", "','").Remove(0, 2) + "'";
                    // MessageBox.Show(tr);
                    cmb_salctr.Visible = false;
                    label1.Visible=false;
                    dttrtyps = daml.SELECT_QUIRY_only_retrn_dt("select * from trtypes where tr_no in(" + tr + ") and tr_no in('31','32','33')");
                    cmb_type.DataSource = dttrtyps;
                    cmb_type.DisplayMember = BL.CLS_Session.lang.Equals("E") ? "tr_ename" : "tr_name";
                    cmb_type.ValueMember = "tr_no";
                    cmb_type.SelectedIndex = -1;
                    break;
            }
            if (skey == "02")
            {/*
                comboBox1.DisplayMember = "Text";
                comboBox1.ValueMember = "Value";

                var items = new[] { new { Text = "سند قبض", Value = "02" }};
                comboBox1.DataSource = items;
                comboBox1.SelectedIndex = 0;
              */
               // txt_ref.Select();
            }
            if (skey == "03")
            {/*
                comboBox1.DisplayMember = "Text";
                comboBox1.ValueMember = "Value";

                var items = new[] { new { Text = "سند صرف", Value = "03" } };
                comboBox1.DataSource = items;
                comboBox1.SelectedIndex = 0;
              * */
               // txt_ref.Select();
            }

            if (skey == "04")
            {
                var stn = new Sal.Sal_Tran_notax("","","");
                cmb_salctr.Visible = true;
                /*
                DataTable dtsal = daml.SELECT_QUIRY_only_retrn_dt("Select * from slcenters where sl_brno=" + BL.CLS_Session.brno);
                cmb_salctr.DataSource = dtsal;
                cmb_salctr.DisplayMember = "sl_name";
                cmb_salctr.ValueMember = "sl_no";
                cmb_salctr.SelectedIndex = 0;
                */
                //comboBox1.DisplayMember = "Text";
                //comboBox1.ValueMember = "Value";

                //var items = new[] { new { Text = "مبيعات نقدية", Value = "04" }, new { Text = "مبيعات اجله", Value = "05" }, new { Text = "مرتجع نقدي", Value = "14" }, new { Text = "مرتجع اجل", Value = "15" } };
                //comboBox1.DataSource = items;
                //comboBox1.SelectedIndex = -1;
              //  comboBox1.DataSource = stn.comboBox1.DataSource;
            }
            if (skey == "07")
            {
                var stn = new Pur.Pur_Tran_D("", "", "");
                cmb_salctr.Visible = true;
            }

            if (skey == "30")
            {
                var stn = new Sto.Sto_ImpExp("", "", "");
                cmb_salctr.Visible = false;
            }

           // comboBox1.FlatStyle = FlatStyle.Flat;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

              //  this.Close();


            }


        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
               txt_fdate.Focus();
            }
            if (e.KeyCode == Keys.Delete)
            {
                cmb_type.SelectedIndex = -1;
            }
        }

        private void cmb_salctr_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmb_type.Focus();
            }
            if (e.KeyCode == Keys.Delete)
            {
                cmb_salctr.SelectedIndex = -1;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(txt_ref.Text) || cmb_type.SelectedIndex == -1)
            //{
            //    MessageBox.Show("بجب اختيار نوع الحركة وادخال رقمها");
            //    return;
            //}

            string cond = checkBox1.Checked ? " and acc_hdr.a_hdate" : " and acc_hdr.a_mdate";
            string conddd = checkBox1.Checked ? " a_hdate" : " a_mdate";
            // string condp = rad_posted.Checked ? " and a_posted=1 " : (rad_notpostd.Checked ? " and a_posted=0 " : " ");

            string condf = txt_fno.Text != "" ? txt_fno.Text : "1";
            string condt = txt_tno.Text != "" ? txt_tno.Text : "999999999";
            string condft = " and acc_hdr.a_ref between " + condf + " and " + condt + " ";
            string condtty = cmb_type.SelectedIndex != -1 ? " and acc_hdr.a_type='" + cmb_type.SelectedValue + "' " : " ";

            string condsp = checkBox1.Checked ? "invhdate" : "invmdate";
            // string condp = rad_posted.Checked ? " and a_posted=1 " : (rad_notpostd.Checked ? " and a_posted=0 " : " ");

            string condfsp = txt_fno.Text != "" ? txt_fno.Text : "1";
            string condtsp = txt_tno.Text != "" ? txt_tno.Text : "999999999";
            string condftsp = " and ref between " + condf + " and " + condt + " ";
            string condttysp = cmb_type.SelectedIndex != -1 ? " and invtype='" + cmb_type.SelectedValue + "' " : " ";
            string condttystp = cmb_type.SelectedIndex != -1 ? " and trtype='" + cmb_type.SelectedValue + "' " : " ";
            string condttysl = cmb_salctr.SelectedIndex != -1 ? " and slcenter='" + cmb_salctr.SelectedValue + "' " : " ";
            string condttypu = cmb_salctr.SelectedIndex != -1 ? " and pucenter='" + cmb_salctr.SelectedValue + "' " : " ";

            string condttysls = cmb_salctr.SelectedIndex != -1 ? " and sl_no='" + cmb_salctr.SelectedValue + "' " : " ";
            string condttypus = cmb_salctr.SelectedIndex != -1 ? " and pu_no='" + cmb_salctr.SelectedValue + "' " : " ";
            //string condp = checkBox1.Checked ? "hdate" : "a_mdate";
            //// string condp = rad_posted.Checked ? " and a_posted=1 " : (rad_notpostd.Checked ? " and a_posted=0 " : " ");

            //string condfp = txt_fno.Text != "" ? txt_fno.Text : "1";
            //string condtp = txt_tno.Text != "" ? txt_tno.Text : "999999999";
            //string condftp = " and a_ref between " + condf + " and " + condt + " ";
            //string condttyp = cmb_type.SelectedIndex != -1 ? " and a_type='" + cmb_type.SelectedValue + "' " : " ";

            string condst = checkBox1.Checked ? "trhdate" : "trmdate";

            // string condp = rad_posted.Checked ? " and a_posted=1 " : (rad_notpostd.Checked ? " and a_posted=0 " : " ");

            string condfst = txt_fno.Text != "" ? txt_fno.Text : "1";
            string condtst = txt_tno.Text != "" ? txt_tno.Text : "999999999";
            string condftst = " and ref between " + condf + " and " + condt + " ";
            string condttyst = cmb_type.SelectedIndex != -1 ? " and trtype='" + cmb_type.SelectedValue + "' " : " ";

            switch (skey)
            {
                case "acc":
                   // tr = BL.CLS_Session.trkey.Replace(" ", "','").Remove(0, 2) + "'";
                    // MessageBox.Show(tr);
                   // cmb_salctr.Visible = false;
                   // tounpost = daml.Insert_Update_Delete_retrn_int("update acc_hdr set a_posted=1 where exists(select * from acc_dtl b where acc_hdr.a_brno=b.a_brno and acc_hdr.a_ref=b.a_ref and acc_hdr.a_type=b.a_type and acc_hdr.pu_no=b.pu_no and acc_hdr.sl_no=b.sl_no having acc_hdr.a_amt= sum(b.a_damt) and acc_hdr.a_amt= sum(b.a_camt)) " + cond + " between '" + datval.convert_to_yyyyDDdd(txt_fdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tdate.Text) + "' and a_brno='" + BL.CLS_Session.brno + "' " + condtty + condft + condttysls + condttypus + " and a_type in('01','02','03')" ,false); //and jhsrc like 'Acc%'", false);
                     tounpost = daml.Insert_Update_Delete_retrn_int("update acc_hdr set a_posted=1 where exists(select * from acc_dtl b where acc_hdr.a_brno=b.a_brno and acc_hdr.a_ref=b.a_ref and acc_hdr.a_type=b.a_type and acc_hdr.pu_no=b.pu_no and acc_hdr.sl_no=b.sl_no having acc_hdr.a_amt= sum(b.a_damt) and acc_hdr.a_amt= sum(b.a_camt)) " + cond + " between '" + datval.convert_to_yyyyDDdd(txt_fdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tdate.Text) + "' and a_brno='" + BL.CLS_Session.brno + "' " + condtty + condft + condttysls + condttypus + " and jhsrc like 'Acc%' or jhsrc like 'Cus%' or jhsrc like 'Sup%' " ,false); //and jhsrc like 'Acc%'", false);
                    if (tounpost > 0)
                        MessageBox.Show("تم الترحيل بنجاح","",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
                    else
                        MessageBox.Show("فشل الترحيل","",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    break;

                case "sal":
                    tounpost = daml.Insert_Update_Delete_retrn_int("update acc_hdr set a_posted=1 where " + conddd + " between '" + datval.convert_to_yyyyDDdd(txt_fdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tdate.Text) + "' and a_brno='" + BL.CLS_Session.brno + "' " + condtty + condft + condttysls + condttypus + " and (jhsrc like 'Sal%' or  jhsrc like 'Pos%')", false);
                    tounpost2 = daml.Insert_Update_Delete_retrn_int("update sales_hdr set posted=1 where " + condsp + " between '" + datval.convert_to_yyyyDDdd(txt_fdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tdate.Text) + "' and branch='" + BL.CLS_Session.brno + "' " + condttysp + condftsp + condttysl, false);

                    if (tounpost + tounpost2 > 0)
                        MessageBox.Show("تم الترحيل بنجاح","",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
                    else
                        MessageBox.Show("فشل الترحيل","",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    break;

                case "pu":
                    tounpost = daml.Insert_Update_Delete_retrn_int("update acc_hdr set a_posted=1 where " + conddd + " between '" + datval.convert_to_yyyyDDdd(txt_fdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tdate.Text) + "' and a_brno='" + BL.CLS_Session.brno + "' " + condtty + condft + condttysls + condttypus + " and jhsrc like 'Pur%'", false);
                    tounpost2 = daml.Insert_Update_Delete_retrn_int("update pu_hdr set posted=1 where " + condsp + " between '" + datval.convert_to_yyyyDDdd(txt_fdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tdate.Text) + "' and branch='" + BL.CLS_Session.brno + "' " + condttysp + condftsp + condttypu, false);

                    if (tounpost + tounpost2 > 0)
                        MessageBox.Show("تم الترحيل بنجاح","",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
                    else
                        MessageBox.Show("فشل الترحيل","",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    break;

                case "sto":
                  //  tounpost = daml.Insert_Update_Delete_retrn_int("update acc_hdr set a_posted=0 where a_brno='" + BL.CLS_Session.brno + "' and a_type='" + cmb_type.SelectedValue + "' and a_ref=" + txt_ref.Text + " pu_no='" + cmb_salctr.SelectedValue + "'", false);
                    tounpost = daml.Insert_Update_Delete_retrn_int("update sto_hdr set posted=1 where " + condst + " between '" + datval.convert_to_yyyyDDdd(txt_fdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tdate.Text) + "' and branch='" + BL.CLS_Session.brno + "' and isbrtrx=0 " + condttystp + condftsp, false);

                    if (tounpost > 0)
                        MessageBox.Show("تم الترحيل بنجاح","",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
                    else
                        MessageBox.Show("فشل الترحيل","",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    break;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                txt_fdate.Text = DateTime.Now.ToString("01-01-yyyy", new CultureInfo("ar-SA", false));
                //  string temy2 =
                txt_tdate.Text = DateTime.Now.ToString("dd-MM-yyyy", new CultureInfo("ar-SA", false));
            }
            else
            {
                txt_fdate.Text = DateTime.Now.ToString("01-01-yyyy", new CultureInfo("en-US", false));
                //  string temy2 =
                txt_tdate.Text = DateTime.Now.ToString("dd-MM-yyyy", new CultureInfo("en-US", false));
            }
        }

        private void txt_fdate_Leave(object sender, EventArgs e)
        {
            try
            {
                string datestr = txt_fdate.Text.ToString().Replace("-", "").Trim();
                if (datestr.Length == 0)
                {
                    txt_fdate.Text = DateTime.Now.ToString("dd", new CultureInfo("en-US", false)) + DateTime.Now.ToString("MM", new CultureInfo("en-US", false)) + DateTime.Now.ToString("yyyy", new CultureInfo("en-US", false));
                    // txt_hdate.Text = convert_to_hdate(txt_hdate.Text.ToString());
                }
                else
                {
                    if (datestr.Length == 2)
                    {
                        txt_fdate.Text = datestr + DateTime.Now.ToString("MM", new CultureInfo("en-US", false)) + DateTime.Now.ToString("yyyy", new CultureInfo("en-US", false));
                        // txt_hdate.Text = convert_to_hdate(txt_mdate.Text.ToString());
                    }
                    else
                    {
                        if (datestr.Length == 4)
                        {
                            txt_fdate.Text = datestr + DateTime.Now.ToString("yyyy", new CultureInfo("en-US", false));
                            // txt_hdate.Text = convert_to_hdate(txt_mdate.Text.ToString());
                        }
                        else
                        {
                            // txt_hdate.Text = convert_to_hdate(txt_mdate.Text.ToString());
                        }
                    }
                }
            }
            catch { }
        }

        private void txt_tdate_Leave(object sender, EventArgs e)
        {
            try
            {
                string datestr = txt_tdate.Text.ToString().Replace("-", "").Trim();
                if (datestr.Length == 0)
                {
                    txt_tdate.Text = DateTime.Now.ToString("dd", new CultureInfo("en-US", false)) + DateTime.Now.ToString("MM", new CultureInfo("en-US", false)) + DateTime.Now.ToString("yyyy", new CultureInfo("en-US", false));
                    // txt_hdate.Text = convert_to_hdate(txt_hdate.Text.ToString());
                }
                else
                {
                    if (datestr.Length == 2)
                    {
                        txt_tdate.Text = datestr + DateTime.Now.ToString("MM", new CultureInfo("en-US", false)) + DateTime.Now.ToString("yyyy", new CultureInfo("en-US", false));
                        // txt_hdate.Text = convert_to_hdate(txt_mdate.Text.ToString());
                    }
                    else
                    {
                        if (datestr.Length == 4)
                        {
                            txt_tdate.Text = datestr + DateTime.Now.ToString("yyyy", new CultureInfo("en-US", false));
                            // txt_hdate.Text = convert_to_hdate(txt_mdate.Text.ToString());
                        }
                        else
                        {
                            // txt_hdate.Text = convert_to_hdate(txt_mdate.Text.ToString());
                        }
                    }
                }
            }
            catch { }
        }

        private void txt_fno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_tno.Focus();
            }
        }

        private void txt_tno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.Focus();
            }
        }

        private void txt_fdate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_tdate.Focus();
            }
        }

        private void txt_tdate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_fno.Focus();
            }
        }

        private void txt_fdate_Enter(object sender, EventArgs e)
        {
           // txt_fdate.Focus();
            txt_fdate.SelectAll();
        }

        private void txt_tdate_Enter(object sender, EventArgs e)
        {
            txt_tdate.SelectAll();
        }
    }
}
