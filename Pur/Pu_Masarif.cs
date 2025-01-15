using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace POS.Pur
{
    public partial class Pu_Masarif : BaseForm
    {
        BL.DAML daml = new BL.DAML();

        public Pu_Masarif()
        {
            InitializeComponent();
        }

        private void txt_name_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_key_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            total();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            total();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (Convert.ToDouble(txt_total.Text) > 0 && string.IsNullOrEmpty(txt_key.Text))
            {
                MessageBox.Show("يجب ادخال الحساب الوسيط","",MessageBoxButtons.OK,MessageBoxIcon.Error);
                txt_key.Focus();
                return;
            }
            txt_key_Leave( sender,  e);

            this.Close();
        }

        public void txt_key_Leave(object sender, EventArgs e)
        {
            DataTable dt = daml.SELECT_QUIRY_only_retrn_dt("select a_name from accounts where a_key='" + txt_key.Text + "' and a_brno='" + BL.CLS_Session.brno + "'");
            // dataGridView1.DataSource = daml.SELECT_QUIRY(qstr2);

            if (dt.Rows.Count > 0)
                txt_name.Text = dt.Rows[0][0].ToString();
            else
                txt_name.Text = "";
        }

        private void txt_key_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F8)
            {
                txt_name.Text = "";
                try
                {
                    Search_All f4 = new Search_All("1", "Acc");
                    f4.ShowDialog();
                    txt_key.Text = f4.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    txt_name.Text = f4.dataGridView1.CurrentRow.Cells[1].Value.ToString();
                }
                catch { }

            }
        }

        private void Pu_Masarif_Load(object sender, EventArgs e)
        {

        }

        private void total()
        {

            txt_total.Text = (Convert.ToDouble(txt_gmark.Text) + Convert.ToDouble(txt_tamin.Text) + Convert.ToDouble(txt_shn.Text) + Convert.ToDouble(txt_sfr.Text) + Convert.ToDouble(txt_other.Text) + Convert.ToDouble(txt_etax.Text) + Convert.ToDouble(txt_sabr.Text)).ToString();
        }

        private void txt_gmark_TextChanged(object sender, EventArgs e)
        {
            total();
        }

        private void txt_shn_TextChanged(object sender, EventArgs e)
        {
            total();
        }

        private void txt_other_TextChanged(object sender, EventArgs e)
        {
            total();
        }

        private void txt_etax_TextChanged(object sender, EventArgs e)
        {
            total();
        }
    }
}
