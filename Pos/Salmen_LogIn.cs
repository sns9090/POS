using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace POS.Pos
{
    public partial class Salmen_LogIn : BaseForm
    {
        BL.DAML daml = new BL.DAML();
        BL.EncDec ende = new BL.EncDec();
       // SALES_D sd = new SALES_D();
        public DataTable dtsalmen,dtsss;
       // public bool isout = false;
        public Salmen_LogIn()
        {
            InitializeComponent();
        }

        private void Salmen_LogIn_Load(object sender, EventArgs e)
        {
            txt_salman.Select();
        }

        private void button2_Click(object sender, EventArgs e)
        {
           // this.Close();
        }

        private void txt_salman_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F8)
                {
                    Search_All f4 = new Search_All("8-1", "Usr");
                    f4.ShowDialog();



                      txt_salman.Text=f4.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                      txt_salnam.Text = f4.dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    //  textBox7.Focus();
                    //  dataGridView1.CurrentCell = this.dataGridView1[3, dataGridView1.CurrentRow.Index];

                    /*
                   dataGridView1.CurrentRow.Cells[0].Value = f7.listView2.SelectedItems[0].Text;
                   dataGridView1.CurrentRow.Cells[1].Value = f7.listView2.SelectedItems[0].SubItems[1].Text.Replace("|","");
                   dataGridView1.CurrentCell = this.dataGridView1[3, dataGridView1.CurrentRow.Index];
                    */


                }
                if (e.KeyCode == Keys.Enter)
                {
                   // txt_password.Select();
                    txt_password.SelectAll();
                    txt_password.Focus(); 
                }
            }
            catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((txt_salman.Text.Equals("0") || txt_salman.Text.Trim().Equals(""))&& BL.CLS_Session.is_sal_login == false)
            {
               // if(BL.CLS_Session.is_sal_login==false)
                this.Close();
            }
            else
            {

                dtsalmen = daml.SELECT_QUIRY_only_retrn_dt("select * from pos_salmen where sl_id=" + txt_salman.Text + " and sl_password='" + ende.Encrypt(txt_password.Text, true) + "' and sl_brno='" + BL.CLS_Session.brno + "' and sl_inactive=0");
               // daml.SELECT_QUIRY_only_retrn_dt("select *  from pos_salmen where sl_id=" + salid + " and sl_brno='" + BL.CLS_Session.brno + "'");

                if (dtsalmen.Rows.Count == 1)
                {
                    BL.CLS_Session.is_sal_login = true;
                    BL.CLS_Session.dtsalman = dtsalmen;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("رقم البائع او كلمة السر خطاء او غير فعال", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                   // txt_salman.Focus();
                    txt_salman.SelectAll();
                    txt_salman.Focus(); 
                }
            }
        }

        private void txt_password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.Select();
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void txt_salman_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_salman.Text))
                txt_salman.Text = "";
        }

        private void txt_salman_Leave(object sender, EventArgs e)
        {
            if (!txt_salman.Text.Trim().Equals(""))
            {
                dtsss = daml.SELECT_QUIRY_only_retrn_dt("select sl_name from pos_salmen where sl_id=" + txt_salman.Text + " and sl_brno='" + BL.CLS_Session.brno + "' and sl_inactive=0");
                if (dtsss.Rows.Count == 1)

                    txt_salnam.Text = dtsss.Rows[0][0].ToString();

                else
                {
                    txt_salnam.Text = "";
                    txt_salman.Text = "";
                }
            }
        }

        private void Salmen_LogIn_FormClosing(object sender, FormClosingEventArgs e)
        {
           // e.Cancel = true;
        }

        private void Salmen_LogIn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt && e.KeyCode == Keys.F4)
            {
                e.Handled = true;
            }
        }
    }
}
