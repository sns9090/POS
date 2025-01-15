using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;

namespace POS.Pos
{
    public partial class SalesDtlReport : BaseForm
    {
        string condh,condd;
        SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        public SalesDtlReport( string cond)
        {
            InitializeComponent();
            condh = cond.Equals("1") ? " pos_hdr " : " d_pos_hdr ";
            condd = cond.Equals("1") ? " pos_dtl " : " d_pos_dtl ";
        }

        private void SalesDtlReport_Load(object sender, EventArgs e)
        {
            int temp;

            if (DilySalesReport.cc >0)
            {
                temp = DilySalesReport.cc;
            }
            else
            {
                temp = Acc.Acc_Report.qq;
            }


           // string condcncl = checkBox1.Checked ? " pos_hdr " : " d_pos_hdr ";
            
                SqlDataAdapter da = new SqlDataAdapter("select * from "+condd+" where ref ='" + temp + "'", con2);
                SqlDataAdapter da2 = new SqlDataAdapter("select * from "+condh+" where ref ='" + temp + "'", con2);
                DataSet ds = new DataSet();
                da.Fill(ds, "0");
                da2.Fill(ds, "1");

                label1.Text = ds.Tables["1"].Rows[0][1].ToString();

                label2.Text = ds.Tables["1"].Rows[0][2].ToString();
                label3.Text = ds.Tables["1"].Rows[0][4].ToString();
                label4.Text = ds.Tables["1"].Rows[0][5].ToString();
                label9.Text = ds.Tables["1"].Rows[0][6].ToString();

                label12.Text = ds.Tables["1"].Rows[0][9].ToString();


                dataGridView1.DataSource = ds.Tables["0"];

                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].Visible = false;
                dataGridView1.Columns[2].Visible = false;
               // dataGridView1.Columns[0].Visible = false;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
