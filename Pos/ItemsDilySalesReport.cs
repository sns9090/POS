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
using Excel = Microsoft.Office.Interop.Excel;
using System.Drawing.Printing;
using GridPrintPreviewLib;
using System.Globalization;

namespace POS.Pos
{
    public partial class ItemsDilySalesReport : BaseForm
    {
        public static int cc;
        SqlConnection con3 =BL.DAML.con;// new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        public ItemsDilySalesReport()
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
            
            //////////////Control ctrl = ((Control)sender);
            //////////////switch (ctrl.BackColor.Name)
            //////////////{ 
            //////////////    case "Red":
            //////////////        ctrl.BackColor = Color.Yellow;
            //////////////        break;
            //////////////    case "Black":
            //////////////        ctrl.BackColor = Color.White;
            //////////////        break;
            //////////////    case "White":
            //////////////        ctrl.BackColor = Color.Red;
            //////////////        break;
            //////////////    case "Yellow":
            //////////////        ctrl.BackColor = Color.Purple;
            //////////////        break;
            //////////////    default:
            //////////////        ctrl.BackColor = Color.Red;
            //////////////        break;
            //////////////}
          //  string xxx;
          //  xxx = maskedTextBox1.Text.ToString();
            
          //  DateTime zzz = Convert.ToDateTime(xxx).AddHours(24);
          // // yyy = dateTimePicker2.Text + " 04:59:59 ص";
          //  SqlDataAdapter da3 = new SqlDataAdapter("select * from hdr where date between'" + xxx + "' and '" + zzz.ToString("yyyy-MM-dd HH:mm:ss") + "'", con3);
          ////  SqlDataAdapter da3 = new SqlDataAdapter("select * from hdr where date like'%" + xxx + "%'", con3);

          // // SqlDataAdapter da4 = new SqlDataAdapter("select sum(total) from hdr where date between'" + xxx + "' and '" + yyy + "'", con3);
          //  SqlDataAdapter da4 = new SqlDataAdapter("select sum(total),sum(total_cost) from hdr where date between'" + xxx + "' and '" + zzz.ToString("yyyy-MM-dd HH:mm:ss") + "'", con3);
          //  DataSet ds3 = new DataSet();
          //  da3.Fill(ds3, "0");
          //  da4.Fill(ds3, "1");
          //  dataGridView1.DataSource=ds3.Tables["0"];
          //  label1.Text = ds3.Tables[1].Rows[0][0].ToString();
          //  label3.Text = ds3.Tables[1].Rows[0][1].ToString();
            try
            {
                string xxx;
                xxx = maskedTextBox1.Text.ToString();

                DateTime zzz = Convert.ToDateTime(xxx).AddHours(24);
                SqlDataAdapter da1 = new SqlDataAdapter("select min(ref),max(ref) from pos_hdr where date between'" + xxx + "' and '" + zzz.ToString("yyyy-MM-dd HH:mm:ss",new CultureInfo("en-US", false)) + "'", con3);
                // SqlDataAdapter da2 = new SqlDataAdapter("select max(ref) from hdr where date between'" + xxx + "' and '" + zzz.ToString("yyyy-MM-dd HH:mm:ss") + "'", con3);

                DataSet ds = new DataSet();
                da1.Fill(ds, "0");
                // da2.Fill(ds, "1");
                if (ds.Tables["0"].Rows.Count > 0)
                {
                    int x, y;
                    x = Convert.ToInt32(ds.Tables["0"].Rows[0][0]);
                    y = Convert.ToInt32(ds.Tables["0"].Rows[0][1]);

                    SqlDataAdapter da3 = new SqlDataAdapter("select  a.item_no as 'رقم الصنف',a.item_name as 'اسم الصنف', isnull(sum(case when type=1 then b.qty*b.pkqty else 0 end),0)  as 'كمية البيع',isnull(sum(case when type=0 then b.qty*b.pkqty else 0 end),0) as 'كمية المرتجع', isnull(sum(case when type=1 then b.qty*b.pkqty*(b.price - (b.price*b.discpc/100)) else 0 end ),0)  as 'صافي البيع',isnull(sum(case when type=0 then b.qty*b.pkqty*(b.price - (b.price*b.discpc/100)) else 0 end ),0)  as 'صافي المرتجع' from items a,pos_dtl b where b.itemno=a.item_no and a.item_name+a.item_no like '%" + textBox1.Text + "%' and b.ref between " + x + " and " + y + " group by a.item_no,a.item_name", con3);
                  //  SqlDataAdapter da4 = new SqlDataAdapter("select  items.item_barcode as 'رقم الصنف',items.item_name as 'اسم الصنف', (select isnull(sum(dtl.qty),0) from dtl where dtl.barcode=items.item_barcode and dtl.ref between " + x + " and " + y + " and dtl.type=1) as 'كمية البيع',(select isnull(sum(dtl.qty*dtl.price),0) from dtl where dtl.barcode=items.item_barcode and dtl.ref between " + x + " and " + y + " and dtl.type=1) as 'المجموع' from items where items.item_name+items.item_barcode like '%" + textBox1.Text + "%'", con3);
                    SqlDataAdapter da4 = new SqlDataAdapter("select top 1 '   ' as 'رقم الصنف','المجموع' as 'اسم الصنف', (select isnull(sum(pos_dtl.qty*pos_dtl.pkqty),0) from pos_dtl where pos_dtl.ref between " + x + " and " + y + " and pos_dtl.type=1 and pos_dtl.name+pos_dtl.itemno like '%" + textBox1.Text + "%') as 'كمية البيع',(select isnull(sum(pos_dtl.qty*pos_dtl.pkqty),0) from pos_dtl where pos_dtl.ref between " + x + " and " + y + " and pos_dtl.type=0) as 'كمية المرتجع',(select isnull(sum(pos_dtl.qty*pos_dtl.pkqty*(pos_dtl.price - (pos_dtl.price*pos_dtl.discpc/100))),0) from pos_dtl where pos_dtl.ref between " + x + " and " + y + " and pos_dtl.type=1 and pos_dtl.name+pos_dtl.itemno like '%" + textBox1.Text + "%') as 'صافي البيع',(select isnull(sum(pos_dtl.qty*pos_dtl.pkqty*(pos_dtl.price - (pos_dtl.price*pos_dtl.discpc/100))),0) from pos_dtl where pos_dtl.ref between " + x + " and " + y + " and pos_dtl.type=0 and pos_dtl.name+pos_dtl.itemno like '%" + textBox1.Text + "%') as 'صافي المرتجع'  from items where items.item_name+items.item_no like '%" + textBox1.Text + "%' and exists(select * from pos_dtl where pos_dtl.itemno=items.item_no and pos_dtl.ref between " + x + " and " + y + ")", con3);
                   
                    
                    da3.Fill(ds, "3");
                    da4.Fill(ds, "4");
                    if (ds.Tables["3"].Rows.Count > 0)
                    {
                        ds.Tables["3"].Merge(ds.Tables["4"]);
                        dataGridView1.DataSource = ds.Tables["3"];

                        total();

                      //  hide_ifzero();
                    }
                    else
                    { 
                        MessageBox.Show("لا توجد بيانات لعرضها"); 
                    }
                }
                else
                {
                    MessageBox.Show("لا توجد بيانات لعرضها"); 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }




        }

        private void total()
        {
            double sum = 0, sum1 = 0;
            for (int i = 0; i < dataGridView1.Rows.Count-1; ++i)
               {
                   sum +=  Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value);
                   sum1 += Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value) - Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value);
                  
               }
               label1.Text = sum.ToString();
               label3.Text = sum1.ToString();

               dataGridView1.Rows[dataGridView1.Rows.Count-1].DefaultCellStyle.BackColor = Color.GreenYellow;
              

        }

        private void hide_ifzero()
        {

            foreach (DataGridViewRow dr in dataGridView1.Rows)
            {
                if (dr.Cells[2].Value.ToString() == "0")
                {
                    dr.Visible = false;
                }
            }
        }

        private void DilySalesReport_Load(object sender, EventArgs e)
        {
           // dateTimePicker1.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            // DateTime.Now.ToString("dd-MM-yyyy", new CultureInfo("en-US", false));
          //  maskedTextBox1.Text = DateTime.Now.ToString("yyyy-MM-dd") + "05:00:00";
            maskedTextBox1.Text = DateTime.Now.ToString("yyyy-MM-dd 05:00:00", new CultureInfo("en-US", false)) ;
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


        
       


        private void button2_Click(object sender, EventArgs e)
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

       //private void copyAlltoClipboard()
       //{
       //    dataGridView1.SelectAll();
       //    DataObject dataObj = dataGridView1.GetClipboardContent();
       //    if (dataObj != null)
       //        Clipboard.SetDataObject(dataObj);
       //}

       private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
       {

       }

       private void textBox1_TextChanged(object sender, EventArgs e)
       {
           button1_Click(sender, e);
       }

       private void button3_Click(object sender, EventArgs e)
       {

           //GridPrintDocument doc = new GridPrintDocument(this.dataGridView1, this.dataGridView1.Font, true);
           //doc.DocumentName = "Preview Test";
           //PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
           //printPreviewDialog.ClientSize = new Size(800, 600);
           //printPreviewDialog.Location = new System.Drawing.Point(29, 29);
           //printPreviewDialog.Name = "Print Preview Dialog";
           //printPreviewDialog.UseAntiAlias = true;
           //printPreviewDialog.Document = doc;
           //printPreviewDialog.ShowDialog();

           //float scale = doc.CalcScaleForFit();
           //doc.ScaleFactor = scale;

           //printPreviewDialog = new PrintPreviewDialog();
           //printPreviewDialog.ClientSize = new Size(800, 400);
           //printPreviewDialog.Location = new System.Drawing.Point(29, 29);
           //printPreviewDialog.Name = "PrintPreviewDialog1";
           //printPreviewDialog.UseAntiAlias = true;
           //printPreviewDialog.Document = doc;
           //printPreviewDialog.ShowDialog();

           //doc.Dispose();
           //doc = null;

           GridPrintDocument doc = new GridPrintDocument(this.dataGridView1, this.dataGridView1.Font, true);
           doc.DocumentName = "Preview Test";
           doc.DrawCellBox = true;
           PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
           printPreviewDialog.ClientSize = new Size(800, 800);
           printPreviewDialog.Location = new System.Drawing.Point(29, 29);
           printPreviewDialog.Name = "Print Preview Dialog";
           printPreviewDialog.UseAntiAlias = true;
           printPreviewDialog.Document = doc;
           printPreviewDialog.ShowDialog();
           doc.Dispose();
           doc = null;
       }

       private void button1_MouseHover(object sender, EventArgs e)
       {
         //  button1.BackColor = Color.AliceBlue;
       }

       private void button1_MouseLeave(object sender, EventArgs e)
       {
         //  button1.BackColor = Color.AntiqueWhite;
       }
    }
}
