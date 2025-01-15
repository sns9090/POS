using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace POS.Sal
{
    public partial class Sal_Chang_Type : BaseForm
    {
        DataTable dttrtyps, dtslctr;
        BL.DAML daml = new BL.DAML();

        public Sal_Chang_Type()
        {
            InitializeComponent();
        }

        private void Sal_Chang_Type_Load(object sender, EventArgs e)
        {
            

            string tr = BL.CLS_Session.trkey.Replace(" ", "','").Remove(0, 2) + "'";
            // MessageBox.Show(tr);
            dttrtyps = daml.SELECT_QUIRY_only_retrn_dt("select * from trtypes where tr_no in(" + tr + ") and tr_no in('04','05')");
            cmb_type.DataSource = dttrtyps;
            cmb_type.DisplayMember = BL.CLS_Session.lang.Equals("E") ? "tr_ename" : "tr_name";
            cmb_type.ValueMember = "tr_no";
            cmb_type.SelectedIndex = -1;

            dttrtyps = daml.SELECT_QUIRY_only_retrn_dt("select * from trtypes where tr_no in(" + tr + ") and tr_no in('04','05')");
            cmb_type2.DataSource = dttrtyps;
            cmb_type2.DisplayMember = BL.CLS_Session.lang.Equals("E") ? "tr_ename" : "tr_name";
            cmb_type2.ValueMember = "tr_no";
            cmb_type2.SelectedIndex = -1;


            string sl = BL.CLS_Session.slkey.Replace(" ", "','").Remove(0, 2) + "'";
            // MessageBox.Show(sl);
            dtslctr = daml.SELECT_QUIRY_only_retrn_dt("select * from slcenters where sl_brno + sl_no in(" + sl + ") and sl_brno='" + BL.CLS_Session.brno + "'");
            cmb_salctr.DataSource = dtslctr;
            cmb_salctr.DisplayMember = "sl_name";
            cmb_salctr.ValueMember = "sl_no";
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void cmb_type_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmb_type_Leave(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (cmb_type.SelectedIndex == -1 || cmb_type2.SelectedIndex == -1 || cmb_salctr.SelectedIndex == -1 || txt_oldno.Text.Trim().Equals(""))
            {
                MessageBox.Show("يرجى ادخال جميع الحقول المطلوبة", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // cmb_type.Focus();
                return;
            }

            if (cmb_type.SelectedValue == cmb_type2.SelectedValue)
            {
                MessageBox.Show("عند التغيير يجب ان يكون نوع الفاتورة مختلف", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //cmb_type.Focus();
                return;
            }

            string qr = @"BEGIN TRANSACTION;  
                          BEGIN TRY 
                             update sales_hdr set invtype='" + cmb_type2.SelectedValue + "' where ref=" + txt_oldno.Text + " and branch='" + BL.CLS_Session.brno + "' and invtype='" + cmb_type.SelectedValue + "' and slcenter='" + cmb_salctr.SelectedValue + "';"
                         + " update acc_hdr set a_type='" + cmb_type2.SelectedValue + "' where a_ref=" + txt_oldno.Text + " and a_brno='" + BL.CLS_Session.brno + "' and a_type='" + cmb_type.SelectedValue + "' and sl_no='" + cmb_salctr.SelectedValue + "';"
	                     +@" COMMIT;  	  
	                      END TRY

                          BEGIN CATCH
                             ROLLBACK; 
                          END CATCH";

            int exec = daml.Insert_Update_Delete_retrn_int(qr, false);

            if (exec > 0)
            {
                MessageBox.Show("تم التغيير بنجاح","تنبيه",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
              //  MessageBox.Show("يرجى اعادة حفظ الفاتورة من شاشة المبيعات","تنبيه",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                if (BL.CLS_Session.cmp_type.StartsWith("a"))
                {
                    Sal.Sal_Tran_D_TF rs = new Sal.Sal_Tran_D_TF(cmb_salctr.SelectedValue.ToString(), cmb_type2.SelectedValue.ToString(), txt_oldno.Text);
                    rs.MdiParent = MdiParent;
                    rs.btn_Edit_Click(sender,e);
                    rs.btn_Undo.Enabled = false;
                    rs.Show();
                }
                else
                {
                    Sal.Sal_Tran_D rs = new Sal.Sal_Tran_D(cmb_salctr.SelectedValue.ToString(), cmb_type2.SelectedValue.ToString(), txt_oldno.Text);
                    rs.MdiParent = MdiParent;
                    
                    rs.Show();
                    rs.btn_Edit_Click(sender, e);
                    rs.btn_Undo.Enabled = false;
                }
            }
            else
                MessageBox.Show("لم يتم التغيير بنجاح", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
