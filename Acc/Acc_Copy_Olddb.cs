using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace POS.Acc
{
    public partial class Acc_Copy_Olddb : BaseForm
    {
        SqlConnection con = BL.DAML.con;
        BL.DAML daml = new BL.DAML();
        public Acc_Copy_Olddb()
        {
            InitializeComponent();
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WaitForm wf = new WaitForm("");
            wf.MdiParent = MdiParent;

            if (string.IsNullOrEmpty(txt_oyr.Text))
            {
                MessageBox.Show("يرجى ادخال السنة المراد نقل الارصدة منها", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_oyr.Focus();
                return;
            }

            if (Convert.ToInt32(txt_oyr.Text.Substring(2, 2) + txt_oyr.Text.Substring(5, 4)) >= Convert.ToInt32(BL.CLS_Session.sqldb.Substring(2, 2) + BL.CLS_Session.sqldb.Substring(5, 4)))
            {
                MessageBox.Show("لا يمكن النقل الا من السنوات السابقة فقط", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_oyr.Focus();
                return;
            }

            try
            {
                if (panel1.Enabled == true)
                {

                    if (rd_acc_copy.Checked)
                    {
                        //WaitForm wf = new WaitForm("");
                        //wf.MdiParent = MdiParent;
                        wf.Show();
                        Application.DoEvents();
                      //  daml.Exec_Query_only("db" + BL.CLS_Session.cmp_id + "y" + txt_oyr.Text + ".dbo.beld_acc_balances");
                        //daml.Exec_Query_only("db" + BL.CLS_Session.cmp_id + "y" + txt_oyr.Text + ".dbo.beld_acc_balances_all");
                        //daml.Exec_Query_only("copy_acc_balances_from_old_db @db='db" + BL.CLS_Session.cmp_id + "y" + txt_oyr.Text + "' , @cond='acc'");
                        daml.Exec_Query_only( txt_oyr.Text + ".dbo.beld_acc_balances_all");
                        daml.Exec_Query_only("copy_acc_balances_from_old_db @db='" + txt_oyr.Text + "' , @cond='acc'");
                        MessageBox.Show("تم نقل ارصدة الحسابات بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        // bk.MdiParent = this;
                        // bk.Show();
                        // Application.Exit();
                        wf.Close();
                    }

                    if (rd_cus_copy.Checked)
                    {
                        //WaitForm wf = new WaitForm("");
                        //wf.MdiParent = MdiParent;
                        wf.Show();
                        Application.DoEvents();
                       // daml.Exec_Query_only("db" + BL.CLS_Session.cmp_id + "y" + txt_oyr.Text + ".dbo.beld_cust_balances");
                        //daml.Exec_Query_only("db" + BL.CLS_Session.cmp_id + "y" + txt_oyr.Text + ".dbo.beld_cust_balances_all_br");
                        //daml.Exec_Query_only("copy_acc_balances_from_old_db @db='db" + BL.CLS_Session.cmp_id + "y" + txt_oyr.Text + "' , @cond='cus'");
                        daml.Exec_Query_only( txt_oyr.Text + ".dbo.beld_cust_balances_all_br");
                        daml.Exec_Query_only("copy_acc_balances_from_old_db @db='" + txt_oyr.Text + "' , @cond='cus'");
                        MessageBox.Show("تم نقل ارصدة العملاء بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        // bk.MdiParent = this;
                        // bk.Show();
                        // Application.Exit();
                        wf.Close();
                    }

                    if (rd_sup_copy.Checked)
                    {

                        wf.Show();
                        Application.DoEvents();
                        // daml.Exec_Query_only("beld_acc_balances");
                       // daml.Exec_Query_only("db" + BL.CLS_Session.cmp_id + "y" + txt_oyr.Text + ".dbo.beld_sup_balances");
                       // daml.Exec_Query_only("db" + BL.CLS_Session.cmp_id + "y" + txt_oyr.Text + ".dbo.beld_sup_balances_all_br");
                       // daml.Exec_Query_only("copy_acc_balances_from_old_db @db='db" + BL.CLS_Session.cmp_id + "y" + txt_oyr.Text + "' , @cond='sup'");
                        daml.Exec_Query_only( txt_oyr.Text + ".dbo.beld_sup_balances_all_br");
                        daml.Exec_Query_only("copy_acc_balances_from_old_db @db='" + txt_oyr.Text + "' , @cond='sup'");
                        MessageBox.Show("تم نقل ارصدة الموردين بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        // bk.MdiParent = this;
                        // bk.Show();
                        // Application.Exit();
                        wf.Close();
                    }
                    if (rd_sto_copy.Checked)
                    {
                        //WaitForm wf = new WaitForm("");
                        //wf.MdiParent = MdiParent;
                        wf.Show();
                        Application.DoEvents();
                        // daml.Exec_Query_only("beld_acc_balances");
                       // daml.Exec_Query_only("db" + BL.CLS_Session.cmp_id + "y" + txt_oyr.Text + ".dbo.bld_whbins_cost_all");
                       // daml.Exec_Query_only("copy_stock_balances_from_old_db @db='db" + BL.CLS_Session.cmp_id + "y" + txt_oyr.Text + "'");
                        daml.Exec_Query_only("copy_stock_balances_from_old_db @db='" + txt_oyr.Text + "', @whno='" + (cmb_wh.SelectedIndex == -1 ? "" : cmb_wh.SelectedValue) + "', @allwh=" + (cmb_wh.SelectedIndex == -1 ? 0 : 1) +"" );
                        MessageBox.Show("تم نقل المخزون بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        // bk.MdiParent = this;
                        // bk.Show();
                        // Application.Exit();
                        wf.Close();
                    }
                }
                else
                {
                    if (chk_copy_files.Checked && panel1.Enabled==false)
                    {
                        //WaitForm wf = new WaitForm("");
                        //wf.MdiParent = MdiParent;
                        wf.Show();
                        Application.DoEvents();

                        daml.Exec_Query_only("copy_data_from_old_db @old_db='db" + BL.CLS_Session.cmp_id + "y" + txt_oyr.Text + "'");
                        MessageBox.Show("تم نقل ملفات التعريف بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        // bk.MdiParent = this;
                        // bk.Show();
                        // Application.Exit();
                        wf.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                wf.Close();
            }

         

           
        }

        private void Acc_Copy_Olddb_Load(object sender, EventArgs e)
        {
            string sl = BL.CLS_Session.stkey.Replace(" ", "','").Remove(0, 2) + "'";
            // MessageBox.Show(sl);
            DataTable dtslctr = daml.SELECT_QUIRY_only_retrn_dt("select wh_no,wh_name from whouses where wh_brno + wh_no in(" + sl + ") and wh_brno='" + BL.CLS_Session.brno + "'");



            cmb_wh.DataSource = dtslctr;
            cmb_wh.DisplayMember = "wh_name";
            cmb_wh.ValueMember = "wh_no";
            cmb_wh.SelectedIndex = -1;

            txt_oyr.Select(5, 0);
            //txt_oyr.Focus();
        }

        private void chk_copy_files_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_copy_files.Checked)
            {
                //rd_acc_copy.Checked = false;
                //rd_cus_copy.Checked = false;
                //rd_sup_copy.Checked = false;
                //rd_sto_copy.Checked = false;
                panel1.Enabled = false;
            //foreach(Control ctr in this.Controls)
            //{
            //if(ctr is RadioButton)
            //    ctr.Checked
            //}
            }
            else
            {
                panel1.Enabled = true;
            }
        }

        private void cmb_wh_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                cmb_wh.SelectedIndex = -1;
            }
        }
    }
}
