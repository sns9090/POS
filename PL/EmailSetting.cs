using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace POS
{
    public partial class EmailSetting : BaseForm
    {
        BL.DAML daml = new BL.DAML();
        BL.EncDec enco = new BL.EncDec();
        SqlConnection con = BL.DAML.con;

        public EmailSetting()
        {
            InitializeComponent();
        }

        private void EmailSetting_Load(object sender, EventArgs e)
        {
            DataTable dt = daml.SELECT_QUIRY_only_retrn_dt("select * from email");
            textBox1.Text=dt.Rows[0][0].ToString();
            textBox2.Text = dt.Rows[0][1].ToString();
            textBox3.Text = enco.Decrypt(dt.Rows[0][2].ToString(),true);
            textBox4.Text = dt.Rows[0][3].ToString();
            textBox5.Text = dt.Rows[0][5].ToString();
            if (Convert.ToBoolean(dt.Rows[0][4]) == true)
                checkBox1.Checked = true;
            else
                checkBox1.Checked = false;
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "")
            {
                daml.Exec_Query_only("update email set email='" + textBox2.Text + "',password='" + enco.Encrypt(textBox3.Text, true) + "',portno=" + textBox4.Text + ",ssl=" + (checkBox1.Checked ? 1 : 0) + ",smtp_server='" + textBox5.Text + "' where id=" + textBox1.Text + "");
                MessageBox.Show("modifed success تم التعديل", "ok", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btn_Save.Enabled = false;
            }
            else
            {
                MessageBox.Show("Fill all date", "ok", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox2_Validating(object sender, CancelEventArgs e)
        {
            System.Text.RegularExpressions.Regex rEMail = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$");

            if (textBox2.Text.Length > 0)
            {

                if (!rEMail.IsMatch(textBox2.Text))
                {

                    MessageBox.Show("E-Mail expected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    textBox2.SelectAll();

                    e.Cancel = true;

                }

            }


        }
    }
}
