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
    public partial class Chang_UsrPass : BaseForm
    {
        BL.EncDec enco = new BL.EncDec();
        BL.DAML daml = new BL.DAML();
        string u_id;
        public Chang_UsrPass(string uid)
        {
            InitializeComponent();
            u_id = uid;
        }

        private void Chang_UsrPass_Load(object sender, EventArgs e)
        {
            txt_uid.Text = u_id;
        }

        private void txt_oldp_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txt_oldp_Leave(object sender, EventArgs e)
        {
            DataTable dtp = daml.SELECT_QUIRY_only_retrn_dt("select password from users where user_name='" + txt_uid.Text + "'");
            if (enco.Encrypt(txt_oldp.Text, true).Equals(dtp.Rows[0][0].ToString()))
                txt_newp.Focus();
            else
            {
                MessageBox.Show("كلمة السر خطا", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_oldp.Text = "";
                txt_oldp.Focus();
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            if (txt_newp.Text.Trim().Length<4)
            {
                MessageBox.Show("يجب ان يكون طول كلمة السر 4 ارقام او اكثر", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_cnewp.Focus();
                return;
            }

            if (txt_newp.Text.Equals(txt_cnewp.Text))
            {
                daml.Exec_Query_only("update users set password='" + enco.Encrypt(txt_newp.Text, true) + "' where user_name='" + txt_uid.Text + "'");
                MessageBox.Show("تم تغيير كلمة السر بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                this.Close();
            }
            else
            {
                MessageBox.Show("كلمة كلمة السر غير مطابقة", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_cnewp.Focus();
            }

        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
