using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace POS.Sto
{
    public partial class Items_Bld : BaseForm
    {
        SqlConnection con = BL.DAML.con;
        BL.DAML daml = new BL.DAML();
        public Items_Bld()
        {
            InitializeComponent();
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WaitForm wf = new WaitForm("يرجى الانتظار ... جاري اعادة البناء");
            wf.MdiParent = MdiParent;

            try
            {
                // daml.SqlCon_Open();
                // daml.Exec_Query("bld_whbins");
                // daml.Exec_Query_only("bld_whbins_sl_pu");
                //   DialogResult result = MessageBox.Show("Do you want to bild items now هل تريد بناء ارصدة الاصناف الان", "تنبيه", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Asterisk);
                // if (result == DialogResult.Yes)
                if (rd_qty_cost.Checked)
                {
                    wf.Show();
                    Application.DoEvents();

                    if(checkBox1.Checked)
                        daml.Exec_Query_only("bld_whbins_cost_all_ok @posted=1");
                    else
                        daml.Exec_Query_only("bld_whbins_cost_all_ok @posted=0");
                    wf.Close();
                    MessageBox.Show("تم بناء الارصدة والتكاليف بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    // bk.MdiParent = this;
                    // bk.Show();
                    // Application.Exit();

                }
                //  else if (result == DialogResult.No)
                if (rd_sal_cost.Checked)
                {
                    wf.Show();
                    Application.DoEvents();
                    // if (this.ActiveMdiChild != null)
                    // int fcont = Application.OpenForms.Count;

                    daml.Exec_Query_only("bld_sal_cost_all @br_no='" + BL.CLS_Session.brno + "'");
                    wf.Close();
                    MessageBox.Show("تم بناء تكاليف المبيعات بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                    //  this.ActiveMdiChild.Close();
                    // daml.Exec_Query_only("update tobld set id=1");
                    // Application.Exit();
                    // wf.Close();
                    //...
                }

                if (rd_cus_copy.Checked)
                {
                    wf.Show();
                    Application.DoEvents();
                    // daml.Exec_Query_only("beld_cust_balances @branch_no='"+BL.CLS_Session.brno+"'");
                    daml.Exec_Query_only("beld_cust_balances_all_br");
                    wf.Close();
                    MessageBox.Show("تم بناء ارصدة العملاء بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    // bk.MdiParent = this;
                    // bk.Show();
                    // Application.Exit();
                    // wf.Close();
                }

                if (rd_sup_copy.Checked)
                {
                    wf.Show();
                    Application.DoEvents();
                    // daml.Exec_Query_only("beld_sup_balances @branch_no='" + BL.CLS_Session.brno + "'");
                    daml.Exec_Query_only("beld_sup_balances_all_br");
                    wf.Close();
                    MessageBox.Show("تم بناء ارصدة الموردين بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    // bk.MdiParent = this;
                    // bk.Show();
                    // Application.Exit();
                    //  wf.Close();
                }

                if (rd_acc_copy.Checked)
                {
                    wf.Show();
                    Application.DoEvents();
                    daml.Exec_Query_only("beld_acc_balances_all");
                    wf.Close();
                    MessageBox.Show("تم بناء ارصدة الحسابات بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    // bk.MdiParent = this;
                    // bk.Show();
                    // Application.Exit();
                    //  wf.Close();
                }

                
               // if (textBox1.Text == "sa735356688")
                //if (Convert.ToInt32(textBox1.Text.Trim()) == pass)
                
                    if (rd_convrt_qtcost.Checked)
                    {
                        int pass = 0;
                        if (Convert.ToInt32(DateTime.Now.Day) % 2 == 0)
                            pass = ((Convert.ToInt32(DateTime.Now.Year) + Convert.ToInt32(DateTime.Now.Month)) * 411) - (Convert.ToInt32(DateTime.Now.Day) * 114);
                        else
                            pass = ((Convert.ToInt32(DateTime.Now.Year) + Convert.ToInt32(DateTime.Now.Month)) * 311) - (Convert.ToInt32(DateTime.Now.Day) * 113);
                        if (textBox1.Text.Trim().Equals(pass.ToString()))
                        {
                            wf.Show();
                            Application.DoEvents();
                            // if (this.ActiveMdiChild != null)
                            // int fcont = Application.OpenForms.Count;

                            daml.Exec_Query_only("bld_qty_convert_cost_all @br_no='" + BL.CLS_Session.brno + "'");
                            wf.Close();
                            MessageBox.Show("تم بناء تكاليف سندات التحويل", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                        else
                        {
                            MessageBox.Show("PassWord Error خطا كلمة السر", " Note تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                
            }
            catch (Exception ex)
            {
                wf.Close();
                MessageBox.Show(ex.Message);
                
            }
            finally
            {
               // wf.Close();
            }
        }

        private void Items_Bld_Load(object sender, EventArgs e)
        {
            if (BL.CLS_Session.is_dorymost.Equals("2"))
                rd_sal_cost.Enabled = false;
        }

        private void rd_qty_cost_CheckedChanged(object sender, EventArgs e)
        {
            if (rd_qty_cost.Checked)
            {
               // checkBox1.Checked = true;
                checkBox1.Enabled = true;
            }
            else
            {
                checkBox1.Checked = false;
                checkBox1.Enabled = false;
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            daml.Exec_Query_only("bld_whbins_cost_all_posted");
            MessageBox.Show("تم بناء الارصدة والتكاليف بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
        }
    }
}
