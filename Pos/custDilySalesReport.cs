using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;
using Microsoft.Office.Interop.Excel;

namespace POS.Pos
{
    public partial class custDilySalesReport : BaseForm
    {
        public static int cc;
       // string typeno = "";
        SqlConnection con3 = BL.DAML.con;//
        public custDilySalesReport()
        {
            InitializeComponent();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "")
                { MessageBox.Show("لا يوجد عميل مرتبط", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                else
                {
                    string xxx;
                    xxx = maskedTextBox1.Text.ToString();

                    DateTime zzz = Convert.ToDateTime(xxx).AddHours(24);
                    // yyy = dateTimePicker2.Text + " 04:59:59 ص";
                    SqlDataAdapter da3 = new SqlDataAdapter("select [ref],[contr],[type],[date],[total],[count],[payed],[total_cost],[saleman] from pos_hdr where date between'" + xxx + "' and '" + zzz.ToString("yyyy-MM-dd HH:mm:ss") + "' and cust_no = " + Convert.ToInt32(textBox1.Text) + "", con3);
                    //  SqlDataAdapter da3 = new SqlDataAdapter("select * from pos_hdr where date like'%" + xxx + "%'", con3);

                    // SqlDataAdapter da4 = new SqlDataAdapter("select sum(total) from pos_hdr where date between'" + xxx + "' and '" + yyy + "'", con3);
                    SqlDataAdapter da4 = new SqlDataAdapter("select sum(total),sum(total_cost),sum(payed) from pos_hdr where date between'" + xxx + "' and '" + zzz.ToString("yyyy-MM-dd HH:mm:ss") + "' and cust_no = " + Convert.ToInt32(textBox1.Text) + "", con3);
                    DataSet ds3 = new DataSet();
                    da3.Fill(ds3, "0");
                    da4.Fill(ds3, "1");
                    dataGridView1.DataSource = ds3.Tables["0"];
                    label1.Text = ds3.Tables[1].Rows[0][0].ToString();
                    label3.Text = ds3.Tables[1].Rows[0][1].ToString();
                    label5.Text = ds3.Tables[1].Rows[0][2].ToString();

                    label9.Text = Convert.ToString(Convert.ToDouble(label1.Text) - Convert.ToDouble(label5.Text));

                    dataGridView1.Columns[9].Visible = false;
                    dataGridView1.Columns[10].Visible = false;
                }
            }
            catch (Exception ex)
            {
               // MessageBox.Show(ex.Message);
            }
            
        }

        private void DilySalesReport_Load(object sender, EventArgs e)
        {
            // dateTimePicker1.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            maskedTextBox1.Text = DateTime.Now.ToString("yyyy-MM-dd") + "05:00:00";
            //string xxx;
            //xxx = maskedTextBox1.Text.ToString();
            //DateTime zzz = Convert.ToDateTime(xxx).AddHours(24);

            //label3.Text = zzz.ToString("yyyy-MM-dd HH:mm:ss");
        }



        private void dataGridView1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
            {
                // int selectedIndex = dataGridView1.CurrentCell.RowIndex;
                DilySalesReport.cc = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                // if (selectedIndex > -1)
                // {
                //dataGridView1.Rows.RemoveAt(selectedIndex);
                //dataGridView1.Refresh(); // if needed


                // }
                SalesDtlReport sdtr = new SalesDtlReport("1");
                //MAIN mn = new MAIN();
                //sdtr.MdiParent = mn;

                sdtr.ShowDialog();


            }
        }




        private void copyAlltoClipboard()
        {
            dataGridView1.SelectAll();
            DataObject dataObj = dataGridView1.GetClipboardContent();
            if (dataObj != null)
                Clipboard.SetDataObject(dataObj);
        }


        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.RightToLeft = RightToLeft.No;
            copyAlltoClipboard();
            Microsoft.Office.Interop.Excel.Application xlexcel;
            Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;
            xlexcel = new Microsoft.Office.Interop.Excel.Application();
            xlexcel.Visible = true;
            xlWorkBook = xlexcel.Workbooks.Add(misValue);
            xlWorkSheet = xlWorkBook.Worksheets.get_Item(1) as Microsoft.Office.Interop.Excel.Worksheet;

            string sHeaders = "";

            for (int j = 0; j < dataGridView1.Columns.Count; j++)
            {
                sHeaders = sHeaders.ToString() + Convert.ToString(dataGridView1.Columns[j].HeaderText) + "\t";
            }
            // stOutput += sHeaders + "\r\n";





            xlWorkSheet.Cells[1, 1] = "رقم الفاتورة";
            xlWorkSheet.Cells[1, 2] = "النوع";
            xlWorkSheet.Cells[1, 3] = "رقم الكاشير";
            xlWorkSheet.Cells[1, 4] = "التاريخ";
            xlWorkSheet.Cells[1, 5] = "الكمية";
            xlWorkSheet.Cells[1, 6] = "المجموع";
            xlWorkSheet.Cells[1, 7] = "المبلغ المدفوع";
            xlWorkSheet.Cells[1, 8] = "التكلفة";
            //---------------------------------------------
            /*
            // Changing the Column Width.
            sheet.Range["A1"].ColumnWidth = 20;
            sheet.Range["B1"].ColumnWidth = 30;
            sheet.Range["C1"].ColumnWidth = 40;
            sheet.Range["D1"].ColumnWidth = 50;

            // Changing the Row Height.
            sheet.Range["A2"].RowHeight = 20;
            sheet.Range["A4"].RowHeight = 35;
            sheet.Range["A5"].RowHeight = 50;
            sheet.Range["A7"].RowHeight = 60; */
            xlWorkSheet.Range["A1"].RowHeight = 60;
            xlWorkSheet.Range["D1"].ColumnWidth = 20;


            for (int i = 1; i <= 7; i++)
            {
                // xlWorkSheet.Cells[1, i].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Green);
                //xlWorkSheet.Cells[1, i].Style.Font.Size = 20;
                // xlWorkSheet.Cells[1, i].Font.Size = 12;
                // xlWorkSheet.Cells[1, i].Font.Bold = true;
                //  xlWorkSheet.Cells[1, i].EntireColumn.ColumnWidth = 15;
                // xlWorkSheet.Cells[1, i].EntireRow.RowHeight = 15;

            }

            Microsoft.Office.Interop.Excel.Range CR = xlWorkSheet.Cells[2, 1] as Microsoft.Office.Interop.Excel.Range;
            CR.Select();
            xlWorkSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);
            dataGridView1.RightToLeft = RightToLeft.Yes;
        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {


            // int selectedIndex = dataGridView1.CurrentCell.RowIndex;
            DilySalesReport.cc = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            // if (selectedIndex > -1)
            // {
            //dataGridView1.Rows.RemoveAt(selectedIndex);
            //dataGridView1.Refresh(); // if needed


            // }
            SalesDtlReport sdtr = new SalesDtlReport("1");
            //MAIN mn = new MAIN();
            //sdtr.MdiParent = mn;

            sdtr.ShowDialog();



        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                Microsoft.Office.Interop.Excel._Application XcelApp = new Microsoft.Office.Interop.Excel.Application();
                XcelApp.Application.Workbooks.Add(Type.Missing);

                for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
                {
                    XcelApp.Cells[1, i] = dataGridView1.Columns[i - 1].HeaderText;
                }

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        XcelApp.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                    }
                }
                XcelApp.Columns.AutoFit();
                XcelApp.Visible = true;
            }

        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            // int selectedIndex = dataGridView1.CurrentCell.RowIndex;
            DilySalesReport.cc = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            // if (selectedIndex > -1)
            // {
            //dataGridView1.Rows.RemoveAt(selectedIndex);
            //dataGridView1.Refresh(); // if needed


            // }
            SalesDtlReport sdtr = new SalesDtlReport("1");
            //MAIN mn = new MAIN();
            //sdtr.MdiParent = mn;

            sdtr.ShowDialog();
        }

        //private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (comboBox1.SelectedItem.ToString() == "محلي")
        //    {
        //        typeno = "1";
        //    }
        //    else
        //    {
        //        if (comboBox1.SelectedItem.ToString() == "سفري")
        //        {
        //            typeno = "3";
        //        }
        //        else
        //        {
        //            if(comboBox1.SelectedItem.ToString() == "على الحساب")
        //            {
        //            typeno = "4";
        //            }
        //            else      
        //             {
        //                typeno = "";
        //             }
        //        }

        //    }
        

        //}

        private void button4_Click(object sender, EventArgs e)
        {
            //CUST_SEARCH cs = new CUST_SEARCH();
            //cs.StartPosition = FormStartPosition.CenterScreen;
            //cs.ShowDialog();

            //textBox1.Text = cs.dataGridView1.CurrentRow.Cells[0].Value.ToString();
            //textBox2.Text = cs.dataGridView1.CurrentRow.Cells[1].Value.ToString();


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}