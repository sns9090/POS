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
    public partial class Acc_bdgt : BaseForm
    {
        SqlConnection con = BL.DAML.con;
        BL.DAML daml = new BL.DAML();
        string a_key, a_name;
        
        public Acc_bdgt(string akey , string aname)
        {
            InitializeComponent();
            a_key = akey;
            a_name = aname;
        }

        private void Acc_bdgt_Load(object sender, EventArgs e)
        {
            txt_code.Text = a_key;
            txt_name.Text = a_name;
            textBox1.Focus();
            DataTable dt = daml.SELECT_QUIRY_only_retrn_dt("select * from accbdgt where bacc='" + a_key + "' and brno='" + BL.CLS_Session.brno + "'");

            if (dt.Rows.Count > 0)
            {
                textBox1.Text = dt.Rows[0]["bubal01"].ToString();
                textBox2.Text = dt.Rows[0]["bubal02"].ToString();
                textBox3.Text = dt.Rows[0]["bubal03"].ToString();
                textBox4.Text = dt.Rows[0]["bubal04"].ToString();
                textBox5.Text = dt.Rows[0]["bubal05"].ToString();
                textBox6.Text = dt.Rows[0]["bubal06"].ToString();
                textBox7.Text = dt.Rows[0]["bubal07"].ToString();
                textBox8.Text = dt.Rows[0]["bubal08"].ToString();
                textBox9.Text = dt.Rows[0]["bubal09"].ToString();
                textBox10.Text = dt.Rows[0]["bubal10"].ToString();
                textBox11.Text = dt.Rows[0]["bubal11"].ToString();
                textBox12.Text = dt.Rows[0]["bubal12"].ToString();
                textBox13.Text = dt.Rows[0]["year_bgt"].ToString();
            }
           

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable dtchk = daml.SELECT_QUIRY_only_retrn_dt("select * from accbdgt where bacc='" + a_key + "' and brno='"+BL.CLS_Session.brno+"'");
            if (dtchk.Rows.Count > 0 && Convert.ToDouble(textBox13.Text)>0)
            {
                daml.Exec_Query_only("update accbdgt set bubal01=" + textBox1.Text + ",bubal02=" + textBox2.Text + ",bubal03=" + textBox3.Text + ",bubal04=" + textBox4.Text + ",bubal05=" + textBox5.Text + ",bubal06=" + textBox6.Text + ",bubal07=" + textBox7.Text + ",bubal08=" + textBox8.Text + ",bubal09=" + textBox9.Text + ",bubal10=" + textBox10.Text + ",bubal11=" + textBox11.Text + ",bubal12=" + textBox12.Text + ",year_bgt="+textBox13.Text+" where bacc='"+txt_code.Text+"' and brno='"+BL.CLS_Session.brno+"'");
            }
            else
            {
                if (dtchk.Rows.Count > 0 && Convert.ToDouble(textBox13.Text) == 0)
                    daml.Exec_Query_only(@"delete from  accbdgt where bacc='" + txt_code.Text + "' and brno='" + BL.CLS_Session.brno + "'");
                    else
                    daml.Exec_Query_only(@"insert into accbdgt(brno, bacc, bufcy, bubal01, bubal02, bubal03, bubal04, bubal05, bubal06, bubal07, bubal08, bubal09, bubal10, bubal11, bubal12, year_bgt) values('"+BL.CLS_Session.brno+"','"+a_key+"','',"+textBox1.Text+","+textBox2.Text+","+textBox3.Text+","+textBox4.Text+","+textBox5.Text+","+textBox6.Text+","+textBox7.Text+","+textBox8.Text+","+textBox9.Text+","+textBox10.Text+","+textBox11.Text+","+textBox12.Text+","+textBox13.Text+")");
            }
            button1.Enabled = false;
            panel2.Enabled = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            total();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            total();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            total();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            total();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            total();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            total();
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            total();
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            total();
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            total();
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            total();
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            total();
        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {
            total();
        }

        private void total()
        {
            double sum =Convert.ToDouble(textBox1.Text) + Convert.ToDouble(textBox2.Text)+Convert.ToDouble(textBox3.Text)+Convert.ToDouble(textBox4.Text)+Convert.ToDouble(textBox5.Text)+Convert.ToDouble(textBox6.Text)+Convert.ToDouble(textBox7.Text)+Convert.ToDouble(textBox8.Text) + Convert.ToDouble(textBox9.Text) +Convert.ToDouble(textBox10.Text)+Convert.ToDouble(textBox11.Text) + Convert.ToDouble(textBox12.Text);
            textBox13.Text = sum.ToString();
        }

    }
}
