using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace POS
{
    public partial class UnPost_All : BaseForm
    {
        BL.DAML daml = new BL.DAML();   
        string skey,tr,sl;
        int tounpost, tounpost2;
        DataTable  dttrtyps;
        public UnPost_All(string key)
        {
            InitializeComponent();
            skey = key;
        }

        private void Search_by_No_Load(object sender, EventArgs e)
        {
            switch (skey)
            {
                case "acc":
                       tr = BL.CLS_Session.trkey.Replace(" ", "','").Remove(0, 2) + "'";
                    // MessageBox.Show(tr);
                      cmb_salctr.Visible = false;
                    dttrtyps = daml.SELECT_QUIRY_only_retrn_dt("select * from trtypes where tr_no in(" + tr + ") and tr_no in('01','02','03','11','12','08','09')");
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
                    break;

                case "pu":
                    sl = BL.CLS_Session.pukey.Replace(" ", "','").Remove(0, 2) + "'";
                    // MessageBox.Show(tr);
                    // cmb_salctr.Visible = false;
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
                    dttrtyps = daml.SELECT_QUIRY_only_retrn_dt("select * from trtypes where tr_no in(" + tr + ") and tr_no in('31','32','33','35')");
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
                txt_ref.Select();
            }
            if (skey == "03")
            {/*
                comboBox1.DisplayMember = "Text";
                comboBox1.ValueMember = "Value";

                var items = new[] { new { Text = "سند صرف", Value = "03" } };
                comboBox1.DataSource = items;
                comboBox1.SelectedIndex = 0;
              * */
                txt_ref.Select();
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
                button1.Focus();

            }


        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_ref.Focus();
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
            if (string.IsNullOrEmpty(txt_ref.Text) || cmb_type.SelectedIndex == -1)
            {
                MessageBox.Show("بجب اختيار نوع الحركة وادخال رقمها");
                return;
            }

            switch (skey)
            {
                case "acc":
                   // tr = BL.CLS_Session.trkey.Replace(" ", "','").Remove(0, 2) + "'";
                    // MessageBox.Show(tr);
                   // cmb_salctr.Visible = false;
                    tounpost = daml.Insert_Update_Delete_retrn_int("update acc_hdr set a_posted=0 where a_brno='" + BL.CLS_Session.brno + "' and a_type='" + cmb_type.SelectedValue + "' and a_ref=" + txt_ref.Text + "", false);
                    if (tounpost > 0)
                        MessageBox.Show("تم فك الترحيل بنجاح","",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
                    else
                        MessageBox.Show("فشل في فك الترحيل","",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    break;

                case "sal":
                    tounpost = daml.Insert_Update_Delete_retrn_int("update acc_hdr set a_posted=0 where a_brno='" + BL.CLS_Session.brno + "' and a_type='" + cmb_type.SelectedValue + "' and a_ref=" + txt_ref.Text + " and sl_no='" + cmb_salctr.SelectedValue + "'", false);
                    tounpost2 = daml.Insert_Update_Delete_retrn_int("update sales_hdr set posted=0 where branch='" + BL.CLS_Session.brno + "' and invtype='" + cmb_type.SelectedValue + "' and ref=" + txt_ref.Text + " and slcenter='" + cmb_salctr.SelectedValue + "'", false);

                    if (tounpost + tounpost2 > 0)
                        MessageBox.Show("تم فك الترحيل بنجاح","",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
                    else
                        MessageBox.Show("فشل في فك الترحيل","",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    break;

                case "pu":
                    tounpost = daml.Insert_Update_Delete_retrn_int("update acc_hdr set a_posted=0 where a_brno='" + BL.CLS_Session.brno + "' and a_type='" + cmb_type.SelectedValue + "' and a_ref=" + txt_ref.Text + " and pu_no='" + cmb_salctr.SelectedValue + "'", false);
                    tounpost2 = daml.Insert_Update_Delete_retrn_int("update pu_hdr set posted=0 where branch='" + BL.CLS_Session.brno + "' and invtype='" + cmb_type.SelectedValue + "' and ref=" + txt_ref.Text + " and pucenter='" + cmb_salctr.SelectedValue + "'", false);

                    if (tounpost + tounpost2 > 0)
                        MessageBox.Show("تم فك الترحيل بنجاح","",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
                    else
                        MessageBox.Show("فشل في فك الترحيل","",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    break;

                case "sto":
                  //  tounpost = daml.Insert_Update_Delete_retrn_int("update acc_hdr set a_posted=0 where a_brno='" + BL.CLS_Session.brno + "' and a_type='" + cmb_type.SelectedValue + "' and a_ref=" + txt_ref.Text + " pu_no='" + cmb_salctr.SelectedValue + "'", false);
                    tounpost = daml.Insert_Update_Delete_retrn_int("update sto_hdr set posted=0 where branch='" + BL.CLS_Session.brno + "' and trtype='" + cmb_type.SelectedValue + "' and ref=" + txt_ref.Text + "", false);

                    if (tounpost > 0)
                        MessageBox.Show("تم فك الترحيل بنجاح","",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
                    else
                        MessageBox.Show("فشل في فك الترحيل","",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    break;
            }
        }
    }
}
