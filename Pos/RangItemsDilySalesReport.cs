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
using Microsoft.Reporting.WinForms;

namespace POS.Pos
{
    public partial class RangItemsDilySalesReport : BaseForm
    {
        System.Data.DataTable dtslctr, dt, dtgrp, dtunits;
        public static int cc;
        BL.DAML daml = new BL.DAML();
        SqlConnection con3 = BL.DAML.con;// new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        public RangItemsDilySalesReport()
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
            string congrp = cmb_grp.SelectedIndex != -1 ? " and items.item_group='" + cmb_grp.SelectedValue + "'" : " ";
            string congrsp = cmb_sgrp.SelectedIndex != -1 ? " and items.sgroup='" + cmb_sgrp.SelectedValue + "'" : " ";
            string conit = string.IsNullOrEmpty(txt_code.Text) ? " " : " and items.item_no='" + txt_code.Text + "' ";
            string consup = string.IsNullOrEmpty(txt_supno.Text) ? " " : " and items.supno='" + txt_supno.Text + "' ";

            System.Data.DataTable dt = daml.SELECT_QUIRY_only_retrn_dt("Select isnull(pos_dtl.itemno,' ') as itemno,rtrim(rTrim(items.item_Name)) as name, sum(pos_dtl.qty*(case when POS_HDR.type='1' then 1 else 0 end)) as salqty, sum(((qty*(price-iif(1=1,(price*discpc/100),floor(price*discpc/100))))-offr_amt)*IIF(POS_HDR.type='1',1,0)) as salval, sum(pos_dtl.qty*(case when POS_HDR.type in ('0','3') then 1 else 0 end)) as rtnqty, sum(((qty*(price-iif(1=1,(price*discpc/100),floor(price*discpc/100))))-offr_amt)*IIF(POS_HDR.type in ('0','3'),1,0)) as rtnval ,sum(pos_dtl.qty * (case when POS_HDR.type='1' then 1 else -1 end))as netqty, round(sum(((((qty*(price-iif(1=1,(price*discpc/100),floor(price*discpc/100))))-isnull(offr_amt,0))*(case when POS_HDr.type='1' then 1 else -1 end))/(iif(pos_hdr.net_total=0,1,pos_hdr.net_total)))*pos_hdr.discount),2) as dscval, round(sum(((pos_dtl.qty*(pos_dtl.price-iif(1=1,(price*discpc/100),floor(price*discpc/100))))-offr_amt)*IIF(POS_HDR.type='1',1,-1)),2) as netval , SUM(case when pos_hdr.contr='         1' then (case when POS_HDR.type='1' then pos_dtl.qty else 0.00 end) else 0.00 end) as m_val1 from pos_hdr inner Join pos_dtl LEFT OUTER JOIN items  ON pos_dtl.itemno=items.item_no on pos_hdr.brno=pos_dtl.brno and pos_hdr.Contr=pos_dtl.Contr and pos_hdr.ref=pos_dtl.ref where POS_HDr.brno='01' AND pos_hdr.type<>'4' and date  between CAST('" + maskedTextBox1.Text + "' as smalldatetime) and CAST('" + maskedTextBox2.Text + "' as smalldatetime) and pos_dtl.brno='" + BL.CLS_Session.brno + "' " + congrp + congrsp + conit + consup + " GROUP BY pos_dtl.itemno,rtrim(rTrim(items.item_Name)) order By 1");

            DataRow dr = dt.NewRow();
            double  sum2 = 0, sum3 = 0, sum4 = 0, sum5 = 0,sum6=0,sum7=0,sum8=0,sum9=0;//, sumttlm = 0;
            foreach (DataRow dtr in dt.Rows)
            {
                sum2 = sum2 + Convert.ToDouble(dtr[2]);
                sum3 = sum3 + Convert.ToDouble(dtr[3]);
                sum4 = sum4 + Convert.ToDouble(dtr[4]);
                sum5 = sum5 + Convert.ToDouble(dtr[5]);
                sum6 = sum6 + Convert.ToDouble(dtr[6]);
                sum7 = sum7 + Convert.ToDouble(dtr[7]);
                sum8 = sum8 + Convert.ToDouble(dtr[8]);
                sum9 = sum9 + Convert.ToDouble(dtr[9]);
                ////////DataRow[] res = dtunits.Select("unit_id ='" + dtr[3] + "'");
                ////////dtr[3] = res[0][1];

                // row[6] = "%" + row[6];

                // sumtrm = sumtrm + Convert.ToDouble(dtr[9]);
                // sumttld = sumttld + Convert.ToDouble(dtr[6]);
                //  sumttlm = sumttlm + Convert.ToDouble(dtr[7]);
            }
            //  dr[0] = "";
            //  dr[1] = "";
            // dr[2] = "";
            // dr[3] = "";
            //  dr[4] = "";
            dr[1] = "الاجمالي";
            dr[2] = Math.Round(sum2, 2);
            dr[3] = Math.Round(sum3, 2);
            dr[4] = Math.Round(sum4, 2);
            dr[5] = Math.Round(sum5, 2);
            dr[6] = Math.Round(sum6, 2);
            dr[7] = Math.Round(sum7, 2);
            dr[8] = Math.Round(sum8, 2);
            dr[9] = Math.Round(sum9, 2);
            //dr[10] = 0;
            // dr[9] = Math.Round(sumtrm, 2);
            // dr[10] = true;
            // dr[11] = "";

            dt.Rows.Add(dr);

            dataGridView1.DataSource = dt;

            dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.LightSteelBlue;
            //   dataGridView1.ClearSelection();
            dataGridView1.ClearSelection();
            /*
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
          //  SqlDataAdapter da3 = new SqlDataAdapter("select * from sales_hdr where date between'" + xxx + "' and '" + zzz.ToString("yyyy-MM-dd HH:mm:ss") + "'", con3);
          ////  SqlDataAdapter da3 = new SqlDataAdapter("select * from sales_hdr where date like'%" + xxx + "%'", con3);

          // // SqlDataAdapter da4 = new SqlDataAdapter("select sum(total) from sales_hdr where date between'" + xxx + "' and '" + yyy + "'", con3);
          //  SqlDataAdapter da4 = new SqlDataAdapter("select sum(total),sum(total_cost) from sales_hdr where date between'" + xxx + "' and '" + zzz.ToString("yyyy-MM-dd HH:mm:ss") + "'", con3);
          //  DataSet ds3 = new DataSet();
          //  da3.Fill(ds3, "0");
          //  da4.Fill(ds3, "1");
          //  dataGridView1.DataSource=ds3.Tables["0"];
          //  label1.Text = ds3.Tables[1].Rows[0][0].ToString();
          //  label3.Text = ds3.Tables[1].Rows[0][1].ToString();
            try
            {
                string xxx,yyy;
                xxx = maskedTextBox1.Text.ToString();
                yyy = maskedTextBox2.Text.ToString();
               // DateTime zzz1 = Convert.ToDateTime(xxx);
               // DateTime zzz2 = Convert.ToDateTime(yyy);
                SqlDataAdapter da1 = new SqlDataAdapter("select min(ref),max(ref) from pos_hdr where date between'" + xxx + "' and '" + yyy + "'", con3);
                // SqlDataAdapter da2 = new SqlDataAdapter("select max(ref) from sales_hdr where date between'" + xxx + "' and '" + zzz.ToString("yyyy-MM-dd HH:mm:ss") + "'", con3);

                DataSet ds = new DataSet();
                da1.Fill(ds, "0");
                // da2.Fill(ds, "1");
                if (ds.Tables["0"].Rows.Count > 0)
                {
                    int x, y;
                    x = Convert.ToInt32(ds.Tables["0"].Rows[0][0]);
                    y = Convert.ToInt32(ds.Tables["0"].Rows[0][1]);

                  //  SqlDataAdapter da3 = new SqlDataAdapter("select  items.item_no as 'رقم الصنف',items.item_name as 'اسم الصنف', (select isnull(sum(sales_dtl.qty),0) from sales_dtl where sales_dtl.itemno=items.item_no and sales_dtl.ref between " + x + " and " + y + " and sales_dtl.type=1) as 'كمية البيع',(select isnull(sum(sales_dtl.qty),0) from sales_dtl where sales_dtl.itemno=items.item_no and sales_dtl.ref between " + x + " and " + y + " and sales_dtl.type=0) as 'كمية المرتجع',(select isnull(sum(sales_dtl.qty*sales_dtl.price),0) from sales_dtl where sales_dtl.itemno=items.item_no and sales_dtl.ref between " + x + " and " + y + " and sales_dtl.type=1) as 'صافي البيع' from items where items.item_name+items.item_no like '%" + textBox1.Text + "%' and exists(select * from sales_dtl where sales_dtl.itemno=items.item_no and sales_dtl.ref between " + x + " and " + y + ")", con3);
                  ////  SqlDataAdapter da4 = new SqlDataAdapter("select  items.item_no as 'رقم الصنف',items.item_name as 'اسم الصنف', (select isnull(sum(sales_dtl.qty),0) from sales_dtl where sales_dtl.itemno=items.item_no and sales_dtl.ref between " + x + " and " + y + " and sales_dtl.type=1) as 'كمية البيع',(select isnull(sum(sales_dtl.qty*sales_dtl.price),0) from sales_dtl where sales_dtl.itemno=items.item_no and sales_dtl.ref between " + x + " and " + y + " and sales_dtl.type=1) as 'المجموع' from items where items.item_name+items.item_no like '%" + textBox1.Text + "%'", con3);
                  //  SqlDataAdapter da4 = new SqlDataAdapter("select top 1 '   ' as 'رقم الصنف','المجموع' as 'اسم الصنف', (select isnull(sum(sales_dtl.qty),0) from sales_dtl where sales_dtl.ref between " + x + " and " + y + " and sales_dtl.type=1 and sales_dtl.name+sales_dtl.itemno like '%" + textBox1.Text + "%') as 'كمية البيع',(select isnull(sum(sales_dtl.qty),0) from sales_dtl where sales_dtl.ref between " + x + " and " + y + " and sales_dtl.type=0) as 'كمية المرتجع',(select isnull(sum(sales_dtl.qty*sales_dtl.price),0) from sales_dtl where sales_dtl.ref between " + x + " and " + y + " and sales_dtl.type=1 and sales_dtl.name+sales_dtl.itemno like '%" + textBox1.Text + "%') as 'صافي البيع' from items where items.item_name+items.item_no like '%" + textBox1.Text + "%' and exists(select * from sales_dtl where sales_dtl.itemno=items.item_no and sales_dtl.ref between " + x + " and " + y + ")", con3);
                    //  SqlDataAdapter da3 = new SqlDataAdapter("select  items.item_no as 'رقم الصنف',items.item_name as 'اسم الصنف', (select isnull(sum(pos_dtl.qty*pos_dtl.pkqty),0) from pos_dtl where pos_dtl.itemno=items.item_no and pos_dtl.ref between " + x + " and " + y + " and pos_dtl.type=1) as 'كمية البيع',(select isnull(sum(pos_dtl.qty*pos_dtl.pkqty),0) from pos_dtl where pos_dtl.itemno=items.item_no and pos_dtl.ref between " + x + " and " + y + " and pos_dtl.type=0) as 'كمية المرتجع',(select isnull(sum(pos_dtl.qty*pos_dtl.pkqty*pos_dtl.price),0) from pos_dtl where pos_dtl.itemno=items.item_no and pos_dtl.ref between " + x + " and " + y + " and pos_dtl.type=1) as 'صافي البيع' from items where items.item_name+items.item_no like '%" + textBox1.Text + "%' and exists(select * from pos_dtl where pos_dtl.itemno=items.item_no and pos_dtl.ref between " + x + " and " + y + ")", con3);
                    ////  SqlDataAdapter da4 = new SqlDataAdapter("select  items.item_barcode as 'رقم الصنف',items.item_name as 'اسم الصنف', (select isnull(sum(dtl.qty),0) from dtl where dtl.barcode=items.item_barcode and dtl.ref between " + x + " and " + y + " and dtl.type=1) as 'كمية البيع',(select isnull(sum(dtl.qty*dtl.price),0) from dtl where dtl.barcode=items.item_barcode and dtl.ref between " + x + " and " + y + " and dtl.type=1) as 'المجموع' from items where items.item_name+items.item_barcode like '%" + textBox1.Text + "%'", con3);
                    //  SqlDataAdapter da4 = new SqlDataAdapter("select top 1 '   ' as 'رقم الصنف','المجموع' as 'اسم الصنف', (select isnull(sum(pos_dtl.qty*pos_dtl.pkqty),0) from pos_dtl where pos_dtl.ref between " + x + " and " + y + " and pos_dtl.type=1 and pos_dtl.name+pos_dtl.itemno like '%" + textBox1.Text + "%') as 'كمية البيع',(select isnull(sum(pos_dtl.qty*pos_dtl.pkqty),0) from pos_dtl where pos_dtl.ref between " + x + " and " + y + " and pos_dtl.type=0) as 'كمية المرتجع',(select isnull(sum(pos_dtl.qty*pos_dtl.pkqty*pos_dtl.price),0) from pos_dtl where pos_dtl.ref between " + x + " and " + y + " and pos_dtl.type=1 and pos_dtl.name+pos_dtl.itemno like '%" + textBox1.Text + "%') as 'صافي البيع' from items where items.item_name+items.item_no like '%" + textBox1.Text + "%' and exists(select * from pos_dtl where pos_dtl.itemno=items.item_no and pos_dtl.ref between " + x + " and " + y + ")", con3);
                //ok   SqlDataAdapter da3 = new SqlDataAdapter("select  items.item_no as 'رقم الصنف',items.item_name as 'اسم الصنف', (select isnull(sum(pos_dtl.qty*pos_dtl.pkqty),0) from pos_dtl where pos_dtl.itemno=items.item_no and pos_dtl.ref between " + x + " and " + y + " and pos_dtl.type=1) as 'كمية البيع',(select isnull(sum(pos_dtl.qty*pos_dtl.pkqty),0) from pos_dtl where pos_dtl.itemno=items.item_no and pos_dtl.ref between " + x + " and " + y + " and pos_dtl.type=0) as 'كمية المرتجع',(select isnull(sum(pos_dtl.qty*pos_dtl.pkqty*pos_dtl.price),0) from pos_dtl where pos_dtl.itemno=items.item_no and pos_dtl.ref between " + x + " and " + y + " and pos_dtl.type=1) as 'صافي البيع' from items where items.item_name+items.item_no like '%" + textBox1.Text + "%' and exists(select * from pos_dtl where pos_dtl.itemno=items.item_no and pos_dtl.ref between " + x + " and " + y + ")", con3);
                    //  SqlDataAdapter da4 = new SqlDataAdapter("select  items.item_barcode as 'رقم الصنف',items.item_name as 'اسم الصنف', (select isnull(sum(dtl.qty),0) from dtl where dtl.barcode=items.item_barcode and dtl.ref between " + x + " and " + y + " and dtl.type=1) as 'كمية البيع',(select isnull(sum(dtl.qty*dtl.price),0) from dtl where dtl.barcode=items.item_barcode and dtl.ref between " + x + " and " + y + " and dtl.type=1) as 'المجموع' from items where items.item_name+items.item_barcode like '%" + textBox1.Text + "%'", con3);
                //ok    SqlDataAdapter da4 = new SqlDataAdapter("select top 1 '   ' as 'رقم الصنف','المجموع' as 'اسم الصنف', (select isnull(sum(pos_dtl.qty*pos_dtl.pkqty),0) from pos_dtl where pos_dtl.ref between " + x + " and " + y + " and pos_dtl.type=1 and pos_dtl.name+pos_dtl.itemno like '%" + textBox1.Text + "%') as 'كمية البيع',(select isnull(sum(pos_dtl.qty*pos_dtl.pkqty),0) from pos_dtl where pos_dtl.ref between " + x + " and " + y + " and pos_dtl.type=0) as 'كمية المرتجع',(select isnull(sum(pos_dtl.qty*pos_dtl.pkqty*pos_dtl.price),0) from pos_dtl where pos_dtl.ref between " + x + " and " + y + " and pos_dtl.type=1 and pos_dtl.name+pos_dtl.itemno like '%" + textBox1.Text + "%') as 'صافي البيع' from items where items.item_name+items.item_no like '%" + textBox1.Text + "%' and exists(select * from pos_dtl where pos_dtl.itemno=items.item_no and pos_dtl.ref between " + x + " and " + y + ")", con3);
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
                MessageBox.Show(ex.ToString());
            }


            */

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
            maskedTextBox1.Text = DateTime.Now.ToString("yyyy-MM-01 05:00:00", new CultureInfo("en-US", false));
            DateTime dt = new DateTime();
            dt = DateTime.Now.AddDays(1);
           // maskedTextBox2.Text = DateTime.Now.ToString("yyyy-MM-dd") + "05:00:00";
            maskedTextBox2.Text = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd 05:00:00", new CultureInfo("en-US", false));

           

            dtgrp = daml.SELECT_QUIRY_only_retrn_dt("select group_id,group_name from groups where group_pid is null");
            // DataTable dt = new DataTable();
            // da.Fill(dt);


            cmb_grp.DataSource = dtgrp;
            // metroComboBox3.DataSource = dt2;
            //comboBox1.DataSource = dataSet1.Tables["Suppliers"];
            // metroComboBox3.DisplayMember = "a";
            //  comboBox1.ValueMember = comboBox1.Text;
            cmb_grp.DisplayMember = "group_name";
            cmb_grp.ValueMember = "group_id";
            cmb_grp.SelectedIndex = -1;
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
                Pos.DilySalesReport.cc = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
               // if (selectedIndex > -1)
               // {
                    //dataGridView1.Rows.RemoveAt(selectedIndex);
                    //dataGridView1.Refresh(); // if needed


               // }
                Pos.SalesDtlReport sdtr = new Pos.SalesDtlReport("1");
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
            if (dataGridView1.Rows.Count == 0)
                return;
            try
            {
                // Fill_Data(cmb_salctr.SelectedValue.ToString(), cmb_type.SelectedValue.ToString(), txt_ref.Text);

                Acc.rpt.Acc_frmPrint rf = new Acc.rpt.Acc_frmPrint();
                //rf.reportViewer1.RefreshReport();
                //SqlConnection con = new SqlConnection(@"Data Source=RYD1-PC\INFOSOFT12;" +
                //                                    "Initial Catalog=DBASE;" +
                //                                    "User id=sa;" +
                //                                    "Password=infocic;");

                //SqlDataAdapter da1 = new SqlDataAdapter("select * from accounts where a_key='010201001'", con);
                //SqlDataAdapter da2 = new SqlDataAdapter("select * from acc_hdr where a_ref=7", con);
                //SqlDataAdapter da3 = new SqlDataAdapter("SELECT acc_dtl.a_key, accounts.a_name, acc_dtl.a_text, acc_dtl.a_camt, acc_dtl.a_damt"
                //                                       + " FROM acc_dtl INNER JOIN"
                //                                       + " accounts ON acc_dtl.a_key = accounts.a_key"
                //                                       + " where acc_dtl.a_key='010201001'", con);

                //  DataSet ds1 = new DataSet();

                // ds1.Tables["accounts"]=dthdr;
                // ds1.Tables["accounts"] = dthdr;
                //  da3.Fill(ds1, "details");

                // this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds1));

                //ReportParameter[] parameters = new ReportParameter[1];
                //parameters[0] = new ReportParameter("comp_name", "hi");

                //rf.reportViewer1.LocalReport.SetParameters(parameters);

                // rf.reportViewer1.LocalReport.SetParameters("comp_name", BL.CLS_Session.cmp_name);

                // rf.reportViewer1.LocalReport.Refresh();
                /*
                 DataTable dttt = new DataTable();
                 foreach (DataGridViewColumn col in dataGridView1.Columns)
                 {
                     dttt.Columns.Add(col.Name);
                     // dt.Columns.Add(col.HeaderText);
                 }

                 foreach (DataGridViewRow row in dataGridView1.Rows)
                 {
                     DataRow dRow = dttt.NewRow();
                     foreach (DataGridViewCell cell in row.Cells)
                     {
                         dRow[cell.ColumnIndex] = cell.Value;
                     }
                     dttt.Rows.Add(dRow);
                 }
                */



                System.Data.DataTable dt2 = new System.Data.DataTable();
                int cn = 1;
                foreach (DataGridViewColumn col in dataGridView1.Columns)
                {
                    /*
                    dt.Columns.Add(col.Name);
                    // dt.Columns.Add(col.HeaderText);
                     * */
                    dt2.Columns.Add("c" + cn.ToString());
                    cn++;
                }

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    DataRow dRow = dt2.NewRow();
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        dRow[cell.ColumnIndex] = cell.Value;
                    }
                    dt2.Rows.Add(dRow);
                }


                System.Data.DataTable dt = new System.Data.DataTable();
                dt = dt2.Copy();

                //foreach (DataRow row in dt.Rows)
                //{/*
                //    DataView dv1 = dtunits.DefaultView;
                //    dv1.RowFilter = "unit_id ='" + row[2] +"'";
                //    DataTable dtNew = dv1.ToTable();
                //    //dcombo.DataSource = dtNew;
                //    row[2] = dtNew.Rows[0][1];
                //  * */
                //    DataRow[] res = dtunits.Select("unit_id ='" + row[2] + "'");
                //    row[2] = res[0][1];
                //    // row[6] = "%" + row[6];
                //}
            
                rf.reportViewer1.LocalReport.DataSources.Clear();
                // rf.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("acc_hdr", dthdr));
                rf.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("sdt", dt));

                rf.reportViewer1.LocalReport.ReportEmbeddedResource = "POS.Pos.rpt.Pos_Items_rpt.rdlc";

                //ReportParameter rp = new ReportParameter("comp_name", BL.CLS_Session.cmp_name);
                //rf.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp });

                ReportParameter[] parameters = new ReportParameter[3];
                parameters[0] = new ReportParameter("comp_name", BL.CLS_Session.cmp_name);
                //  parameters[1] = new ReportParameter("mdate", dthdr.Rows[0][3].ToString().Substring(6, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(4, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(0, 4));
                //  parameters[2] = new ReportParameter("aType", dthdr.Rows[0][1].ToString());
                parameters[1] = new ReportParameter("br_name", BL.CLS_Session.brname);
                parameters[2] = new ReportParameter("title", "ملخص مبيعات الاصناف");
                //parameters[2] = new ReportParameter("type", cmb_type.Text);
                //parameters[3] = new ReportParameter("ref", txt_ref.Text);
                //parameters[4] = new ReportParameter("date", txt_mdate.Text);
                //parameters[5] = new ReportParameter("fwh_name", cmb_frwh.Text);

                //parameters[6] = new ReportParameter("desc", txt_desc.Text);
                //parameters[7] = new ReportParameter("twh_name", cmb_towh.Text);
                //parameters[8] = new ReportParameter("total", txt_total.Text);
                //parameters[9] = new ReportParameter("descount", txt_des.Text);
                //parameters[10] = new ReportParameter("tax", "0");
                //parameters[11] = new ReportParameter("nettotal", txt_total.Text);

                //BL.ToWord toWord = new BL.ToWord(Convert.ToDecimal(txt_total.Text), currencies[Convert.ToInt32(BL.CLS_Session.cur)]);

                ////  MessageBox.Show(toWord.ConvertToArabic());
                //parameters[12] = new ReportParameter("a_toword", toWord.ConvertToArabic());

                rf.reportViewer1.LocalReport.SetParameters(parameters);

                rf.reportViewer1.RefreshReport();
                rf.MdiParent = ParentForm;
                rf.Show();
            }
            catch { }
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

           /************************okkkkkkkkkkkkkkkkk
           GridPrintDocument doc = new GridPrintDocument(this.dataGridView1, this.dataGridView1.Font, true);
           doc.DocumentName = "Preview Test";
           doc.DrawCellBox = true;
           PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
           printPreviewDialog.ClientSize = new Size(800, 600);
           printPreviewDialog.Location = new System.Drawing.Point(29, 29);
           printPreviewDialog.Name = "Print Preview Dialog";
           printPreviewDialog.UseAntiAlias = true;
           printPreviewDialog.Document = doc;
           printPreviewDialog.ShowDialog();
           doc.Dispose();
           doc = null;
            * */
       }

       private void button1_MouseHover(object sender, EventArgs e)
       {
         //  button1.BackColor = Color.AliceBlue;
       }

       private void button1_MouseLeave(object sender, EventArgs e)
       {
         //  button1.BackColor = Color.AntiqueWhite;
       }

       private void cmb_grp_SelectedIndexChanged(object sender, EventArgs e)
       {
           cmb_sgrp_Enter(sender, e);
           cmb_sgrp.SelectedIndex = -1;
       }

       private void cmb_sgrp_Enter(object sender, EventArgs e)
       {
           try
           {
               cmb_sgrp.DataSource = daml.SELECT_QUIRY_only_retrn_dt("select group_id,group_name from groups where group_pid='" + cmb_grp.SelectedValue + "' and group_pid is not null");
               // metroComboBox3.DataSource = dt2;
               //comboBox1.DataSource = dataSet1.Tables["Suppliers"];
               // metroComboBox3.DisplayMember = "a";
               //  comboBox1.ValueMember = comboBox1.Text;
               cmb_sgrp.DisplayMember = "group_name";
               cmb_sgrp.ValueMember = "group_id";

           }
           catch { }
       }

       private void txt_supno_Leave(object sender, EventArgs e)
       {
           System.Data.DataTable dt2 = daml.SELECT_QUIRY_only_retrn_dt("select su_no ,su_name  from suppliers where su_brno='" + BL.CLS_Session.brno + "' and su_no='" + txt_supno.Text + "'");


           // dataGridView1.DataSource = daml.SELECT_QUIRY(qstr2);

           if (dt2.Rows.Count > 0)
           {
               txt_supname.Text = dt2.Rows[0][1].ToString();
               // txt_temp.Text = dt2.Rows[0][2].ToString();
               // txt_crlmt.Text = dt2.Rows[0][3].ToString();
           }
           else
           {
               txt_supname.Text = "";
               txt_supno.Text = "";
               // txt_crlmt.Text = "";
           }
       }

       private void txt_supno_KeyDown(object sender, KeyEventArgs e)
       {
           if (e.KeyCode == Keys.F8)
           {
               // txt_name.Text = "";
               try
               {
                   Search_All f4 = new Search_All("7", "Sup");
                   f4.ShowDialog();
                   txt_supno.Text = f4.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                   txt_supname.Text = f4.dataGridView1.CurrentRow.Cells[1].Value.ToString();
               }
               catch { }

           }
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

                   // button1_Click( sender,  e);
                   System.Data.DataTable dta = daml.SELECT_QUIRY_only_retrn_dt("select item_name,round(item_obalance,2) a_opnbal,item_no  from items where item_no='" + txt_code.Text + "'");
                   if (dta.Rows.Count > 0)
                   {
                       txt_name.Text = dta.Rows[0][0].ToString();
                       txt_code.Text = dta.Rows[0][2].ToString();
                       //  txt_opnbal.Text = dta.Rows[0][1].ToString();
                   }
                   else
                   {
                       //    MessageBox.Show("الحساب غير موجود");
                       txt_name.Text = "";
                       txt_code.Text = "";
                       //  txt_code.Text = dt.Rows[0][2].ToString();
                       //  txt_opnbal.Text = "0";
                   }

               }
           }
           catch { }
       }
    }
}
