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
using System.Globalization;
using System.Drawing.Printing;
using Microsoft.Reporting.WinForms;
//using CrystalDecisions.CrystalReports.Engine;

namespace POS.Pos
{
    
    public partial class InvPrview : BaseForm
    {
        List<BL.CurrencyInfo> currencies = new List<BL.CurrencyInfo>();
        BL.DAML daml = new BL.DAML();
        BL.Date_Validate datval = new BL.Date_Validate();
        System.Data.DataTable dtt, dthdr, dtdtl, dt222, dtunits, dtsal, dtvat, dttrtyps, dtslctr,dttax,dttbld;
        public static int cc;
        string a_slctr, a_ref, a_type, strcash, strrcash, strrcrdt, strcrdt, strdsc, strtax, strcashr, stwhno, stbrno, stsndoq, stccno,printer_nam = "";
        
        string typeno = "", xxx, zzz, temy, msg;
        SqlConnection con3 = BL.DAML.con; //new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        public InvPrview()
        {
            InitializeComponent();
            //rpttyp = iscpy;
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
              //  xxx = temy;
                CultureInfo culture = new CultureInfo("en-US");
                DateTime tempDate = Convert.ToDateTime(datval.convert_to_yyyy_MMddwith_dash(txt_mdate.Text) + " 05:00:00.000", culture).AddHours(24);
               // string.
            //  DateTime yyy = DateTime.TryParse(xxx).AddHours(24);
            //    MessageBox.Show(tempDate.ToString("dd-MM-yyyy", new CultureInfo("en-US")));
             //   MessageBox.Show(datval.convert_to_yyyy_MMddwith_dash(txt_mdate.Text) + " 05:00:00.000");
                    
                zzz = tempDate.ToString("dd-MM-yyyy", new CultureInfo("en-US"));
                xxx = txt_mdate.Text;
             //   MessageBox.Show(temy);
               ////// MessageBox.Show(datval.convert_to_yyyy_MMddwith_dash(xxx) + " 05:00:00.000");
              //////  MessageBox.Show(datval.convert_to_yyyy_MMddwith_dash(zzz) + " 05:00:00.000");
           // MessageBox.Show(zzz);
                string cndtocu = chk_ocubinv.Checked ? " r_pos_hdr " : " pos_hdr ";
                string cndtyp = comboBox2.SelectedIndex == 1 ? " and type=1 " : comboBox2.SelectedIndex == 2 ? " and type=0 " : " ";
                string conds = cmb_salctr.SelectedIndex != -1 ? " and slno='" + cmb_salctr.SelectedValue + "' " : " ";
                string condt = string.IsNullOrEmpty(txt_contr.Text)? " ": " and contr=" + txt_contr.Text + " "  ;
                string condam = string.IsNullOrEmpty(txt_salman.Text) ? " " : " and saleman=" + txt_salman.Text + " ";
              //  string condcncl = checkBox1.Checked ? " d_pos_hdr " : " pos_hdr ";

                string condeq = string.IsNullOrEmpty(txt_equl.Text) ? " " : " and net_total " + (comboBox3.SelectedIndex==-1? "=" : comboBox3.Text.Substring(0, 1)) + txt_equl.Text + " ";
            //if (typeno == "")
            //{
               
               
                // yyy = dateTimePicker2.Text + " 04:59:59 ص";
               // SqlDataAdapter da3 = new SqlDataAdapter("select [ref] ,[company] ,[type],[date] ,[total] ,[count] ,[payed],[total_cost],[saleman] ,[req_no] ,[cust_no],[discount],case [type] when 1 then [net_total] when 0 then -[net_total] end as [net_total] from pos_hdr where date between'" + xxx + "' and '" + zzz + "'", con3);
               // SqlDataAdapter da3 = new SqlDataAdapter("select top 1000 contr ,case [type] when 1 then 'بيع نقدي' when 0 then 'مرتجع نقدي' when 2 then 'مرتجع اجل' when 3 then 'بيع اجل' end as [type],ref,(CONVERT(varchar, date, 25)) [date]  ,[count],round((case type when 1 then total  when 0 then -total end),2)[total],round((case type when 1 then discount  when 0 then -discount end),2)[discount],round(((case type when 1 then net_total  when 0 then -net_total end) - (case type when 1 then tax_amt  when 0 then -tax_amt end)),2) net1,round((case type when 1 then tax_amt  when 0 then -tax_amt end),2) tax , round((case type when 1 then net_total  when 0 then -net_total end),2)[net_total] ,[payed],round((case type when 1 then total_cost  when 0 then -total_cost end),2)[total_cost],[saleman]   from " + condcncl + " where date between'" + datval.convert_to_yyyy_MMddwith_dash(xxx) + " 05:00:00.000' and '" + datval.convert_to_yyyy_MMddwith_dash(zzz) + " 05:00:00.000' " + conds + condt + condam + cndtyp + "", con3);
              ////  SqlDataAdapter da3 = new SqlDataAdapter("select top " + (chk_ocubinv.Checked ? "100" : "100") + " contr ,case [type] when 1 then 'بيع نقدي' when 0 then 'مرتجع نقدي' when 2 then 'مرتجع اجل' when 3 then 'بيع اجل' when 4 then 'ف محجوزة' end as [type],ref,(CONVERT(varchar, date, 25)) [date]  ,[count],round((case type when 1 then total  when 0 then -total end),2)[total],round((case type when 1 then discount  when 0 then -discount end),2)[discount],round(((case type when 1 then net_total  when 0 then -net_total end) - (case type when 1 then tax_amt  when 0 then -tax_amt end)),2) net1,round((case type when 1 then tax_amt  when 0 then -tax_amt end),2) tax , round((case type when 1 then net_total  when 0 then -net_total end),2)[net_total] ,[payed],round((case type when 1 then total_cost  when 0 then -total_cost end),2)[total_cost],[saleman]   from " + cndtocu + " where 1=1 " + conds + condt + condam + cndtyp + condeq + (chk_ocubinv.Checked ? " and rref=0 " : " ") +" order by date DESC ", con3);
                SqlDataAdapter da3 = new SqlDataAdapter("select contr ,case [type] when 1 then 'بيع نقدي' when 0 then 'مرتجع نقدي' when 2 then 'مرتجع اجل' when 3 then 'بيع اجل' when 4 then 'ف محجوزة' end as [type],ref,(CONVERT(varchar, date, 25)) [date]  ,[count],round((case type when 1 then total  when 0 then -total end),2)[total],round((case type when 1 then discount  when 0 then -discount end),2)[discount],round(((case type when 1 then net_total  when 0 then -net_total end) - (case type when 1 then tax_amt  when 0 then -tax_amt end)),2) net1,round((case type when 1 then tax_amt  when 0 then -tax_amt end),2) tax , round((case type when 1 then net_total  when 0 then -net_total end),2)[net_total] ,[payed],round((case type when 1 then total_cost  when 0 then -total_cost end),2)[total_cost],[saleman]   from " + cndtocu + " where 1=1 and date>'"+DateTime.Now.AddDays(-10).ToString("yyyy-MM-dd 05:00:00", new CultureInfo("en-US")) +"'"+ conds + condt + condam + cndtyp + condeq + (chk_ocubinv.Checked ? " and rref=0 " : " ") + " order by date DESC ", con3);
                
                //  SqlDataAdapter da3 = new SqlDataAdapter("select * from hdr where date like'%" + xxx + "%'", con3);

                // SqlDataAdapter da4 = new SqlDataAdapter("select sum(total) from hdr where date between'" + xxx + "' and '" + yyy + "'", con3);
                ////SqlDataAdapter da4 = new SqlDataAdapter("select top " + (chk_ocubinv.Checked ? "100" : "100") + " round(sum(case type when 1 then total  when 0 then -total end),2) total,round(sum(case type when 1 then total_cost  when 0 then -total_cost end),2) cost,round(sum(case type when 1 then discount  when 0 then -discount end),2) discnt,round(sum(case type when 1 then net_total  when 0 then -net_total end ),2) netttl,round(sum(case type when 1 then net_total-card_amt  when 0 then -net_total end),2) netcash,round(isnull(sum(case when type=1 and card_type in(1,2,3) then card_amt  else 0 end),0),2) crds,round(sum(case type when 1 then tax_amt  when 0 then -tax_amt end),2) tax,round(sum(((case type when 1 then net_total  when 0 then -net_total end) - (case type when 1 then tax_amt  when 0 then -tax_amt end))),2) net1,round(isnull(sum(case when type=1 and card_type in(4) then card_amt  else 0 end),0),2) othr from " + cndtocu + " where 1=1 " + conds + condt + condam + cndtyp + condeq + "", con3);
                SqlDataAdapter da4 = new SqlDataAdapter("select round(sum(case type when 1 then total  when 0 then -total end),2) total,round(sum(case type when 1 then total_cost  when 0 then -total_cost end),2) cost,round(sum(case type when 1 then discount  when 0 then -discount end),2) discnt,round(sum(case type when 1 then net_total  when 0 then -net_total end ),2) netttl,round(sum(case type when 1 then net_total-card_amt  when 0 then -net_total end),2) netcash,round(isnull(sum(case when type=1 and card_type in(1,2,3) then card_amt  else 0 end),0),2) crds,round(sum(case type when 1 then tax_amt  when 0 then -tax_amt end),2) tax,round(sum(((case type when 1 then net_total  when 0 then -net_total end) - (case type when 1 then tax_amt  when 0 then -tax_amt end))),2) net1,round(isnull(sum(case when type=1 and card_type in(4) then card_amt  else 0 end),0),2) othr from " + cndtocu + " where 1=1 and date>'" + DateTime.Now.AddDays(-10).ToString("yyyy-MM-dd 05:00:00", new CultureInfo("en-US"))+"'" + conds + condt + condam + cndtyp + condeq + "", con3);
                
                DataSet ds3 = new DataSet();
                da3.Fill(ds3, "0");
                da4.Fill(ds3, "1");
                dataGridView1.DataSource = ds3.Tables["0"];
                txt_total.Text = ds3.Tables[1].Rows[0][0].ToString();
                txt_cost.Text = ds3.Tables[1].Rows[0][1].ToString();

                textBox2.Text = ds3.Tables[1].Rows[0][2].ToString();
                textBox1.Text = ds3.Tables[1].Rows[0][3].ToString();

                txt_cash.Text = ds3.Tables[1].Rows[0][4].ToString();
                txt_card.Text = ds3.Tables[1].Rows[0][5].ToString();
                txt_tax.Text = ds3.Tables[1].Rows[0]["tax"].ToString();
                txt_net1.Text = ds3.Tables[1].Rows[0]["net1"].ToString();
                txt_other.Text = ds3.Tables[1].Rows[0]["othr"].ToString();

              //  dataGridView1.Columns[9].Visible = false;
              //  dataGridView1.Columns[10].Visible = false;
                dtt = ds3.Tables["0"];

               // CultureInfo culture = new CultureInfo("en-US");
              //  DateTime tempDate = Convert.ToDateTime(xxx, culture).AddHours(24);

                string st ="مبيعات يوم"+" "+tempDate.ToString("yyyy-MM-dd", new CultureInfo("en-US"));
               // string st = "ملخص مبيعات" + " " + zzz.Substring(0, 10);

                msg = st + "\n\r" + "المجموع" + " " + ds3.Tables[1].Rows[0][0].ToString() + "\n\r" + "الخصم" + " " + ds3.Tables[1].Rows[0][2].ToString() + "\n\r" + "الصافي" + " " + ds3.Tables[1].Rows[0][3].ToString();
             //////   MessageBox.Show(msg);
             //////   MessageBox.Show(msg.Length.ToString());
            //}
            //else
            //{
            //   // string xxx = maskedTextBox1.Text.ToString();
            //    //xxx = maskedTextBox1.Text.ToString();

            //   // DateTime zzz = Convert.ToDateTime(xxx).AddHours(24);
            //    // yyy = dateTimePicker2.Text + " 04:59:59 ص";
            //    SqlDataAdapter da3 = new SqlDataAdapter("select contr ,case [type] when 1 then 'نقدي' when 0 then 'مرتجع' end as [type],ref,(CONVERT(varchar, date, 25)) [date] ,[total] ,[count] ,[payed],[total_cost],[saleman] ,[req_no] ,[cust_no],[discount],[net_total] from pos_hdr where date between'" + xxx + "' and '" + zzz + "' and type = " + typeno + "", con3);
            //    //  SqlDataAdapter da3 = new SqlDataAdapter("select * from hdr where date like'%" + xxx + "%'", con3);

            //    // SqlDataAdapter da4 = new SqlDataAdapter("select sum(total) from hdr where date between'" + xxx + "' and '" + yyy + "'", con3);
            //    SqlDataAdapter da4 = new SqlDataAdapter("select sum(total),sum(total_cost),sum(discount),sum(net_total) from pos_hdr where date between'" + xxx + "' and '" + zzz + "' and type = " + typeno + "", con3);
            //    DataSet ds3 = new DataSet();
            //    da3.Fill(ds3, "0");
            //    da4.Fill(ds3, "1");
            //    dataGridView1.DataSource = ds3.Tables["0"];
            //    txt_total.Text = ds3.Tables[1].Rows[0][0].ToString();
            //    txt_cost.Text = ds3.Tables[1].Rows[0][1].ToString();

            //    textBox2.Text = ds3.Tables[1].Rows[0][2].ToString();
            //    textBox1.Text = ds3.Tables[1].Rows[0][3].ToString();

            //    dataGridView1.Columns[9].Visible = false;
            //    dataGridView1.Columns[10].Visible = false;
            //    dtt = ds3.Tables["0"];
            //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DilySalesReport_Load(object sender, EventArgs e)
        {
            currencies.Add(new BL.CurrencyInfo(BL.CurrencyInfo.Currencies.Syria));
            currencies.Add(new BL.CurrencyInfo(BL.CurrencyInfo.Currencies.UAE));
            currencies.Add(new BL.CurrencyInfo(BL.CurrencyInfo.Currencies.SaudiArabia));
            currencies.Add(new BL.CurrencyInfo(BL.CurrencyInfo.Currencies.Tunisia));
            currencies.Add(new BL.CurrencyInfo(BL.CurrencyInfo.Currencies.Gold));
            currencies.Add(new BL.CurrencyInfo(BL.CurrencyInfo.Currencies.Yemen));
            currencies.Add(new BL.CurrencyInfo(BL.CurrencyInfo.Currencies.nul));
            currencies.Add(new BL.CurrencyInfo(BL.CurrencyInfo.Currencies.Kuwait));
            currencies.Add(new BL.CurrencyInfo(BL.CurrencyInfo.Currencies.Qatar));

            dtslctr = daml.SELECT_QUIRY_only_retrn_dt("select * from taxs");

            // dateTimePicker1.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
         ///////////   maskedTextBox1.Text = DateTime.Now.ToString("yyyy-MM-dd") + "05:00:00";
          //  temy = DateTime.Now.ToString("yyyy-MM-dd 05:00:00.000", new CultureInfo("en-US"));
            txt_mdate.Text = DateTime.Now.ToString("dd-MM-yyyy", new CultureInfo("en-US"));
           // maskedTextBox1.Text = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt", new CultureInfo("en-US"));
            //string xxx;
            //xxx = maskedTextBox1.Text.ToString();
            //DateTime zzz = Convert.ToDateTime(xxx).AddHours(24);
             PrinterSettings settings = new PrinterSettings();
            printer_nam = settings.PrinterName;
            //label3.Text = zzz.ToString("yyyy-MM-dd HH:mm:ss");
            string sl = BL.CLS_Session.slkey.Replace(" ", "','").Remove(0, 2) + "'";
            
            dtslctr = daml.SELECT_QUIRY_only_retrn_dt("select * from slcenters where sl_brno + sl_no in(" + sl + ") and sl_brno='" + BL.CLS_Session.brno + "'");
            cmb_salctr.DataSource = dtslctr;
            cmb_salctr.DisplayMember = "sl_name";
            cmb_salctr.ValueMember = "sl_no";
            cmb_salctr.SelectedIndex = -1;
            txt_mdate.Select();

            button1_Click(sender, e);
        }



        private void dataGridView1_KeyUp(object sender, KeyEventArgs e)
        {
           
            //if (e.KeyCode == Keys.F10)
            //{
            //    // int selectedIndex = dataGridView1.CurrentCell.RowIndex;
            //    DilySalesReport.cc = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            //    // if (selectedIndex > -1)
            //    // {
            //    //dataGridView1.Rows.RemoveAt(selectedIndex);
            //    //dataGridView1.Refresh(); // if needed


            //    // }
            //    SalesDtlReport sdtr = new SalesDtlReport("1");
            //    //MAIN mn = new MAIN();
            //    //sdtr.MdiParent = mn;

            //    sdtr.ShowDialog();


            //}
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
            xlWorkSheet.Cells[2, 1] = "رقم الفاتورة2";
            xlWorkSheet.Cells[2, 2] = "النوع2";
            xlWorkSheet.Cells[2, 3] = "رقم الكاشير2";
            xlWorkSheet.Cells[2, 4] = "التاريخ2";
            xlWorkSheet.Cells[2, 5] = "الكمية2";
            xlWorkSheet.Cells[2, 6] = "المجموع2";
            xlWorkSheet.Cells[2, 7] = "المبلغ المدفوع2";
            xlWorkSheet.Cells[2, 8] = "التكلفة2";
            
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

            Microsoft.Office.Interop.Excel.Range CR = xlWorkSheet.Cells[3, 1] as Microsoft.Office.Interop.Excel.Range;
            CR.Select();
            xlWorkSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);
            dataGridView1.RightToLeft = RightToLeft.Yes;
        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
           


            /*

            // int selectedIndex = dataGridView1.CurrentCell.RowIndex;
            DilySalesReport.cc = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            // if (selectedIndex > -1)
            // {
            //dataGridView1.Rows.RemoveAt(selectedIndex);
            //dataGridView1.Refresh(); // if needed


            // }
            SalesDtlReport sdtr = new SalesDtlReport(checkBox1.Checked ? "0" : "1");
            //MAIN mn = new MAIN();
            //sdtr.MdiParent = mn;

            sdtr.ShowDialog();

       */

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
            try
            {
                if (dataGridView1.CurrentRow.Cells[0].Value.ToString() != "" && dataGridView1.CurrentRow.Cells[2].Value.ToString() != "")
                {
                    int toprt = Convert.ToInt32(dataGridView1.CurrentRow.Cells[2].Value.ToString());
                    int toprtc = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                    string tbld = chk_ocubinv.Checked ? "r_pos_dtl" : "pos_dtl";
                    string tblh = chk_ocubinv.Checked ? "r_pos_hdr" : "pos_hdr";
                    SqlDataAdapter dacr = new SqlDataAdapter("select [brno],[slno],[ref],[contr],[type],[barcode], [name],[unit_name] [unit],([price]-([price]*[discpc]/100)) price,[qty],[cost],[is_req],[itemno],[srno],[pkqty],[offr_amt] from " + tbld + " join units on " + tbld + ".[unit]=units.unit_id where ref=" + toprt + " and contr=" + toprtc + " order by srno", con3);
                    SqlDataAdapter dacr1 = new SqlDataAdapter("select * from " + tblh + " where ref=" + toprt + " and contr=" + toprtc + "", con3);
                    SqlDataAdapter chk = new SqlDataAdapter("select sum(isnull(is_req,0)) from " + tbld + " where ref=" + toprt + " and contr=" + toprtc + "", con3);

                    DataSet1 ds = new DataSet1();

                    ds.Tables["dtl"].Clear();
                    ds.Tables["hdr"].Clear();
                    ds.Tables["sum"].Clear();
                    
                    dacr.Fill(ds, "dtl");
                    dacr1.Fill(ds, "hdr");
                    chk.Fill(ds, "sum");

                    System.Data.DataTable dtcust = daml.SELECT_QUIRY_only_retrn_dt("select cu_no ,cu_name ,cl_acc,cu_crlmt,vndr_taxcode,cu_alwcr,c_bulding_no,  c_street, c_site_name, c_city, c_cuntry, c_postal_code, c_ex_postalcode, c_other_id,cu_kind,cu_addrs from customers a join cuclass b on a.cu_class=b.cl_no  where a.inactive=0 and a.cu_brno='" + BL.CLS_Session.brno + "' and cu_no='" + ds.Tables["hdr"].Rows[0]["cust_no"].ToString() + "'");

                    //DataSet ds = new DataSet();



                    //dacr.Fill(ds, "dtl");
                    //dacr1.Fill(ds, "hdr");
                    //chk.Fill(ds, "sum");

                    // DataTable dtchk = new DataTable();
                    //// dtchk.Clear();

                    // chk.Fill(dtchk);

                    if (BL.CLS_Session.prnt_type.Equals("1"))
                    {
                        try
                        {
                            //FastReport.Report rpt2 = new FastReport.Report();


                            //rpt2.Load(Environment.CurrentDirectory + @"\reports\Pos_Sale_rpt.frx");

                            // rpt2.SetParameterValue("br_name", BL.CLS_Session.brname);
                            FastReport.Report rpt = new FastReport.Report();

                            if (dataGridView1.CurrentRow.Cells[1].Value.ToString().Equals("بيع نقدي"))
                                rpt.Load(Environment.CurrentDirectory + @"\reports\Pos_Sale_rpt" + (numericUpDown1.Value == 1 ? "" : numericUpDown1.Value.ToString()) + ".frx");
                            else if (dataGridView1.CurrentRow.Cells[1].Value.ToString().Equals("ف محجوزة"))
                                rpt.Load(Environment.CurrentDirectory + @"\reports\Pos_Ocu_rpt.frx");
                            else
                                rpt.Load(Environment.CurrentDirectory + @"\reports\Pos_RSale_rpt" + (numericUpDown1.Value == 1 ? "" : numericUpDown1.Value.ToString()) + ".frx");
                            //  rpt.SetParameterValue("br_name", BL.CLS_Session.brname);
                            rpt.SetParameterValue("Comp_Name", BL.CLS_Session.cmp_name);
                            rpt.SetParameterValue("Br_Name", BL.CLS_Session.brname);
                            rpt.SetParameterValue("inv_bar", ds.Tables["hdr"].Rows[0][3].ToString() + "-" + ds.Tables["hdr"].Rows[0][2].ToString());
                            rpt.SetParameterValue("Msg1", BL.CLS_Session.msg1.ToString());
                            rpt.SetParameterValue("Msg2", BL.CLS_Session.msg2.ToString());
                            rpt.SetParameterValue("taxper", BL.CLS_Session.tax_per.ToString());
                           // rpt.SetParameterValue("tax_type", "3");
                            rpt.SetParameterValue("tax_type", BL.CLS_Session.autotax ? "3" : "0");
                            rpt.SetParameterValue("tax_id", BL.CLS_Session.tax_no);
                            rpt.SetParameterValue("type", ds.Tables["hdr"].Rows[0][4].ToString());

                            BL.ToWord toWord = new BL.ToWord(Convert.ToDecimal(ds.Tables["hdr"].Rows[0]["net_total"].ToString()), currencies[Convert.ToInt32(BL.CLS_Session.cur)]);

                            //  MessageBox.Show(toWord.ConvertToArabic());
                            rpt.SetParameterValue("a_toword", BL.CLS_Session.lang.Equals("E") ? toWord.ConvertToEnglish() : toWord.ConvertToArabic());

                            if (dtcust.Rows.Count > 0)
                            {
                                rpt.SetParameterValue("cust_no", dtcust.Rows[0]["cu_no"].ToString());
                                rpt.SetParameterValue("cust_taxid", dtcust.Rows[0]["vndr_taxcode"].ToString());
                            }
                            string dtt = Convert.ToDateTime(ds.Tables["hdr"].Rows[0]["date"].ToString()).ToString("yyyy-MM-ddTHH:mm:ssZ", new CultureInfo("en-US", false));
                            var tlvs = new GenerateQrCode.GenerateQrCodeTLV(
                                          BL.CLS_Session.cmp_ename,
                                           BL.CLS_Session.tax_no,
                                           dtt,
                                           ds.Tables["hdr"].Rows[0]["net_total"].ToString(),
                                          Math.Round(Convert.ToDouble(ds.Tables["hdr"].Rows[0]["tax_amt"].ToString()), 2).ToString());

                            rpt.SetParameterValue("qr", Convert.ToBase64String(tlvs.GetQRCode()));
                            //////string tlvs = new BL.qr_ar().GenerateQrCodeTLV(BL.CLS_Session.cmp_name, BL.CLS_Session.tax_no, dtt, Convert.ToDouble(ds.Tables["hdr"].Rows[0]["net_total"].ToString()), Convert.ToDouble(Math.Round(Convert.ToDouble(ds.Tables["hdr"].Rows[0]["tax_amt"].ToString()), 2).ToString()));
                            //////rpt.SetParameterValue("qr", tlvs);

                            rpt.RegisterData(ds.Tables["hdr"], "pos_hdr");
                            rpt.RegisterData(ds.Tables["dtl"], "pos_dtl");

                            FR_Viewer rptd = new FR_Viewer(rpt);

                            rptd.MdiParent = MdiParent;
                            rptd.Show();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }

                    }
                    else if (BL.CLS_Session.prnt_type.Equals("0"))
                    {


                        if (ds.Tables["sum"].Rows.Count > 0 && Convert.ToInt32(ds.Tables["sum"].Rows[0][0]) > 0)
                        {
                            // rpt.SalesReport_req report = new rpt.SalesReport_req();
                            rpt.SalesReport111 report = new rpt.SalesReport111();
                            //  CrystalReport1 report = new CrystalReport1();
                            report.SetDataSource(ds);
                            report.SetParameterValue("Comp_Name", BL.CLS_Session.cmp_name);
                            report.SetParameterValue("br_name", BL.CLS_Session.brname);
                            report.SetParameterValue("inv_bar", "*" + ds.Tables["hdr"].Rows[0][3].ToString() + "-" + ds.Tables["hdr"].Rows[0][2].ToString() + "*");
                            report.SetParameterValue("msg1", BL.CLS_Session.msg1.ToString());
                            report.SetParameterValue("msg2", BL.CLS_Session.msg2.ToString());
                            report.SetParameterValue("taxper", BL.CLS_Session.tax_per.ToString());
                            //    crystalReportViewer1.ReportSource = report;
                            RPT.FRM_RPT_PRODUCT prt = new RPT.FRM_RPT_PRODUCT();

                            prt.crystalReportViewer1.ReportSource = report;

                            prt.ShowDialog();

                            //  crystalReportViewer1.Refresh();

                            // report.PrintOptions.PrinterName = "pos";

                            // report.PrintToPrinter(1, false, 0, 0);
                            // report.PrintToPrinter(0,true, 1, 1);
                            report.Close();
                            // report.Dispose();
                        }
                        else
                        {
                            // reports.SalesReport report = new reports.SalesReport();

                            // rpt.SalesReport report = new rpt.SalesReport();
                            rpt.SalesReport111 report = new rpt.SalesReport111();

                            //  CrystalReport1 report = new CrystalReport1();
                            report.SetDataSource(ds);
                            report.SetParameterValue("Comp_Name", BL.CLS_Session.cmp_name);
                            report.SetParameterValue("br_name", BL.CLS_Session.brname);
                            report.SetParameterValue("inv_bar", "*" + ds.Tables["hdr"].Rows[0][3].ToString() + "-" + ds.Tables["hdr"].Rows[0][2].ToString() + "*");
                            report.SetParameterValue("msg1", BL.CLS_Session.msg1.ToString());
                            report.SetParameterValue("msg2", BL.CLS_Session.msg2.ToString());
                            report.SetParameterValue("taxper", BL.CLS_Session.tax_per.ToString());
                            RPT.FRM_RPT_PRODUCT prt = new RPT.FRM_RPT_PRODUCT();

                            prt.crystalReportViewer1.ReportSource = report;

                            prt.ShowDialog();
                            //    crystalReportViewer1.ReportSource = report;

                            //  crystalReportViewer1.Refresh();

                            // report.PrintOptions.PrinterName = "pos";

                            // report.PrintToPrinter(1, false, 0, 0);
                            // report.PrintToPrinter(0, true, 1, 1);
                            report.Close();
                            // report.Dispose();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("لا يوجد فاتورة","",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
            
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //// int selectedIndex = dataGridView1.CurrentCell.RowIndex;
            //DilySalesReport.cc = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            //// if (selectedIndex > -1)
            //// {
            ////dataGridView1.Rows.RemoveAt(selectedIndex);
            ////dataGridView1.Refresh(); // if needed


            //// }
            //SalesDtlReport sdtr = new SalesDtlReport("1");
            ////MAIN mn = new MAIN();
            ////sdtr.MdiParent = mn;

            //sdtr.ShowDialog();
        
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() == "محلي" || comboBox1.SelectedItem.ToString() == "Local")
            {
                typeno = "1";
            }
            else
            {
                if (comboBox1.SelectedItem.ToString() == "سفري" || comboBox1.SelectedItem.ToString() == "Out")
                {
                    typeno = "3";
                }
                else
                {
                    if (comboBox1.SelectedItem.ToString() == "على الحساب" || comboBox1.SelectedItem.ToString() == "On Acc")
                    {
                    typeno = "4";
                    }
                    else      
                     {
                        typeno = "";
                     }
                }

            }
        

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            طباعةToolStripMenuItem_Click(sender, e);
            ///*
            //Reports.Report_Form rrf = new Reports.Report_Form("DR", dtt);
            //rrf.MdiParent = ParentForm;
            //rrf.Show();
            //*/
            // if (dataGridView1.Rows.Count == 0)
            //    return;
            ///*
            //Reports.Report_Form rrf = new Reports.Report_Form("DR", dtt);
            //rrf.MdiParent = ParentForm;
            //rrf.Show();
            // */
            //try
            //{
            //    Acc.rpt.Acc_frmPrint rf = new Acc.rpt.Acc_frmPrint();


            //    // DataSet ds1 = new DataSet();
            //    System.Data.DataTable dt = new System.Data.DataTable();
            //    int cn = 1;
            //    foreach (DataGridViewColumn col in dataGridView1.Columns)
            //    {
            //        /*
            //        dt.Columns.Add(col.Name);
            //        // dt.Columns.Add(col.HeaderText);
            //         * */
            //        dt.Columns.Add("c" + cn.ToString());

            //       // MessageBox.Show("c" + cn.ToString());
            //        cn++;
            //    }

            //    foreach (DataGridViewRow row in dataGridView1.Rows)
            //    {
            //        DataRow dRow = dt.NewRow();
            //        foreach (DataGridViewCell cell in row.Cells)
            //        {
            //            dRow[cell.ColumnIndex] = cell.Value;
            //          //  MessageBox.Show(cell.Value.ToString());
            //        }
            //        dt.Rows.Add(dRow);
            //    }




            //    rf.reportViewer1.LocalReport.DataSources.Clear();
            //    rf.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));
            //    rf.reportViewer1.LocalReport.ReportEmbeddedResource = "POS.Pos.rpt.Dily_Sal_Report_rpt.rdlc";



            //    // ReportParameter[] parameters = new ReportParameter[2];
            //    // parameters[0] = new ReportParameter("comp_name", BL.CLS_Session.cmp_name);
            //    // parameters[1] = new ReportParameter("mdate", dthdr.Rows[0][3].ToString().Substring(6, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(4, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(0, 4));

            //    // rf.reportViewer1.LocalReport.SetParameters(parameters);
            //    ReportParameter[] parameters = new ReportParameter[4];
            //    parameters[0] = new ReportParameter("comp_name", BL.CLS_Session.cmp_name);
            //    //  parameters[1] = new ReportParameter("mdate", dthdr.Rows[0][3].ToString().Substring(6, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(4, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(0, 4));
            //    parameters[1] = new ReportParameter("fmdate", txt_mdate.Text);
            //    parameters[2] = new ReportParameter("tmdate", txt_mdate.Text);
            //    parameters[3] = new ReportParameter("br_name", BL.CLS_Session.brname);

            //    rf.reportViewer1.LocalReport.SetParameters(parameters);

            //    rf.reportViewer1.RefreshReport();
            //    rf.MdiParent = ParentForm;
            //    rf.Show();
            //}
            //catch { }

        }

        private void button5_Click(object sender, EventArgs e)
        {
           // SqlDataAdapter dacr = new SqlDataAdapter("select * from pos_dtl where ref=(select max(ref) from pos_dtl)", con3);
          //////  SqlDataAdapter dacr1 = new SqlDataAdapter("select * from pos_hdr where ref=(select max(ref) from pos_hdr)", con3);
           

            

            PrinterSettings settings = new PrinterSettings();
          //////  //Console.WriteLine(settings.PrinterName);
          ////// // MessageBox.Show(settings.PrinterName.ToString());

          //////  SqlDataAdapter da1 = new SqlDataAdapter("select sum(total) from pos_hdr where date between'" + xxx + "' and '" + zzz + "'", con3);
          //////  SqlDataAdapter da2 = new SqlDataAdapter("select sum(discount) from pos_hdr where date between'" + xxx + "' and '" + zzz + "'", con3);
          //////  SqlDataAdapter da3 = new SqlDataAdapter("select sum(net_total) from pos_hdr where date between'" + xxx + "' and '" + zzz + "'", con3);

          //////  // SqlDataAdapter da4 = new SqlDataAdapter("select sum(total) from pos_hdr where type=4 and date between'" + xxx + "' and '" + zzz + "'", con3);
          //////  DataSet1 ds1 = new DataSet1();
          //////  SqlDataAdapter da11 = new SqlDataAdapter("select sum(total),sum(discount),sum(net_total) from pos_hdr where date between'" + xxx + "' and '" + zzz + "'", con3);
          //////  System.Data.DataTable dt = new System.Data.DataTable();
          //////  da11.Fill(dt);
          ////// // da3.Fill(ds3, "0");
          ////// // ds1.Tables["sum"].Clear();
          ////// // ds1.Tables["sum1"].Clear();
          ////// // ds1.Tables["sum2"].Clear();
          ////// // ds1.Tables["sum3"].Clear();
          ////////  ds1.Tables["dtl"].Clear();
          //////  ds1.Tables["hdr"].Clear();
          //////  ds1.Tables["sum"].Clear();
          //////  ds1.Tables["sum1"].Clear();
          //////  ds1.Tables["sum2"].Clear();
          ////// // dacr.Fill(ds1, "dtl");

          //////  dacr1.Fill(ds1, "hdr");
          //////  da1.Fill(ds1, "sum");
          //////  da2.Fill(ds1, "sum1");
          //////  da3.Fill(ds1, "sum2");

           // da4.Fill(ds1, "sum3");

           // MessageBox.Show(ds1.Tables["sum"].Rows[0][0].ToString());
          //  MessageBox.Show(ds1.Tables["sum1"].Rows[0][0].ToString());
          //  MessageBox.Show(ds1.Tables["sum2"].Rows[0][0].ToString());
          //  MessageBox.Show(ds1.Tables["sum3"].Rows[0][0].ToString());

            rpt.ExpresSalesReport report = new rpt.ExpresSalesReport();
           // report.SetDataSource(ds1);

            report.SetParameterValue("Comp_Name", BL.CLS_Session.cmp_name.ToString());
            report.SetParameterValue("msg2", BL.CLS_Session.msg2.ToString());
            report.SetParameterValue("msg1", BL.CLS_Session.msg1.ToString());

            report.SetParameterValue("total", txt_total.Text);
            report.SetParameterValue("disc", textBox2.Text);
            report.SetParameterValue("net", textBox1.Text);

            report.SetParameterValue("net_cash", txt_cash.Text);
            report.SetParameterValue("net_card", txt_card.Text);
            report.SetParameterValue("cashir", string.IsNullOrEmpty(txt_contr.Text) ? BL.CLS_Session.contr_id : txt_contr.Text);
            report.SetParameterValue("other_amt", txt_other.Text);
            report.SetParameterValue("tax_amt", txt_tax.Text);

            report.PrintOptions.PrinterName = settings.PrinterName.ToString();//"POS";
            report.PrintToPrinter(1, false, 0, 0);
            // report.PrintToPrinter(0,true, 1, 1);
            report.Close();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            /*
          //  com.experttexting.www.   
            com.experttexting.www.ExptTextingAPI ExptTexting = new com.experttexting.www.ExptTextingAPI();

            var result = ExptTexting.SendMultilingualSMS("sns9090", "Sss735356688", "19umsuhjqsjfej3", "PosReportAD","966" + BL.CLS_Session.ownrmob.Substring(1,9) ,  msg);
            if (result.ChildNodes[0].InnerXml == "SUCCESS")
            {
                MessageBox.Show("تم الارسال بنجاح");
            }
            else
            {
                MessageBox.Show("لم يتم الارسال");
            }
             */
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (cmb_salctr.SelectedIndex == -1)
            {
                MessageBox.Show("يرجى اختيار مركز البيع قبل التكوين");
                cmb_salctr.Focus();
                return;
            }
            
            try
            {
                // int exexcuteds = daml.Insert_Update_Delete_retrn_int("INSERT INTO sales_hdr(branch,slcenter,invtype,ref,invmdate, invhdate,text,remarks,casher,entries,invttl,invdsvl,nettotal,invdspc,tax_amt_rcvd,with_tax,usrid,custno,invcst,suspend) VALUES('" + BL.CLS_Session.brno + "','" + cmb_salctr.SelectedValue + "','" + cmb_type.SelectedValue + "'," + ref_max + ",'" + mdate + "','" + hdate + "','" + txt_desc.Text + "','" + txt_remark.Text + "','" + txt_key.Text + "'," + (dataGridView1.RowCount - 1) + "," + txt_total.Text + "," + txt_des.Text + "," + txt_net.Text + "," + txt_desper.Text + "," + txt_tax.Text + "," + (chk_shaml_tax.Checked ? 1 : 0) + ",'" + txt_user.Text + "','" + txt_custno.Text + "'," + txt_cost.Text + "," + (chk_suspend.Checked ? 1 : 0) + ")", false);
                cmb_salctr_Leave(sender, e);

                //  System.Data.DataTable dtsalchk = daml.SELECT_QUIRY_only_retrn_dt("select count(*) from pos_hdr where gen_ref=0 and type=1 and date between'" + datval.convert_to_yyyy_MMddwith_dash(xxx) + " 05:00:00.000' and '" + datval.convert_to_yyyy_MMddwith_dash(zzz) + " 05:00:00.000'");
                // System.Data.DataTable dtrsalchk = daml.SELECT_QUIRY_only_retrn_dt("select count(*) from pos_hdr where gen_ref=0 and type=0 and date between'" + datval.convert_to_yyyy_MMddwith_dash(xxx) + " 05:00:00.000' and '" + datval.convert_to_yyyy_MMddwith_dash(zzz) + " 05:00:00.000'");

                //System.Data.DataTable dtsalhdr = daml.SELECT_QUIRY_only_retrn_dt("select isnull(sum(count),0) count,isnull(sum(total),0) total,isnull(sum(net_total),0) net_total,isnull(sum(discount),0) discount,isnull(sum(tax_amt),0) tax_amt,isnull(sum(gen_ref),0) gen_ref from pos_hdr where gen_ref=0 and type=1 and date between '" + datval.convert_to_yyyy_MMddwith_dash(xxx) + " 05:00:00.000' and '" + datval.convert_to_yyyy_MMddwith_dash(zzz) + " 05:00:00.000'");
                //System.Data.DataTable dtsaldtl = daml.SELECT_QUIRY_only_retrn_dt("select * from pos_dtl a where exists(select * from pos_hdr b where a.brno=b.brno and a.slno=b.slno and a.ref=b.ref and a.contr=b.contr and a.type=b.type and b.type=1 and b.gen_ref=0 and b.date between '" + datval.convert_to_yyyy_MMddwith_dash(xxx) + " 05:00:00.000' and '" + datval.convert_to_yyyy_MMddwith_dash(zzz) + " 05:00:00.000' )");
                System.Data.DataTable dtsalhdr = daml.SELECT_QUIRY_only_retrn_dt("select isnull(sum(count),0) count,isnull(sum(total - tax_amt),0) total,isnull(sum(net_total),0) net_total,isnull(sum(discount),0) discount,isnull(sum(tax_amt),0) tax_amt,isnull(sum(gen_ref),0) gen_ref,isnull(sum(taxfree_amt),0) taxfree,isnull(sum(total_cost),0) cost from pos_hdr where gen_ref=0 and type=1 and cust_no=0 and brno='" + BL.CLS_Session.brno + "' and slno='" + cmb_salctr.SelectedValue + "' and date between '" + datval.convert_to_yyyy_MMddwith_dash(xxx) + " 05:00:00.000' and '" + datval.convert_to_yyyy_MMddwith_dash(zzz) + " 05:00:00.000'");
                System.Data.DataTable dtsaldtl = daml.SELECT_QUIRY_only_retrn_dt("select a.[brno],a.[slno],a.[contr],a.[type],a.[barcode],a.[unit],a.[price],sum(a.[qty]) qty,a.[cost],a.[itemno],a.[pkqty],a.[discpc],a.[tax_id],sum(a.[tax_amt]) tax_amt from pos_dtl a where exists(select * from pos_hdr b where a.brno=b.brno and a.slno=b.slno and a.ref=b.ref and a.contr=b.contr and a.type=b.type and b.type=1 and b.cust_no=0 and b.gen_ref=0 and b.brno='" + BL.CLS_Session.brno + "' and b.slno='" + cmb_salctr.SelectedValue + "' and b.date between '" + datval.convert_to_yyyy_MMddwith_dash(xxx) + " 05:00:00.000' and '" + datval.convert_to_yyyy_MMddwith_dash(zzz) + " 05:00:00.000' ) group by a.[brno],a.[slno],a.[contr],a.[type],a.[barcode],a.[unit],a.[price],[qty],a.[cost],a.[itemno],a.[pkqty],a.[discpc],a.[tax_id],a.[tax_amt]");


                System.Data.DataTable dtrsalhdr = daml.SELECT_QUIRY_only_retrn_dt("select isnull(sum(count),0) count,isnull(sum(total  - tax_amt),0) total,isnull(sum(net_total),0) net_total,isnull(sum(discount),0) discount,isnull(sum(tax_amt),0) tax_amt,isnull(sum(gen_ref),0) gen_ref,isnull(sum(taxfree_amt),0) taxfree,isnull(sum(total_cost),0) cost from pos_hdr where gen_ref=0 and type=0 and cust_no=0 and brno='" + BL.CLS_Session.brno + "' and slno='" + cmb_salctr.SelectedValue + "' and date between '" + datval.convert_to_yyyy_MMddwith_dash(xxx) + " 05:00:00.000' and '" + datval.convert_to_yyyy_MMddwith_dash(zzz) + " 05:00:00.000'");
                System.Data.DataTable dtrsaldtl = daml.SELECT_QUIRY_only_retrn_dt("select a.[brno],a.[slno],a.[contr],a.[type],a.[barcode],a.[unit],a.[price],sum(a.[qty]) qty,a.[cost],a.[itemno],a.[pkqty],a.[discpc],a.[tax_id],sum(a.[tax_amt]) tax_amt from pos_dtl a where exists(select * from pos_hdr b where a.brno=b.brno and a.slno=b.slno and a.ref=b.ref and a.contr=b.contr and a.type=b.type and b.gen_ref=0 and b.type=0 and b.cust_no=0 and b.brno='" + BL.CLS_Session.brno + "' and b.slno='" + cmb_salctr.SelectedValue + "' and b.date between '" + datval.convert_to_yyyy_MMddwith_dash(xxx) + " 05:00:00.000' and '" + datval.convert_to_yyyy_MMddwith_dash(zzz) + " 05:00:00.000' ) group by a.[brno],a.[slno],a.[contr],a.[type],a.[barcode],a.[unit],a.[price],[qty],a.[cost],a.[itemno],a.[pkqty],a.[discpc],a.[tax_id],a.[tax_amt]");

                System.Data.DataTable dtsref = daml.SELECT_QUIRY_only_retrn_dt("select isnull(max(ref)," + BL.CLS_Session.dtbranch.Rows[0]["jv_04"] + ") from sales_hdr where invtype='04' and slcenter='" + cmb_salctr.SelectedValue + "' and branch='" + BL.CLS_Session.brno + "'");
                System.Data.DataTable dtrref = daml.SELECT_QUIRY_only_retrn_dt("select isnull(max(ref)," + BL.CLS_Session.dtbranch.Rows[0]["jv_14"] + ") from sales_hdr where invtype='14' and slcenter='" + cmb_salctr.SelectedValue + "' and branch='" + BL.CLS_Session.brno + "'");
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
              


                int ref_max = Convert.ToInt32(dtsref.Rows[0][0]) + 1;
                int ref_rmax = Convert.ToInt32(dtrref.Rows[0][0]) + 1;

               
              //  MessageBox.Show(Convert.ToDouble(dtsalhdr.Rows[0]["count"]).ToString());
              //  MessageBox.Show(Convert.ToDouble(dtrsalhdr.Rows[0]["count"]).ToString());
                daml.SqlCon_Open();

                if (Convert.ToDouble(dtsalhdr.Rows[0]["count"]) <= 0)
                {
                    MessageBox.Show("لا يوجد فواتير بيع للتكوين");
                    // return;
                }
                else
                {

                    int exexcuteds = daml.Insert_Update_Delete_retrn_int("INSERT INTO sales_hdr(branch,slcenter,invtype,ref,invmdate, invhdate,text,remarks,casher,entries,invttl,invdsvl,nettotal,invdspc,tax_amt_rcvd,with_tax,usrid,custno,invcst,suspend,glser,taxfree_amt)"
                        + " VALUES('" + stbrno + "','" + cmb_salctr.SelectedValue + "','04'," + ref_max + ",'" + datval.convert_to_yyyy_MMddwith_dash(xxx).Replace("-", "") + "','" + datval.convert_to_yyyyDDdd(datval.convert_to_hdate(xxx).Replace("-", "")) + "','مبيعات نقدية من الكاشير',' ','" + stsndoq + "'," + dtsalhdr.Rows[0]["count"] + "," + (BL.CLS_Session.isshamltax.Equals("1") ? Convert.ToDouble(dtsalhdr.Rows[0]["total"]) : Convert.ToDouble(dtsalhdr.Rows[0]["total"]) + Convert.ToDouble(dtsalhdr.Rows[0]["tax_amt"])) + "," + dtsalhdr.Rows[0]["discount"] + "," + dtsalhdr.Rows[0]["net_total"] + "," + 0 + "," + dtsalhdr.Rows[0]["tax_amt"] + "," + (BL.CLS_Session.isshamltax.Equals("1") ? 0 : 1) + ",'" + BL.CLS_Session.UserName + "',' '," + Convert.ToDouble(dtsalhdr.Rows[0]["cost"]) + "," + 0 + ",'Pos'," + Convert.ToDouble(dtsalhdr.Rows[0]["taxfree"]) + ")", false);
                    int exexcuteds2 = daml.Insert_Update_Delete_retrn_int("update pos_hdr set gen_ref= " + ref_max + " where type=1 and gen_ref=0 and date between'" + datval.convert_to_yyyy_MMddwith_dash(xxx) + " 05:00:00.000' and '" + datval.convert_to_yyyy_MMddwith_dash(zzz) + " 05:00:00.000' and brno='" + BL.CLS_Session.brno + "' and slno='" + cmb_salctr.SelectedValue + "'", false);
            
                    int folio=0;
                    foreach (DataRow row in dtsaldtl.Rows)
                    { // using (SqlCommand cmd = new SqlCommand("INSERT INTO DB.dbo.sales_dtl (ref, barcod, name, unit, price) VALUES(@r1,@c1,@c2,@c3,@c4)", con2))
                        using (SqlCommand cmd = new SqlCommand("INSERT INTO sales_dtl(branch,slcenter,invtype,ref,invmdate, invhdate, itemno, qty, price, pack, pkqty,folio,tax_amt,tax_id,discpc,whno,cost,barcode) VALUES(@a1,@a2,@a3,@a4,@a5,@a6,@a7,@a8,@a9,@a10,@a11,@a12,@a13,@a14,@a15,@a16,@a17,@a18)", con3))
                        {
                            cmd.Parameters.AddWithValue("@a1", stbrno);
                            cmd.Parameters.AddWithValue("@a2", cmb_salctr.SelectedValue);
                            cmd.Parameters.AddWithValue("@a3", "04");
                            cmd.Parameters.AddWithValue("@a4", ref_max);
                            // cmd.Parameters.AddWithValue("@a4", row.Cells[2].Value);
                            cmd.Parameters.AddWithValue("@a5", datval.convert_to_yyyy_MMddwith_dash(xxx).Replace("-", ""));
                            // cmd.Parameters.AddWithValue("@a5", (string.IsNullOrEmpty(row.Cells[3].Value.ToString())? 0 : Convert.ToDouble(row.Cells[3].Value)));
                            cmd.Parameters.AddWithValue("@a6", datval.convert_to_yyyyDDdd(datval.convert_to_hdate(xxx).Replace("-", "")));
                            cmd.Parameters.AddWithValue("@a7", row["itemno"]);
                            cmd.Parameters.AddWithValue("@a8", row["qty"]);
                            cmd.Parameters.AddWithValue("@a9", row["price"]);
                            cmd.Parameters.AddWithValue("@a10", row["unit"]);
                            cmd.Parameters.AddWithValue("@a11", row["pkqty"]);
                            cmd.Parameters.AddWithValue("@a12", folio + 1);
                           // DataRow[] dtr = dttax.Select("tex_id=" + row["tax_id"]);
                            cmd.Parameters.AddWithValue("@a13", row["tax_amt"]);
                            cmd.Parameters.AddWithValue("@a14", row["tax_id"]);
                            cmd.Parameters.AddWithValue("@a15", row["discpc"]);
                            cmd.Parameters.AddWithValue("@a16", stwhno);
                            cmd.Parameters.AddWithValue("@a17", row["cost"]);
                            cmd.Parameters.AddWithValue("@a18", row["barcode"]);
                            //  cmd.Parameters.AddWithValue("@c7", Convert.ToInt32(lblcashir.Text));
                            //if (row.Cells[7].Value != null)
                            // { cmd.Parameters.AddWithValue("@c8", row.Cells[7].Value); }
                            // cmd.Parameters.AddWithValue("@c9", comp_id);
                            // cmd.Parameters.AddWithValue("@c10", row.Cells[1].Value);
                            //con.Open();
                            if (con3.State != ConnectionState.Open)
                            {
                                con3.Open();
                            }
                            cmd.ExecuteNonQuery();
                            //con.Close();
                            if (con3.State == ConnectionState.Open)
                            {
                                con3.Close();
                            }
                            // lblref.Text = Convert.ToString(dss.Tables[0].Rows[0][0]);
                            folio = folio + 1;
                        }
                    }

                    daml.SqlCon_Open();
                    int exexcutedac = daml.Insert_Update_Delete_retrn_int("INSERT INTO acc_hdr(a_brno,a_type,a_ref,a_mdate, a_hdate,a_text,a_amt,a_entries,usrid,jhsrc,sl_no)"
                                   + " VALUES('" + stbrno + "','04'," + ref_max + ",'" + datval.convert_to_yyyy_MMddwith_dash(xxx).Replace("-", "") + "','" + datval.convert_to_yyyyDDdd(datval.convert_to_hdate(xxx).Replace("-", "")) + "',' مبيعات نقدية من الكاشير '," + (Convert.ToDouble(dtsalhdr.Rows[0]["net_total"]) + Convert.ToDouble(dtsalhdr.Rows[0]["discount"])) + "," + dtsalhdr.Rows[0]["count"] + ",'" + BL.CLS_Session.UserName + "','Pos','" + cmb_salctr.SelectedValue + "')", false);

                    daml.Insert_Update_Delete_Only("INSERT INTO acc_dtl(a_brno,a_type, a_ref, a_key, a_text, a_camt, a_damt, a_folio,a_mdate,a_hdate,jddbcr,sl_no,cc_no) VALUES('" + stbrno + "','04'," + ref_max + ",'" + strcash + "',' مبيعات نقدية من الكاشير '," + (Convert.ToDouble(dtsalhdr.Rows[0]["net_total"]) - Convert.ToDouble(dtsalhdr.Rows[0]["tax_amt"]) + Convert.ToDouble(dtsalhdr.Rows[0]["discount"])) + ",0,1,'" + datval.convert_to_yyyy_MMddwith_dash(xxx).Replace("-", "") + "','" + datval.convert_to_yyyyDDdd(datval.convert_to_hdate(xxx).Replace("-", "")) + "','C','" + cmb_salctr.SelectedValue + "','"+stccno+"')", false);
                    if (Convert.ToDouble(dtsalhdr.Rows[0]["tax_amt"]) > 0)
                    {
                        daml.Insert_Update_Delete_Only("INSERT INTO acc_dtl(a_brno,a_type, a_ref, a_key, a_text, a_camt, a_damt, a_folio,a_mdate,a_hdate,jddbcr,sl_no,taxcatId,cc_no) VALUES('" + stbrno + "','04'," + ref_max + ",'" + strtax + "','ضريبة مبيعات من الكاشير'," + Convert.ToDouble(dtsalhdr.Rows[0]["tax_amt"]) + ", 0,2,'" + datval.convert_to_yyyy_MMddwith_dash(xxx).Replace("-", "") + "','" + datval.convert_to_yyyyDDdd(datval.convert_to_hdate(xxx).Replace("-", "")) + "','C','" + cmb_salctr.SelectedValue + "',1,'" + stccno + "')", false);
                    }
                    daml.Insert_Update_Delete_Only("INSERT INTO acc_dtl(a_brno,a_type, a_ref, a_key, a_text, a_camt, a_damt, a_folio,a_mdate,a_hdate,jddbcr,cu_no,sl_no,cc_no) VALUES('" + stbrno + "','04'," + ref_max + ",'" + stsndoq + "',' مبيعات نقدية من الكاشير ',0," + Convert.ToDouble(dtsalhdr.Rows[0]["net_total"]) + ",3,'" + datval.convert_to_yyyy_MMddwith_dash(xxx).Replace("-", "") + "','" + datval.convert_to_yyyyDDdd(datval.convert_to_hdate(xxx).Replace("-", "")) + "','D','','" + cmb_salctr.SelectedValue + "','" + stccno + "')", false);

                    if (Convert.ToDouble(dtsalhdr.Rows[0]["discount"]) > 0)
                    {
                        daml.Insert_Update_Delete_Only("INSERT INTO acc_dtl(a_brno,a_type, a_ref, a_key, a_text, a_camt, a_damt, a_folio,a_mdate,a_hdate,jddbcr,sl_no,cc_no) VALUES('" + stbrno + "','04'," + ref_max + ",'" + strdsc + "','خصومات مبيعات من الكاشير'," + 0 + "," + Convert.ToDouble(dtsalhdr.Rows[0]["discount"]) + ",4,'" + datval.convert_to_yyyy_MMddwith_dash(xxx).Replace("-", "") + "','" + datval.convert_to_yyyyDDdd(datval.convert_to_hdate(xxx).Replace("-", "")) + "','D','" + cmb_salctr.SelectedValue + "','" + stccno + "')", false);
                    }
                    daml.SqlCon_Close();
                  //  daml.SqlCon_Close(); 
                    dttbld = daml.SELECT_QUIRY_only_retrn_dt("select itemno item_no,pkqty * qty item_qty from sales_dtl where invtype='04' and ref=" + ref_max + " and branch='" + BL.CLS_Session.brno + "' and slcenter='" + cmb_salctr.SelectedValue + "'");
               
                    using (SqlCommand cmd = new SqlCommand("bld_whbins_cost_all_by_tran"))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con3;
                        cmd.Parameters.AddWithValue("@items_tran_tb", dttbld);
                        con3.Open();
                        cmd.ExecuteNonQuery();
                        con3.Close();

                        //   MessageBox.Show("Items Updated : " + cmd.Parameters["@NO_items_updated"].Value, "عدد الاصناف اللتي تم تحديثها", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                    }
                    
                    MessageBox.Show("تم تكوين المبيعات");

                }

                if (Convert.ToDouble(dtrsalhdr.Rows[0]["count"]) <= 0)
                {
                    MessageBox.Show("لا يوجد فواتير مرتجع للتكوين");
                    //  return;
                }
                else
                {

                    int exexcutedr = daml.Insert_Update_Delete_retrn_int("INSERT INTO sales_hdr(branch,slcenter,invtype,ref,invmdate, invhdate,text,remarks,casher,entries,invttl,invdsvl,nettotal,invdspc,tax_amt_rcvd,with_tax,usrid,custno,invcst,suspend,glser,taxfree_amt)"
                                   + " VALUES('" + stbrno + "','" + cmb_salctr.SelectedValue + "','14'," + ref_rmax + ",'" + datval.convert_to_yyyy_MMddwith_dash(xxx).Replace("-", "") + "','" + datval.convert_to_yyyyDDdd(datval.convert_to_hdate(xxx).Replace("-", "")) + "','مرتجعات نقدية من الكاشير',' ','" + stsndoq + "'," + dtrsalhdr.Rows[0]["count"] + "," + (BL.CLS_Session.isshamltax.Equals("1") ? Convert.ToDouble(dtrsalhdr.Rows[0]["total"]) : Convert.ToDouble(dtrsalhdr.Rows[0]["total"]) + Convert.ToDouble(dtrsalhdr.Rows[0]["tax_amt"])) + "," + dtrsalhdr.Rows[0]["discount"] + "," + dtrsalhdr.Rows[0]["net_total"] + "," + 0 + "," + dtrsalhdr.Rows[0]["tax_amt"] + "," + (BL.CLS_Session.isshamltax.Equals("1") ? 0 : 1) + ",'" + BL.CLS_Session.UserName + "',' '," + Convert.ToDouble(dtsalhdr.Rows[0]["cost"]) + "," + 0 + ",'Pos'," + Convert.ToDouble(dtrsalhdr.Rows[0]["taxfree"]) + ")", false);
                    int exexcutedr2 = daml.Insert_Update_Delete_retrn_int("update pos_hdr set gen_ref= " + ref_rmax + " where type=0 and gen_ref=0 and date between'" + datval.convert_to_yyyy_MMddwith_dash(xxx) + " 05:00:00.000' and '" + datval.convert_to_yyyy_MMddwith_dash(zzz) + " 05:00:00.000' and brno='" + BL.CLS_Session.brno + "' and slno='" + cmb_salctr.SelectedValue + "'", false);

                    int folio = 0;
                    foreach (DataRow row in dtrsaldtl.Rows)
                    { // using (SqlCommand cmd = new SqlCommand("INSERT INTO DB.dbo.sales_dtl (ref, barcod, name, unit, price) VALUES(@r1,@c1,@c2,@c3,@c4)", con2))
                        using (SqlCommand cmd = new SqlCommand("INSERT INTO sales_dtl(branch,slcenter,invtype,ref,invmdate, invhdate, itemno, qty, price, pack, pkqty,folio,tax_amt,tax_id,discpc,whno,cost,barcode) VALUES(@a1,@a2,@a3,@a4,@a5,@a6,@a7,@a8,@a9,@a10,@a11,@a12,@a13,@a14,@a15,@a16,@a17,@a18)", con3))
                        {
                            cmd.Parameters.AddWithValue("@a1", stbrno);
                            cmd.Parameters.AddWithValue("@a2", cmb_salctr.SelectedValue);
                            cmd.Parameters.AddWithValue("@a3", "14");
                            cmd.Parameters.AddWithValue("@a4", ref_rmax);
                            // cmd.Parameters.AddWithValue("@a4", row.Cells[2].Value);
                            //cmd.Parameters.AddWithValue("@a5", xxx.Replace("-", ""));
                            //// cmd.Parameters.AddWithValue("@a5", (string.IsNullOrEmpty(row.Cells[3].Value.ToString())? 0 : Convert.ToDouble(row.Cells[3].Value)));
                            //cmd.Parameters.AddWithValue("@a6", datval.convert_to_hdate(xxx).Replace("-", ""));
                            cmd.Parameters.AddWithValue("@a5", datval.convert_to_yyyy_MMddwith_dash(xxx).Replace("-", ""));
                            // cmd.Parameters.AddWithValue("@a5", (string.IsNullOrEmpty(row.Cells[3].Value.ToString())? 0 : Convert.ToDouble(row.Cells[3].Value)));
                            cmd.Parameters.AddWithValue("@a6", datval.convert_to_yyyyDDdd(datval.convert_to_hdate(xxx).Replace("-", "")));
                            cmd.Parameters.AddWithValue("@a7", row["itemno"]);
                            cmd.Parameters.AddWithValue("@a8", row["qty"]);
                            cmd.Parameters.AddWithValue("@a9", row["price"]);
                            cmd.Parameters.AddWithValue("@a10", row["unit"]);
                            cmd.Parameters.AddWithValue("@a11", row["pkqty"]);
                            cmd.Parameters.AddWithValue("@a12", folio + 1);
                            // DataRow[] dtr = dttax.Select("tex_id=" + row["tax_id"]);
                            cmd.Parameters.AddWithValue("@a13", row["tax_amt"]);
                            cmd.Parameters.AddWithValue("@a14", row["tax_id"]);
                            cmd.Parameters.AddWithValue("@a15", row["discpc"]);
                            cmd.Parameters.AddWithValue("@a16", stwhno);
                            cmd.Parameters.AddWithValue("@a17", row["cost"]);
                            cmd.Parameters.AddWithValue("@a18", row["barcode"]);
                            //  cmd.Parameters.AddWithValue("@c7", Convert.ToInt32(lblcashir.Text));
                            //if (row.Cells[7].Value != null)
                            // { cmd.Parameters.AddWithValue("@c8", row.Cells[7].Value); }
                            // cmd.Parameters.AddWithValue("@c9", comp_id);
                            // cmd.Parameters.AddWithValue("@c10", row.Cells[1].Value);
                            //con.Open();
                            if (con3.State != ConnectionState.Open)
                            {
                                con3.Open();
                            }
                            cmd.ExecuteNonQuery();
                            //con.Close();
                            if (con3.State == ConnectionState.Open)
                            {
                                con3.Close();
                            }
                            // lblref.Text = Convert.ToString(dss.Tables[0].Rows[0][0]);
                            folio = folio + 1;
                        }
                    }

                    daml.SqlCon_Open();
                    int exexcutedac = daml.Insert_Update_Delete_retrn_int("INSERT INTO acc_hdr(a_brno,a_type,a_ref,a_mdate, a_hdate,a_text,a_amt,a_entries,usrid,jhsrc,sl_no)"
                                   + " VALUES('" + stbrno + "','14'," + ref_rmax + ",'" + datval.convert_to_yyyy_MMddwith_dash(xxx).Replace("-", "") + "','" + datval.convert_to_yyyyDDdd(datval.convert_to_hdate(xxx).Replace("-", "")) + "',' مرتجعات نقدية من الكاشير '," + (Convert.ToDouble(dtrsalhdr.Rows[0]["net_total"]) + Convert.ToDouble(dtrsalhdr.Rows[0]["discount"])) + "," + dtrsalhdr.Rows[0]["count"] + ",'" + BL.CLS_Session.UserName + "','Pos','" + cmb_salctr.SelectedValue + "')", false);


                    daml.Insert_Update_Delete_Only("INSERT INTO acc_dtl(a_brno,a_type, a_ref, a_key, a_text, a_damt, a_camt, a_folio,a_mdate,a_hdate,jddbcr,sl_no,cc_no) VALUES('" + stbrno + "','14'," + ref_rmax + ",'" + strcash + "',' مرتجعات نقدية من الكاشير '," + (Convert.ToDouble(dtrsalhdr.Rows[0]["net_total"]) - Convert.ToDouble(dtrsalhdr.Rows[0]["tax_amt"]) + Convert.ToDouble(dtrsalhdr.Rows[0]["discount"])) + ",0,1,'" + datval.convert_to_yyyy_MMddwith_dash(xxx).Replace("-", "") + "','" + datval.convert_to_yyyyDDdd(datval.convert_to_hdate(xxx).Replace("-", "")) + "','D','" + cmb_salctr.SelectedValue + "','" + stccno + "')", false);
                    if (Convert.ToDouble(dtrsalhdr.Rows[0]["tax_amt"]) > 0)
                    {
                        daml.Insert_Update_Delete_Only("INSERT INTO acc_dtl(a_brno,a_type, a_ref, a_key, a_text, a_damt, a_camt, a_folio,a_mdate,a_hdate,jddbcr,sl_no,taxcatId,cc_no) VALUES('" + stbrno + "','14'," + ref_rmax + ",'" + strtax + "','ضريبة مرتجعات من الكاشير'," + Convert.ToDouble(dtrsalhdr.Rows[0]["tax_amt"]) + ", 0,2,'" + datval.convert_to_yyyy_MMddwith_dash(xxx).Replace("-", "") + "','" + datval.convert_to_yyyyDDdd(datval.convert_to_hdate(xxx).Replace("-", "")) + "','D','" + cmb_salctr.SelectedValue + "',1,'" + stccno + "')", false);
                    }
                    daml.Insert_Update_Delete_Only("INSERT INTO acc_dtl(a_brno,a_type, a_ref, a_key, a_text, a_damt, a_camt, a_folio,a_mdate,a_hdate,jddbcr,cu_no,sl_no,cc_no) VALUES('" + stbrno + "','14'," + ref_rmax + ",'" + stsndoq + "',' مرتجعات نقدية من الكاشير ',0," + Convert.ToDouble(dtrsalhdr.Rows[0]["net_total"]) + ",3,'" + datval.convert_to_yyyy_MMddwith_dash(xxx).Replace("-", "") + "','" + datval.convert_to_yyyyDDdd(datval.convert_to_hdate(xxx).Replace("-", "")) + "','C','','" + cmb_salctr.SelectedValue + "','" + stccno + "')", false);

                    if (Convert.ToDouble(dtrsalhdr.Rows[0]["discount"]) > 0)
                    {
                        daml.Insert_Update_Delete_Only("INSERT INTO acc_dtl(a_brno,a_type, a_ref, a_key, a_text, a_damt, a_camt, a_folio,a_mdate,a_hdate,jddbcr,sl_no,cc_no) VALUES('" + stbrno + "','14'," + ref_rmax + ",'" + strdsc + "','خصومات مرتجعات من الكاشير'," + 0 + "," + Convert.ToDouble(dtrsalhdr.Rows[0]["discount"]) + ",4,'" + datval.convert_to_yyyy_MMddwith_dash(xxx).Replace("-", "") + "','" + datval.convert_to_yyyyDDdd(datval.convert_to_hdate(xxx).Replace("-", "")) + "','C','" + cmb_salctr.SelectedValue + "','" + stccno + "')", false);
                    }
                   // daml.SqlCon_Close();
                    daml.SqlCon_Close();

                    dttbld = daml.SELECT_QUIRY_only_retrn_dt("select itemno item_no,pkqty * qty item_qty from sales_dtl where invtype='14' and ref=" + ref_max + " and branch='" + BL.CLS_Session.brno + "' and slcenter='" + cmb_salctr.SelectedValue + "'");

                    using (SqlCommand cmd = new SqlCommand("bld_whbins_cost_all_by_tran"))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con3;
                        cmd.Parameters.AddWithValue("@items_tran_tb", dttbld);
                        con3.Open();
                        cmd.ExecuteNonQuery();
                        con3.Close();

                        //   MessageBox.Show("Items Updated : " + cmd.Parameters["@NO_items_updated"].Value, "عدد الاصناف اللتي تم تحديثها", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                    }
                    MessageBox.Show("تم تكوين المرتجعات");

                }

                ////////////////////////////////////////////////////////////////////////////////////AGL/////////////////////////////////////////////////////////////////////////////////////////
                System.Data.DataTable dt_agl = daml.SELECT_QUIRY_only_retrn_dt("SELECT [cust_no] FROM [pos_hdr] where [cust_no]<>0 and date between '" + datval.convert_to_yyyy_MMddwith_dash(xxx) + " 05:00:00.000' and '" + datval.convert_to_yyyy_MMddwith_dash(zzz) + " 05:00:00.000' group by [cust_no]");

               // MessageBox.Show(dt_agl.Rows.Count.ToString());
                foreach (DataRow dtar in dt_agl.Rows)
                {
                    System.Data.DataTable dt_cuclass = daml.SELECT_QUIRY_only_retrn_dt("select a.cu_no,b.cl_acc from customers a join cuclass b on a.cu_class=b.cl_no  where a.inactive=0 and a.cu_brno='" + BL.CLS_Session.brno + "' and cu_no='" + dtar[0].ToString() + "'");

                    System.Data.DataTable dtsalhdr_agl = daml.SELECT_QUIRY_only_retrn_dt("select isnull(sum(count),0) count,isnull(sum(total - tax_amt),0) total,isnull(sum(net_total),0) net_total,isnull(sum(discount),0) discount,isnull(sum(tax_amt),0) tax_amt,isnull(sum(gen_ref),0) gen_ref,isnull(sum(taxfree_amt),0) taxfree,isnull(sum(total_cost),0) cost from pos_hdr where gen_ref=0 and cust_no=" + dtar[0].ToString() + " and type=3 and brno='" + BL.CLS_Session.brno + "' and slno='" + cmb_salctr.SelectedValue + "' and date between '" + datval.convert_to_yyyy_MMddwith_dash(xxx) + " 05:00:00.000' and '" + datval.convert_to_yyyy_MMddwith_dash(zzz) + " 05:00:00.000'");
                    System.Data.DataTable dtsaldtl_agl = daml.SELECT_QUIRY_only_retrn_dt("select a.[brno],a.[slno],a.[contr],a.[type],a.[barcode],a.[unit],a.[price],sum(a.[qty]) qty,a.[cost],a.[itemno],a.[pkqty],a.[discpc],a.[tax_id],sum(a.[tax_amt]) tax_amt from pos_dtl a where exists(select * from pos_hdr b where a.brno=b.brno and a.slno=b.slno and a.ref=b.ref and a.contr=b.contr and a.type=b.type and b.type=3 and b.cust_no=" + dtar[0].ToString() + "  and b.gen_ref=0 and b.brno='" + BL.CLS_Session.brno + "' and b.slno='" + cmb_salctr.SelectedValue + "' and b.date between '" + datval.convert_to_yyyy_MMddwith_dash(xxx) + " 05:00:00.000' and '" + datval.convert_to_yyyy_MMddwith_dash(zzz) + " 05:00:00.000' ) group by a.[brno],a.[slno],a.[contr],a.[type],a.[barcode],a.[unit],a.[price],[qty],a.[cost],a.[itemno],a.[pkqty],a.[discpc],a.[tax_id],a.[tax_amt]");


                    System.Data.DataTable dtrsalhdr_agl = daml.SELECT_QUIRY_only_retrn_dt("select isnull(sum(count),0) count,isnull(sum(total - tax_amt),0) total,isnull(sum(net_total),0) net_total,isnull(sum(discount),0) discount,isnull(sum(tax_amt),0) tax_amt,isnull(sum(gen_ref),0) gen_ref,isnull(sum(taxfree_amt),0) taxfree,isnull(sum(total_cost),0) cost from pos_hdr where gen_ref=0 and type=2 and cust_no=" + dtar[0].ToString() + " and brno='" + BL.CLS_Session.brno + "' and slno='" + cmb_salctr.SelectedValue + "' and date between '" + datval.convert_to_yyyy_MMddwith_dash(xxx) + " 05:00:00.000' and '" + datval.convert_to_yyyy_MMddwith_dash(zzz) + " 05:00:00.000'");
                    System.Data.DataTable dtrsaldtl_agl = daml.SELECT_QUIRY_only_retrn_dt("select a.[brno],a.[slno],a.[contr],a.[type],a.[barcode],a.[unit],a.[price],sum(a.[qty]) qty,a.[cost],a.[itemno],a.[pkqty],a.[discpc],a.[tax_id],sum(a.[tax_amt]) tax_amt from pos_dtl a where exists(select * from pos_hdr b where a.brno=b.brno and a.slno=b.slno and a.ref=b.ref and a.contr=b.contr and a.type=b.type and b.gen_ref=0 and b.type=2 and b.cust_no=" + dtar[0].ToString() + " and brno='" + BL.CLS_Session.brno + "' and slno='" + cmb_salctr.SelectedValue + "' and b.date between '" + datval.convert_to_yyyy_MMddwith_dash(xxx) + " 05:00:00.000' and '" + datval.convert_to_yyyy_MMddwith_dash(zzz) + " 05:00:00.000' ) group by a.[brno],a.[slno],a.[contr],a.[type],a.[barcode],a.[unit],a.[price],[qty],a.[cost],a.[itemno],a.[pkqty],a.[discpc],a.[tax_id],a.[tax_amt]");

                    System.Data.DataTable dtsref_agl = daml.SELECT_QUIRY_only_retrn_dt("select isnull(max(ref)," + BL.CLS_Session.dtbranch.Rows[0]["jv_05"] + ") from sales_hdr where invtype='05' and slcenter='" + cmb_salctr.SelectedValue + "' and branch='" + BL.CLS_Session.brno + "'");
                    System.Data.DataTable dtrref_agl = daml.SELECT_QUIRY_only_retrn_dt("select isnull(max(ref)," + BL.CLS_Session.dtbranch.Rows[0]["jv_15"] + ") from sales_hdr where invtype='15' and slcenter='" + cmb_salctr.SelectedValue + "' and branch='" + BL.CLS_Session.brno + "'");

                    int ref_max_agl = Convert.ToInt32(dtsref_agl.Rows[0][0]) + 1;
                    int ref_rmax_agl = Convert.ToInt32(dtrref_agl.Rows[0][0]) + 1;

                    if (Convert.ToDouble(dtsalhdr_agl.Rows[0]["count"]) <= 0)
                    {
                        MessageBox.Show("لا يوجد فواتير بيع اجل للتكوين");
                        // return;
                    }
                    else
                    {

                        int exexcuteds = daml.Insert_Update_Delete_retrn_int("INSERT INTO sales_hdr(branch,slcenter,invtype,ref,invmdate, invhdate,text,remarks,casher,entries,invttl,invdsvl,nettotal,invdspc,tax_amt_rcvd,with_tax,usrid,custno,invcst,suspend,glser,taxfree_amt)"
                                       + " VALUES('" + stbrno + "','" + cmb_salctr.SelectedValue + "','05'," + ref_max_agl + ",'" + datval.convert_to_yyyy_MMddwith_dash(xxx).Replace("-", "") + "','" + datval.convert_to_yyyyDDdd(datval.convert_to_hdate(xxx).Replace("-", "")) + "','مبيعات اجله من الكاشير','" + dt_cuclass.Rows[0][1].ToString() + "','" + stsndoq + "'," + dtsalhdr_agl.Rows[0]["count"] + "," + (BL.CLS_Session.isshamltax.Equals("1") ? Convert.ToDouble(dtsalhdr_agl.Rows[0]["total"]) : Convert.ToDouble(dtsalhdr_agl.Rows[0]["total"]) + Convert.ToDouble(dtsalhdr_agl.Rows[0]["tax_amt"])) + "," + dtsalhdr_agl.Rows[0]["discount"] + "," + dtsalhdr_agl.Rows[0]["net_total"] + "," + 0 + "," + dtsalhdr_agl.Rows[0]["tax_amt"] + "," + (BL.CLS_Session.isshamltax.Equals("1") ? 0 : 1) + ",'" + BL.CLS_Session.UserName + "','" + dtar[0].ToString() + "'," + Convert.ToDouble(dtsalhdr.Rows[0]["cost"]) + "," + 0 + ",'Pos'," + Convert.ToDouble(dtsalhdr_agl.Rows[0]["taxfree"]) + ")", false);
                        int exexcuteds2 = daml.Insert_Update_Delete_retrn_int("update pos_hdr set gen_ref= " + ref_max_agl + " where type=3 and gen_ref=0 and cust_no=" + dtar[0].ToString() + " and date between'" + datval.convert_to_yyyy_MMddwith_dash(xxx) + " 05:00:00.000' and '" + datval.convert_to_yyyy_MMddwith_dash(zzz) + " 05:00:00.000' and brno='" + BL.CLS_Session.brno + "' and slno='" + cmb_salctr.SelectedValue + "'", false);

                        int folio = 0;
                        foreach (DataRow row in dtsaldtl_agl.Rows)
                        { // using (SqlCommand cmd = new SqlCommand("INSERT INTO DB.dbo.sales_dtl (ref, barcod, name, unit, price) VALUES(@r1,@c1,@c2,@c3,@c4)", con2))
                            using (SqlCommand cmd = new SqlCommand("INSERT INTO sales_dtl(branch,slcenter,invtype,ref,invmdate, invhdate, itemno, qty, price, pack, pkqty,folio,tax_amt,tax_id,discpc,whno,cost,barcode) VALUES(@a1,@a2,@a3,@a4,@a5,@a6,@a7,@a8,@a9,@a10,@a11,@a12,@a13,@a14,@a15,@a16,@a17,@a18)", con3))
                            {
                                cmd.Parameters.AddWithValue("@a1", stbrno);
                                cmd.Parameters.AddWithValue("@a2", cmb_salctr.SelectedValue);
                                cmd.Parameters.AddWithValue("@a3", "05");
                                cmd.Parameters.AddWithValue("@a4", ref_max_agl);
                                // cmd.Parameters.AddWithValue("@a4", row.Cells[2].Value);
                                cmd.Parameters.AddWithValue("@a5", datval.convert_to_yyyy_MMddwith_dash(xxx).Replace("-", ""));
                                // cmd.Parameters.AddWithValue("@a5", (string.IsNullOrEmpty(row.Cells[3].Value.ToString())? 0 : Convert.ToDouble(row.Cells[3].Value)));
                                cmd.Parameters.AddWithValue("@a6", datval.convert_to_yyyyDDdd(datval.convert_to_hdate(xxx).Replace("-", "")));
                                cmd.Parameters.AddWithValue("@a7", row["itemno"]);
                                cmd.Parameters.AddWithValue("@a8", row["qty"]);
                                cmd.Parameters.AddWithValue("@a9", row["price"]);
                                cmd.Parameters.AddWithValue("@a10", row["unit"]);
                                cmd.Parameters.AddWithValue("@a11", row["pkqty"]);
                                cmd.Parameters.AddWithValue("@a12", folio + 1);
                                // DataRow[] dtr = dttax.Select("tex_id=" + row["tax_id"]);
                                cmd.Parameters.AddWithValue("@a13", row["tax_amt"]);
                                cmd.Parameters.AddWithValue("@a14", row["tax_id"]);
                                cmd.Parameters.AddWithValue("@a15", row["discpc"]);
                                cmd.Parameters.AddWithValue("@a16", stwhno);
                                cmd.Parameters.AddWithValue("@a17", row["cost"]);
                                cmd.Parameters.AddWithValue("@a18", row["barcode"]);
                                //  cmd.Parameters.AddWithValue("@c7", Convert.ToInt32(lblcashir.Text));
                                //if (row.Cells[7].Value != null)
                                // { cmd.Parameters.AddWithValue("@c8", row.Cells[7].Value); }
                                // cmd.Parameters.AddWithValue("@c9", comp_id);
                                // cmd.Parameters.AddWithValue("@c10", row.Cells[1].Value);
                                //con.Open();
                                if (con3.State != ConnectionState.Open)
                                {
                                    con3.Open();
                                }
                                cmd.ExecuteNonQuery();
                                //con.Close();
                                if (con3.State == ConnectionState.Open)
                                {
                                    con3.Close();
                                }
                                // lblref.Text = Convert.ToString(dss.Tables[0].Rows[0][0]);
                                folio = folio + 1;
                            }
                        }

                        daml.SqlCon_Open();
                        int exexcutedac = daml.Insert_Update_Delete_retrn_int("INSERT INTO acc_hdr(a_brno,a_type,a_ref,a_mdate, a_hdate,a_text,a_amt,a_entries,usrid,jhsrc,sl_no)"
                                       + " VALUES('" + stbrno + "','05'," + ref_max_agl + ",'" + datval.convert_to_yyyy_MMddwith_dash(xxx).Replace("-", "") + "','" + datval.convert_to_yyyyDDdd(datval.convert_to_hdate(xxx).Replace("-", "")) + "',' مبيعات اجلة من الكاشير '," + (Convert.ToDouble(dtsalhdr.Rows[0]["net_total"]) + Convert.ToDouble(dtsalhdr.Rows[0]["discount"])) + "," + dtsalhdr_agl.Rows[0]["count"] + ",'" + BL.CLS_Session.UserName + "','Pos','" + cmb_salctr.SelectedValue + "')", false);

                        daml.Insert_Update_Delete_Only("INSERT INTO acc_dtl(a_brno,a_type, a_ref, a_key, a_text, a_camt, a_damt, a_folio,a_mdate,a_hdate,jddbcr,sl_no,cc_no) VALUES('" + stbrno + "','05'," + ref_max_agl + ",'" + strrcash + "',' مبيعات اجلة من الكاشير '," + (Convert.ToDouble(dtsalhdr_agl.Rows[0]["net_total"]) - Convert.ToDouble(dtsalhdr_agl.Rows[0]["tax_amt"]) + Convert.ToDouble(dtsalhdr_agl.Rows[0]["discount"])) + ",0,1,'" + datval.convert_to_yyyy_MMddwith_dash(xxx).Replace("-", "") + "','" + datval.convert_to_yyyyDDdd(datval.convert_to_hdate(xxx).Replace("-", "")) + "','C','" + cmb_salctr.SelectedValue + "','" + stccno + "')", false);
                        if (Convert.ToDouble(dtsalhdr_agl.Rows[0]["tax_amt"]) > 0)
                        {
                            daml.Insert_Update_Delete_Only("INSERT INTO acc_dtl(a_brno,a_type, a_ref, a_key, a_text, a_camt, a_damt, a_folio,a_mdate,a_hdate,jddbcr,sl_no,taxcatId,cc_no) VALUES('" + stbrno + "','05'," + ref_max_agl + ",'" + strtax + "','ضريبة مبيعات من الكاشير'," + Convert.ToDouble(dtsalhdr_agl.Rows[0]["tax_amt"]) + ", 0,2,'" + datval.convert_to_yyyy_MMddwith_dash(xxx).Replace("-", "") + "','" + datval.convert_to_yyyyDDdd(datval.convert_to_hdate(xxx).Replace("-", "")) + "','C','" + cmb_salctr.SelectedValue + "',1,'" + stccno + "')", false);
                        }
                        daml.Insert_Update_Delete_Only("INSERT INTO acc_dtl(a_brno,a_type, a_ref, a_key, a_text, a_camt, a_damt, a_folio,a_mdate,a_hdate,jddbcr,cu_no,sl_no,cc_no) VALUES('" + stbrno + "','05'," + ref_max_agl + ",'" + dt_cuclass.Rows[0][1].ToString() + "',' مبيعات اجلة من الكاشير ',0," + Convert.ToDouble(dtsalhdr_agl.Rows[0]["net_total"]) + ",3,'" + datval.convert_to_yyyy_MMddwith_dash(xxx).Replace("-", "") + "','" + datval.convert_to_yyyyDDdd(datval.convert_to_hdate(xxx).Replace("-", "")) + "','D','" + dtar[0].ToString() + "','" + cmb_salctr.SelectedValue + "','" + stccno + "')", false);

                        if (Convert.ToDouble(dtsalhdr_agl.Rows[0]["discount"]) > 0)
                        {
                            daml.Insert_Update_Delete_Only("INSERT INTO acc_dtl(a_brno,a_type, a_ref, a_key, a_text, a_camt, a_damt, a_folio,a_mdate,a_hdate,jddbcr,sl_no,cc_no) VALUES('" + stbrno + "','05'," + ref_max_agl + ",'" + strdsc + "','خصومات مبيعات من الكاشير'," + 0 + "," + Convert.ToDouble(dtsalhdr_agl.Rows[0]["discount"]) + ",4,'" + datval.convert_to_yyyy_MMddwith_dash(xxx).Replace("-", "") + "','" + datval.convert_to_yyyyDDdd(datval.convert_to_hdate(xxx).Replace("-", "")) + "','D','" + cmb_salctr.SelectedValue + "','" + stccno + "')", false);
                        }
                        daml.SqlCon_Close();
                        //  daml.SqlCon_Close(); 
                        dttbld = daml.SELECT_QUIRY_only_retrn_dt("select itemno item_no,pkqty * qty item_qty from sales_dtl where invtype='05' and ref=" + ref_max + " and branch='" + BL.CLS_Session.brno + "' and slcenter='" + cmb_salctr.SelectedValue + "'");

                        using (SqlCommand cmd = new SqlCommand("bld_whbins_cost_all_by_tran"))
                        {

                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Connection = con3;
                            cmd.Parameters.AddWithValue("@items_tran_tb", dttbld);
                            con3.Open();
                            cmd.ExecuteNonQuery();
                            con3.Close();

                            //   MessageBox.Show("Items Updated : " + cmd.Parameters["@NO_items_updated"].Value, "عدد الاصناف اللتي تم تحديثها", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                        }

                        MessageBox.Show("تم تكوين المبيعات الاجل");

                    }

                    if (Convert.ToDouble(dtrsalhdr_agl.Rows[0]["count"]) <= 0)
                    {
                        MessageBox.Show("لا يوجد فواتير مرتجع اجل للتكوين");
                        //  return;
                    }
                    else
                    {

                        int exexcutedr = daml.Insert_Update_Delete_retrn_int("INSERT INTO sales_hdr(branch,slcenter,invtype,ref,invmdate, invhdate,text,remarks,casher,entries,invttl,invdsvl,nettotal,invdspc,tax_amt_rcvd,with_tax,usrid,custno,invcst,suspend,glser,taxfree_amt)"
                                       + " VALUES('" + stbrno + "','" + cmb_salctr.SelectedValue + "','15'," + ref_rmax_agl + ",'" + datval.convert_to_yyyy_MMddwith_dash(xxx).Replace("-", "") + "','" + datval.convert_to_yyyyDDdd(datval.convert_to_hdate(xxx).Replace("-", "")) + "','مرتجعات اجلة من الكاشير','" + dt_cuclass.Rows[0][1].ToString() + "','" + stsndoq + "'," + dtrsalhdr_agl.Rows[0]["count"] + "," + (BL.CLS_Session.isshamltax.Equals("1") ? Convert.ToDouble(dtrsalhdr_agl.Rows[0]["total"]) : Convert.ToDouble(dtrsalhdr_agl.Rows[0]["total"]) + Convert.ToDouble(dtrsalhdr_agl.Rows[0]["tax_amt"])) + "," + dtrsalhdr_agl.Rows[0]["discount"] + "," + dtrsalhdr_agl.Rows[0]["net_total"] + "," + 0 + "," + dtrsalhdr_agl.Rows[0]["tax_amt"] + "," + (BL.CLS_Session.isshamltax.Equals("1") ? 0 : 1) + ",'" + BL.CLS_Session.UserName + "','" + dtar[0].ToString() + "'," + Convert.ToDouble(dtsalhdr.Rows[0]["cost"]) + "," + 0 + ",'Pos'," + Convert.ToDouble(dtrsalhdr_agl.Rows[0]["taxfree"]) + ")", false);
                        int exexcutedr2 = daml.Insert_Update_Delete_retrn_int("update pos_hdr set gen_ref= " + ref_rmax_agl + " where type=2 and gen_ref=0 and cust_no=" + dtar[0].ToString() + " and date between'" + datval.convert_to_yyyy_MMddwith_dash(xxx) + " 05:00:00.000' and '" + datval.convert_to_yyyy_MMddwith_dash(zzz) + " 05:00:00.000' and brno='" + BL.CLS_Session.brno + "' and slno='" + cmb_salctr.SelectedValue + "'", false);

                        int folio = 0;
                        foreach (DataRow row in dtrsaldtl_agl.Rows)
                        { // using (SqlCommand cmd = new SqlCommand("INSERT INTO DB.dbo.sales_dtl (ref, barcod, name, unit, price) VALUES(@r1,@c1,@c2,@c3,@c4)", con2))
                            using (SqlCommand cmd = new SqlCommand("INSERT INTO sales_dtl(branch,slcenter,invtype,ref,invmdate, invhdate, itemno, qty, price, pack, pkqty,folio,tax_amt,tax_id,discpc,whno,cost,barcode) VALUES(@a1,@a2,@a3,@a4,@a5,@a6,@a7,@a8,@a9,@a10,@a11,@a12,@a13,@a14,@a15,@a16,@a17,@a18)", con3))
                            {
                                cmd.Parameters.AddWithValue("@a1", stbrno);
                                cmd.Parameters.AddWithValue("@a2", cmb_salctr.SelectedValue);
                                cmd.Parameters.AddWithValue("@a3", "15");
                                cmd.Parameters.AddWithValue("@a4", ref_rmax_agl);
                                // cmd.Parameters.AddWithValue("@a4", row.Cells[2].Value);
                                cmd.Parameters.AddWithValue("@a5", datval.convert_to_yyyy_MMddwith_dash(xxx).Replace("-", ""));
                                // cmd.Parameters.AddWithValue("@a5", (string.IsNullOrEmpty(row.Cells[3].Value.ToString())? 0 : Convert.ToDouble(row.Cells[3].Value)));
                                cmd.Parameters.AddWithValue("@a6", datval.convert_to_yyyyDDdd(datval.convert_to_hdate(xxx).Replace("-", "")));
                                cmd.Parameters.AddWithValue("@a7", row["itemno"]);
                                cmd.Parameters.AddWithValue("@a8", row["qty"]);
                                cmd.Parameters.AddWithValue("@a9", row["price"]);
                                cmd.Parameters.AddWithValue("@a10", row["unit"]);
                                cmd.Parameters.AddWithValue("@a11", row["pkqty"]);
                                cmd.Parameters.AddWithValue("@a12", folio + 1);
                                // DataRow[] dtr = dttax.Select("tex_id=" + row["tax_id"]);
                                cmd.Parameters.AddWithValue("@a13", row["tax_amt"]);
                                cmd.Parameters.AddWithValue("@a14", row["tax_id"]);
                                cmd.Parameters.AddWithValue("@a15", row["discpc"]);
                                cmd.Parameters.AddWithValue("@a16", stwhno);
                                cmd.Parameters.AddWithValue("@a17", row["cost"]);
                                cmd.Parameters.AddWithValue("@a18", row["barcode"]);
                                //  cmd.Parameters.AddWithValue("@c7", Convert.ToInt32(lblcashir.Text));
                                //if (row.Cells[7].Value != null)
                                // { cmd.Parameters.AddWithValue("@c8", row.Cells[7].Value); }
                                // cmd.Parameters.AddWithValue("@c9", comp_id);
                                // cmd.Parameters.AddWithValue("@c10", row.Cells[1].Value);
                                //con.Open();
                                if (con3.State != ConnectionState.Open)
                                {
                                    con3.Open();
                                }
                                cmd.ExecuteNonQuery();
                                //con.Close();
                                if (con3.State == ConnectionState.Open)
                                {
                                    con3.Close();
                                }
                                // lblref.Text = Convert.ToString(dss.Tables[0].Rows[0][0]);
                                folio = folio + 1;
                            }
                        }

                        daml.SqlCon_Open();
                        int exexcutedac = daml.Insert_Update_Delete_retrn_int("INSERT INTO acc_hdr(a_brno,a_type,a_ref,a_mdate, a_hdate,a_text,a_amt,a_entries,usrid,jhsrc,sl_no)"
                                       + " VALUES('" + stbrno + "','15'," + ref_rmax_agl + ",'" + datval.convert_to_yyyy_MMddwith_dash(xxx).Replace("-", "") + "','" + datval.convert_to_yyyyDDdd(datval.convert_to_hdate(xxx).Replace("-", "")) + "',' مرتجع مبيعات اجلة من الكاشير '," + (Convert.ToDouble(dtsalhdr.Rows[0]["net_total"]) + Convert.ToDouble(dtsalhdr.Rows[0]["discount"])) + "," + dtrsalhdr_agl.Rows[0]["count"] + ",'" + BL.CLS_Session.UserName + "','Pos','" + cmb_salctr.SelectedValue + "')", false);


                        daml.Insert_Update_Delete_Only("INSERT INTO acc_dtl(a_brno,a_type, a_ref, a_key, a_text, a_damt, a_camt, a_folio,a_mdate,a_hdate,jddbcr,sl_no,cc_no) VALUES('" + stbrno + "','15'," + ref_rmax_agl + ",'" + strrcrdt + "',' مرتجع مبيعات اجلة من الكاشير '," + (Convert.ToDouble(dtrsalhdr_agl.Rows[0]["net_total"]) - Convert.ToDouble(dtrsalhdr_agl.Rows[0]["tax_amt"]) + Convert.ToDouble(dtrsalhdr_agl.Rows[0]["discount"])) + ",0,1,'" + datval.convert_to_yyyy_MMddwith_dash(xxx).Replace("-", "") + "','" + datval.convert_to_yyyyDDdd(datval.convert_to_hdate(xxx).Replace("-", "")) + "','D','" + cmb_salctr.SelectedValue + "','" + stccno + "')", false);
                        if (Convert.ToDouble(dtrsalhdr_agl.Rows[0]["tax_amt"]) > 0)
                        {
                            daml.Insert_Update_Delete_Only("INSERT INTO acc_dtl(a_brno,a_type, a_ref, a_key, a_text, a_damt, a_camt, a_folio,a_mdate,a_hdate,jddbcr,sl_no,taxcatId,cc_no) VALUES('" + stbrno + "','15'," + ref_rmax_agl + ",'" + strtax + "','ضريبة مبيعات من الكاشير'," + Convert.ToDouble(dtrsalhdr_agl.Rows[0]["tax_amt"]) + ", 0,2,'" + datval.convert_to_yyyy_MMddwith_dash(xxx).Replace("-", "") + "','" + datval.convert_to_yyyyDDdd(datval.convert_to_hdate(xxx).Replace("-", "")) + "','D','" + cmb_salctr.SelectedValue + "',1,'" + stccno + "')", false);
                        }
                        daml.Insert_Update_Delete_Only("INSERT INTO acc_dtl(a_brno,a_type, a_ref, a_key, a_text, a_damt, a_camt, a_folio,a_mdate,a_hdate,jddbcr,cu_no,sl_no,cc_no) VALUES('" + stbrno + "','15'," + ref_rmax_agl + ",'" + dt_cuclass.Rows[0][1].ToString() + "',' مرتجع مبيعات اجلة من الكاشير ',0," + Convert.ToDouble(dtrsalhdr_agl.Rows[0]["net_total"]) + ",3,'" + datval.convert_to_yyyy_MMddwith_dash(xxx).Replace("-", "") + "','" + datval.convert_to_yyyyDDdd(datval.convert_to_hdate(xxx).Replace("-", "")) + "','C','" + dtar[0].ToString() + "','" + cmb_salctr.SelectedValue + "','" + stccno + "')", false);

                        if (Convert.ToDouble(dtrsalhdr_agl.Rows[0]["discount"]) > 0)
                        {
                            daml.Insert_Update_Delete_Only("INSERT INTO acc_dtl(a_brno,a_type, a_ref, a_key, a_text, a_damt, a_camt, a_folio,a_mdate,a_hdate,jddbcr,sl_no,cc_no) VALUES('" + stbrno + "','15," + ref_rmax_agl + ",'" + strdsc + "','خصومات مبيعات من الكاشير'," + 0 + "," + Convert.ToDouble(dtrsalhdr_agl.Rows[0]["discount"]) + ",4,'" + datval.convert_to_yyyy_MMddwith_dash(xxx).Replace("-", "") + "','" + datval.convert_to_yyyyDDdd(datval.convert_to_hdate(xxx).Replace("-", "")) + "','C','" + cmb_salctr.SelectedValue + "','" + stccno + "')", false);
                        }
                        // daml.SqlCon_Close();
                        daml.SqlCon_Close();
                        dttbld = daml.SELECT_QUIRY_only_retrn_dt("select itemno item_no,pkqty * qty item_qty from sales_dtl where invtype='15' and ref=" + ref_max + " and branch='" + BL.CLS_Session.brno + "' and slcenter='" + cmb_salctr.SelectedValue + "'");

                        using (SqlCommand cmd = new SqlCommand("bld_whbins_cost_all_by_tran"))
                        {

                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Connection = con3;
                            cmd.Parameters.AddWithValue("@items_tran_tb", dttbld);
                            con3.Open();
                            cmd.ExecuteNonQuery();
                            con3.Close();

                            //   MessageBox.Show("Items Updated : " + cmd.Parameters["@NO_items_updated"].Value, "عدد الاصناف اللتي تم تحديثها", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                        }
                        MessageBox.Show("تم تكوين المرتجعات الاجل");

                    }
                }



                /*
                System.Data.DataTable dt = daml.SELECT_QUIRY_only_retrn_dt("select isnull(max(a_ref),0) from acc_hdr where a_type='01'");
                int mref = Convert.ToInt32(dt.Rows[0][0]) + 1;
                daml.SqlCon_Open();
                // exexcuted = exexcuted + daml.Insert_Update_Delete("delete from acc_dtl where a_ref=" + txt_ref.Text + " and a_type='" + comboBox1.SelectedValue + "'", false);
              //  daml.SqlCon_Close();

                System.Data.DataTable dt2 = daml.SELECT_QUIRY_only_retrn_dt("select sum(total),sum(discount),sum(net_total) from pos_hdr where gen_ref=0 and date between'" + xxx + "' and '" + zzz + "'");
                string aamt = dt2.Rows[0][2].ToString();

                int exexcuted = daml.Insert_Update_Delete_retrn_int("update pos_hdr set gen_ref= " + mref + " where gen_ref=0 and date between'" + xxx + "' and '" + zzz + "'", false);
            
               // daml.SqlCon_Open();
                int exexcuted2 = daml.Insert_Update_Delete_retrn_int("INSERT INTO acc_hdr(a_type,a_ref,a_mdate, a_hdate,a_text,a_amt,a_entries,jhsrc) VALUES('01'," + mref + ",(CONVERT([varchar],getdate(),(112))),(CONVERT([varchar],CONVERT([date],CONVERT([varchar],getdate(),(131)),(103)),(112))),'مبيعات نقدية من الكاشير'," + aamt + ",2,'SL')", false);
                int exexcuted3 = daml.Insert_Update_Delete_retrn_int("INSERT INTO acc_dtl(a_type, a_ref, a_key, a_text, a_damt, a_camt, a_folio,a_mdate,a_hdate,jddbcr) VALUES('01'," + mref + ",'010201001','مبيعات نقدية من الكاشير','" + aamt + "',0,1,(CONVERT([varchar],getdate(),(112))),(CONVERT([varchar],CONVERT([date],CONVERT([varchar],getdate(),(131)),(103)),(112))),'D')", false);
                int exexcuted4 = daml.Insert_Update_Delete_retrn_int("INSERT INTO acc_dtl(a_type, a_ref, a_key, a_text, a_camt, a_damt, a_folio,a_mdate,a_hdate,jddbcr) VALUES('01'," + mref + ",'040101001','مبيعات نقدية من الكاشير','" + aamt + "',0,2,(CONVERT([varchar],getdate(),(112))),(CONVERT([varchar],CONVERT([date],CONVERT([varchar],getdate(),(131)),(103)),(112))),'C')", false);
                */


                daml.SqlCon_Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


           // MessageBox.Show(mref.ToString());
        }

        private void maskedTextBox1_Leave(object sender, EventArgs e)
        {
            try
            {
                string datestr = txt_mdate.Text.ToString().Replace("-", "").Trim();
                if (datestr.Length == 0)
                {
                    txt_mdate.Text = DateTime.Now.ToString("dd", new CultureInfo("en-US", false)) + DateTime.Now.ToString("MM", new CultureInfo("en-US", false)) + DateTime.Now.ToString("yyyy", new CultureInfo("en-US", false));
                   // txt_hdate.Text = convert_to_hdate(txt_hdate.Text.ToString());
                }
                else
                {
                    if (datestr.Length == 2)
                    {
                        txt_mdate.Text = datestr + DateTime.Now.ToString("MM", new CultureInfo("en-US", false)) + DateTime.Now.ToString("yyyy", new CultureInfo("en-US", false));
                      //  txt_hdate.Text = convert_to_hdate(txt_mdate.Text.ToString());
                    }
                    else
                    {
                        if (datestr.Length == 4)
                        {
                            txt_mdate.Text = datestr + DateTime.Now.ToString("yyyy", new CultureInfo("en-US", false));
                          //  txt_hdate.Text = convert_to_hdate(txt_mdate.Text.ToString());
                        }
                        else
                        {
                         //   txt_hdate.Text = convert_to_hdate(txt_mdate.Text.ToString());
                        }
                    }
                }
            }
            catch { }
        }

        private void cmb_salctr_Leave(object sender, EventArgs e)
        {
            DataRow[] dr = dtslctr.Select("sl_no = '" + cmb_salctr.SelectedValue + "'");
            stbrno = dr[0][0].ToString();
            stsndoq = dr[0][3].ToString();
            strcash = dr[0][4].ToString();
            strcrdt = dr[0][5].ToString();
            strrcash = dr[0][6].ToString();
            strrcrdt = dr[0][7].ToString();
            strdsc = dr[0][8].ToString();
            strtax = dr[0][9].ToString();
            stwhno = dr[0][14].ToString();
            stccno = dr[0][22].ToString();
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void cmb_salctr_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                cmb_salctr.SelectedIndex = -1;
            }
        }

        private void txt_mdate_Enter(object sender, EventArgs e)
        {
            SendKeys.Send("{Home}");
        }

        private void txt_mdate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.Focus();
            }
        }

        private void طباعةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (Convert.ToDouble(datval.convert_to_yyyyDDdd(Convert.ToDateTime((BL.CLS_Session.minstart.Substring(4, 2) + "/" + BL.CLS_Session.minstart.Substring(6, 2) + "/" + BL.CLS_Session.minstart.Substring(0, 4)), new CultureInfo("en-US", false)).AddDays(365).ToString())) < Convert.ToDouble(DateTime.Now.ToString("yyyyMMdd", new CultureInfo("en-US", false))))
            TimeSpan span = DateTime.Now.Subtract(Convert.ToDateTime((BL.CLS_Session.minstart.Substring(4, 2) + "/" + BL.CLS_Session.minstart.Substring(6, 2) + "/" + BL.CLS_Session.minstart.Substring(0, 4)), new CultureInfo("en-US", false)));
            if (Convert.ToDouble(span.Days) > 365)
            {
                MessageBox.Show(" بيانات الصيانه مفقودة يرجى التواصل مع الدعم الفني لتحديثها ", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string tbld = chk_ocubinv.Checked ? "r_pos_dtl" : "pos_dtl";
            string tblh = chk_ocubinv.Checked ? "r_pos_hdr" : "pos_hdr";
            SqlDataAdapter dacr1 = new SqlDataAdapter("select a.[brno],a.[slno],a.[ref],a.[contr],a.[type],a.[date],a.[total],a.[count],a.[payed],a.[total_cost],b.sl_name [saleman],a.[req_no],a.[cust_no],a.[discount],a.[net_total],a.[sysdate],a.[gen_ref],a.[tax_amt],a.[card_amt],a.[note],a.[mobile] from " + tblh + " a join pos_salmen b on a.saleman=b.sl_id where a.ref=" + dataGridView1.CurrentRow.Cells[2].Value + " and a.[contr]=" + dataGridView1.CurrentRow.Cells[0].Value + " and  a.[brno]= '" + BL.CLS_Session.brno + "' ", con3);
            SqlDataAdapter dacr = new SqlDataAdapter("select [brno],[slno],[ref],[contr],[type],[barcode], [name],[unit_name] [unit],([price]-([price]*[discpc]/100)) price,[qty],[cost],[is_req],[itemno],[srno],[pkqty],[offr_amt] from " + tbld + " join units on " + tbld + ".[unit]=units.unit_id where ref=" + dataGridView1.CurrentRow.Cells[2].Value + " and [contr]=" + dataGridView1.CurrentRow.Cells[0].Value + " and  [brno]= '" + BL.CLS_Session.brno + "' order by srno ", con3);
               
            DataSet1 ds = new DataSet1();

            dacr1.Fill(ds, "hdr");
            dacr.Fill(ds, "dtl");
            System.Data.DataTable dtcust = daml.SELECT_QUIRY_only_retrn_dt("select cu_no ,cu_name ,cl_acc,cu_crlmt,vndr_taxcode,cu_alwcr,c_bulding_no,  c_street, c_site_name, c_city, c_cuntry, c_postal_code, c_ex_postalcode, c_other_id,cu_kind,cu_addrs from customers a join cuclass b on a.cu_class=b.cl_no  where a.inactive=0 and a.cu_brno='" + BL.CLS_Session.brno + "' and cu_no='" + ds.Tables["hdr"].Rows[0]["cust_no"].ToString() + "'");
             
            if (BL.CLS_Session.prnt_type.Equals("1"))
            {
                try
                {
                    //FastReport.Report rpt2 = new FastReport.Report();


                    //rpt2.Load(Environment.CurrentDirectory + @"\reports\Pos_Sale_rpt.frx");

                    // rpt2.SetParameterValue("br_name", BL.CLS_Session.brname);
                    FastReport.Report rpt = new FastReport.Report();

                    if (dataGridView1.CurrentRow.Cells[1].Value.ToString().Equals("بيع نقدي"))
                        rpt.Load(Environment.CurrentDirectory + @"\reports\Pos_Sale_rpt" + (numericUpDown1.Value == 1 ? "" : numericUpDown1.Value.ToString()) + ".frx");
                    else if (dataGridView1.CurrentRow.Cells[1].Value.ToString().Equals("ف محجوزة"))
                        rpt.Load(Environment.CurrentDirectory + @"\reports\Pos_Ocu_rpt.frx");
                    else
                        rpt.Load(Environment.CurrentDirectory + @"\reports\Pos_RSale_rpt" + (numericUpDown1.Value == 1 ? "" : numericUpDown1.Value.ToString()) + ".frx");
                    //  rpt.SetParameterValue("br_name", BL.CLS_Session.brname);
                    rpt.SetParameterValue("Comp_Name", BL.CLS_Session.cmp_name);
                    rpt.SetParameterValue("Br_Name", BL.CLS_Session.brname);
                    rpt.SetParameterValue("inv_bar", ds.Tables["hdr"].Rows[0][3].ToString() + "-" + ds.Tables["hdr"].Rows[0][2].ToString());
                    rpt.SetParameterValue("Msg1", BL.CLS_Session.msg1.ToString());
                    rpt.SetParameterValue("Msg2", BL.CLS_Session.msg2.ToString());
                    rpt.SetParameterValue("taxper", BL.CLS_Session.tax_per.ToString());
                    rpt.SetParameterValue("tax_type", BL.CLS_Session.autotax ? "3" : "0");
                    rpt.SetParameterValue("tax_id", BL.CLS_Session.tax_no);
                    rpt.SetParameterValue("type", ds.Tables["hdr"].Rows[0][4].ToString());

                    BL.ToWord toWord = new BL.ToWord(Convert.ToDecimal(ds.Tables["hdr"].Rows[0]["net_total"].ToString()), currencies[Convert.ToInt32(BL.CLS_Session.cur)]);

                    //  MessageBox.Show(toWord.ConvertToArabic());
                    rpt.SetParameterValue("a_toword", BL.CLS_Session.lang.Equals("E") ? toWord.ConvertToEnglish() : toWord.ConvertToArabic());

                    if (dtcust.Rows.Count > 0)
                    {
                        rpt.SetParameterValue("cust_no", dtcust.Rows[0]["cu_no"].ToString());
                        rpt.SetParameterValue("cust_taxid", dtcust.Rows[0]["vndr_taxcode"].ToString());
                    }
                    string dtt = Convert.ToDateTime(ds.Tables["hdr"].Rows[0]["date"].ToString()).ToString("yyyy-MM-ddTHH:mm:ssZ", new CultureInfo("en-US", false));
                    var tlvs = new GenerateQrCode.GenerateQrCodeTLV(
                                  BL.CLS_Session.cmp_ename,
                                   BL.CLS_Session.tax_no,
                                   dtt,
                                   ds.Tables["hdr"].Rows[0]["net_total"].ToString(),
                                  Math.Round(Convert.ToDouble(ds.Tables["hdr"].Rows[0]["tax_amt"].ToString()), 2).ToString());

                    rpt.SetParameterValue("qr", Convert.ToBase64String(tlvs.GetQRCode()));


                    rpt.RegisterData(ds.Tables["hdr"], "pos_hdr");
                    rpt.RegisterData(ds.Tables["dtl"], "pos_dtl");

                    //FR_Viewer rptd = new FR_Viewer(rpt);

                    //rptd.MdiParent = MdiParent;
                    //rptd.Show();
                    rpt.PrintSettings.Printer = printer_nam;// "pos";
                    rpt.PrintSettings.ShowDialog = false;
                    // FastReport.Utils.Config.ReportSettings.ShowProgress = false;
                    // rpt.PrintSettings.Copies =Convert.ToInt32(txt_count.Text);
                    rpt.Print();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                CrystalDecisions.CrystalReports.Engine.ReportDocument report1 = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                if (File.Exists(Directory.GetCurrentDirectory() + (string.IsNullOrEmpty(BL.CLS_Session.tax_no) ? @"\reports\SalesReport1nt.rpt" : BL.CLS_Session.isshamltax.Equals("1") ? @"\reports\SalesReport112.rpt" : @"\reports\SalesReport111.rpt")))
                {
                    string filePath = Directory.GetCurrentDirectory() + (string.IsNullOrEmpty(BL.CLS_Session.tax_no) ? @"\reports\SalesReport1nt.rpt" : BL.CLS_Session.isshamltax.Equals("1") ? @"\reports\SalesReport112.rpt" : @"\reports\SalesReport111.rpt");
                    report1.Load(filePath);

                    // rpt.SalesReport111 report = new rpt.SalesReport111();

                    //  CrystalReport1 report = new CrystalReport1();
                    report1.SetDataSource(ds);

                    //    crystalReportViewer1.ReportSource = report;

                    //  crystalReportViewer1.Refresh();
                    report1.SetParameterValue("Comp_Name", BL.CLS_Session.cmp_name);
                    report1.SetParameterValue("br_name", BL.CLS_Session.brname);
                    report1.SetParameterValue("inv_bar", "*" + ds.Tables["hdr"].Rows[0][3].ToString() + "-" + ds.Tables["hdr"].Rows[0][2].ToString() + "*");
                    report1.SetParameterValue("msg1", BL.CLS_Session.msg1.ToString());
                    report1.SetParameterValue("msg2", BL.CLS_Session.msg2.ToString());
                    report1.SetParameterValue("taxper", BL.CLS_Session.tax_per.ToString());
                    // report.SetParameterValue("inv_bar", );
                    //  report.SetParameterValue("picpath", Directory.GetCurrentDirectory() + "\\" + BL.CLS_Session.comp_logo);



                    report1.PrintOptions.PrinterName = printer_nam;// "pos";

                    report1.PrintToPrinter(1, false, 0, 0);
                    report1.Close();
                }
                else
                {
                    rpt.SalesReport111 report = new rpt.SalesReport111();

                    //  CrystalReport1 report = new CrystalReport1();
                    report.SetDataSource(ds);

                    //    crystalReportViewer1.ReportSource = report;

                    //  crystalReportViewer1.Refresh();
                    report.SetParameterValue("Comp_Name", BL.CLS_Session.cmp_name);
                    report.SetParameterValue("br_name", BL.CLS_Session.brname);
                    report.SetParameterValue("inv_bar", "*" + ds.Tables["hdr"].Rows[0][3].ToString() + "-" + ds.Tables["hdr"].Rows[0][2].ToString() + "*");
                    report.SetParameterValue("msg1", BL.CLS_Session.msg1.ToString());
                    report.SetParameterValue("msg2", BL.CLS_Session.msg2.ToString());
                    report.SetParameterValue("taxper", BL.CLS_Session.tax_per.ToString());
                    // report.SetParameterValue("inv_bar", );
                    //  report.SetParameterValue("picpath", Directory.GetCurrentDirectory() + "\\" + BL.CLS_Session.comp_logo);



                    report.PrintOptions.PrinterName = printer_nam;// "pos";

                    report.PrintToPrinter(1, false, 0, 0);
                    report.Close();
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            dataGridView1_DoubleClick(sender, e);
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chk_ocubinv_CheckedChanged(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }
    }
}