using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace POS
{
    public partial class Del_Pro : BaseForm
    {
        SqlConnection con2 = BL.DAML.con;//

        public Del_Pro()
        {
            InitializeComponent();
            
        }

        private void Del_Pro_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlCommand cmd1 = new SqlCommand("Del_Product", con2))
            {

                cmd1.CommandType = CommandType.StoredProcedure;

              //  cmd1.Parameters.AddWithValue("@no", textBox1.Text);
              //  cmd1.Parameters.AddWithValue("@barcode", textBox2.Text);

                cmd1.Parameters.Add("@no", SqlDbType.NVarChar, 16).Value = textBox1.Text;
                cmd1.Parameters.Add("@barcode", SqlDbType.NVarChar, 16).Value = textBox2.Text;


                con2.Open();
               // cmd1.ExecuteNonQuery();
                int ok=  cmd1.ExecuteNonQuery();
                con2.Close();

                if (ok == 1)
                {

                    MessageBox.Show("success");
                }
                else
                {
                    MessageBox.Show("failed");
                }
            }
        }
    }
}
