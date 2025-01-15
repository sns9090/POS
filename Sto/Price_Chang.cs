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
    public partial class Price_Chang : BaseForm
    {
        SqlConnection con2 = BL.DAML.con;
        public Price_Chang()
        {
            InitializeComponent();
        }

        private const int CP_DISABLE_CLOSE_BUTTON = 0x200;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ClassStyle = cp.ClassStyle | CP_DISABLE_CLOSE_BUTTON;
                return cp;
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txt_newp.Text) || txt_newp.Text.Equals("0") || string.IsNullOrEmpty(txt_code.Text))
                {
                    MessageBox.Show(BL.CLS_Session.lang.Equals("E") ? "Enter Item and New Price" : "ادخل الصنف والسعر الجديد", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txt_newp.Focus();
                    return;
                
                }
                using (SqlCommand cmd1 = new SqlCommand("update items set item_price=@a11 where item_barcode=@a33", con2))
                {


                    cmd1.Parameters.AddWithValue("@a11", txt_newp.Text);
                    cmd1.Parameters.AddWithValue("@a33", txt_code.Text);
                    con2.Open();
                    cmd1.ExecuteNonQuery();
                    con2.Close();
                }
                using (SqlCommand cmd2 = new SqlCommand("update items_bc set price=@a22 where barcode=@a44 and sl_no=@a66", con2))
                {


                    cmd2.Parameters.AddWithValue("@a22", txt_newp.Text);
                    cmd2.Parameters.AddWithValue("@a44", txt_code.Text);
                    cmd2.Parameters.AddWithValue("@a66", BL.CLS_Session.sl_prices);
                    con2.Open();
                    cmd2.ExecuteNonQuery();
                    con2.Close();
                }

                MessageBox.Show(BL.CLS_Session.lang.Equals("E") ? "Item Price Changed" : "تم تعديل سعر الصنف", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                txt_code.Text = "";
                txt_name.Text = "";
                txt_oldp.Text = "";
                txt_newp.Text = "";
                //using (SqlCommand cmd3 = new SqlCommand("update brprices set lprice1=@a111 where itemno=@a333 and branch=@a444", con2))
                //{
                txt_code.Select();

                //    cmd3.Parameters.AddWithValue("@a111", Convert.ToDouble(txt_brprice.Text));
                //    //  cmd3.Parameters.AddWithValue("@a22", textBox4.Text);
                //    cmd3.Parameters.AddWithValue("@a333", textBox1.Text);
                //    cmd3.Parameters.AddWithValue("@a444", BL.CLS_Session.brno);

                //    con2.Open();
                //    cmd3.ExecuteNonQuery();
                //    con2.Close();
                //}
            
            }

            catch { }
        }

        private void txt_code_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F8)
                {
                    // var selectedCell = dataGridView1.SelectedCells[0];
                    // do something with selectedCell...
                    Search_All f4 = new Search_All("2", "Sto");
                    f4.ShowDialog();



                    txt_code.Text = f4.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    txt_name.Text = f4.dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    //  dataGridView1.CurrentCell = this.dataGridView1[3, dataGridView1.CurrentRow.Index];

                    /*
                   dataGridView1.CurrentRow.Cells[0].Value = f7.listView2.SelectedItems[0].Text;
                   dataGridView1.CurrentRow.Cells[1].Value = f7.listView2.SelectedItems[0].SubItems[1].Text.Replace("|","");
                   dataGridView1.CurrentCell = this.dataGridView1[3, dataGridView1.CurrentRow.Index];
                    */



                }
                if (e.KeyCode == Keys.Enter)
                {


                    if (!string.IsNullOrEmpty(txt_code.Text))
                    {

                        string condi = label8.Text.Equals("الباركود") || label8.Text.Equals("Barcode") ? " and b.barcode='" + txt_code.Text + "'" : " and i.item_no='" + txt_code.Text + "'";
                        // select i.item_no , i.item_name,i.item_cost,b.price as item_price,b.barcode as  item_barcode,b.pack,i.item_obalance,i.item_cbalance,i.item_group,i.item_image,i.item_req,t.tax_percent,b.pk_qty pkqty,i.item_tax i_tax,i.item_image img from items i join items_bc b on b.item_no=i.item_no and i.item_no='" + textBox3.Text + "' join taxs t on i.item_tax=t.tax_id", con2);
                        SqlDataAdapter da23 = new SqlDataAdapter("select i.item_no , i.item_name,i.item_cost,b.price as item_price,b.barcode as  item_barcode,b.pack,i.item_obalance,i.item_cbalance,i.item_group,i.item_image,i.item_req,t.tax_percent,b.pk_qty pkqty,i.item_tax i_tax,i.item_image img from items i join items_bc b on b.item_no=i.item_no " + condi + " join taxs t on i.item_tax=t.tax_id where b.sl_no='" + BL.CLS_Session.sl_prices + "'", con2);
                        DataTable dt = new DataTable();
                        da23.Fill(dt);

                        if (dt.Rows.Count == 1)
                        {
                            txt_name.Text = dt.Rows[0][1].ToString();
                            txt_oldp.Text = dt.Rows[0][3].ToString();
                            txt_newp.Select();
                        }
                        else
                        {
                            MessageBox.Show("الصنف غير موجود","",MessageBoxButtons.OK,MessageBoxIcon.Error);
                            txt_code.Text = "";
                            txt_name.Text = "";
                            txt_oldp.Text = "0.00";
                            txt_newp.Text = "0";
                            txt_code.Select();
                        }
                    }
                    else
                    {
                        MessageBox.Show("ادخل رقم الصنف", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txt_code.Select();

                    }

                }
            }
            catch
            { }


                
        }

        private void Price_Chang_Load(object sender, EventArgs e)
        {
            txt_code.Select();
        }

        private void txt_newp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.Select();
            }
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            if (label8.Text.Equals("الباركود") || label8.Text.Equals("Barcode"))
                label8.Text =BL.CLS_Session.lang.Equals("E") ? "Item No" : "رقم الصنف";
            else
                label8.Text =BL.CLS_Session.lang.Equals("E") ? "Barcode" : "الباركود";
        }
    }
}
