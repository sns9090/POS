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
    public partial class ManagmentQuery : BaseForm
    {
        BL.DAML daml = new BL.DAML();

        public ManagmentQuery()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                daml.SqlCon_Open();

                int pass = 0;
                if (Convert.ToInt32(DateTime.Now.Day) % 2 == 0)
                    pass = ((Convert.ToInt32(DateTime.Now.Year) + Convert.ToInt32(DateTime.Now.Month)) * 411) - (Convert.ToInt32(DateTime.Now.Day) * 114);
                else
                    pass = ((Convert.ToInt32(DateTime.Now.Year) + Convert.ToInt32(DateTime.Now.Month)) * 311) - (Convert.ToInt32(DateTime.Now.Day) * 113);

               // if (textBox1.Text == "sa735356688")
                //if (Convert.ToInt32(textBox1.Text.Trim()) == pass)
                if (textBox1.Text.Trim().Equals(pass.ToString()))
                {
                    if (textBox2.Text.StartsWith("se") || textBox2.Text.StartsWith("S"))
                    {
                        dataGridView1.DataSource = daml.SELECT_QUIRY_only_retrn_dt(textBox2.Text);
                        label1.Text = "(" + dataGridView1.Rows.Count.ToString() + " row(s) affected)";
                       // daml.SqlCon_Msg();
                     //   MessageBox.Show(BL.DAML.var3);
                    }
                    else
                    {
                        label1.Text = "(" + daml.Insert_Update_Delete_retrn_int(textBox2.Text, false).ToString() + " row(s) affected)";
                       // daml.SqlCon_Msg();
                     //   MessageBox.Show(BL.DAML.var3);
                    }

                }
                else
                {
                    MessageBox.Show("PassWord Error", " Note تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                daml.SqlCon_Close();
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                daml.SqlCon_Open();
                if (textBox3.Text == "sa735356688")
                {
                    //daml.SqlCon_Open();
                    //daml.ExecuteCommand("Delete_Trx", null);
                    //MessageBox.Show("Done");
                    //daml.SqlCon_Close();
                    daml.Insert_Update_Delete_retrn_int("Delete_Trx", true);
                    label1.Text = "st pro executed";
                }
                else
                {
                    MessageBox.Show("Pass Error");
                }
                daml.SqlCon_Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ManagmentQuery_Load(object sender, EventArgs e)
        {

        }
    }
}
