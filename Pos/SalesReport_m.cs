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
using System.Globalization;
using Microsoft.Reporting.WinForms;
using System.Drawing.Printing;
//using CrystalDecisions.CrystalReports.Engine;

namespace POS.Pos
{
    public partial class SalesReport_m : BaseForm
    {
        BL.DAML daml = new BL.DAML();
        BL.Date_Validate datval = new BL.Date_Validate();
        DataTable dtt, dtslctr;
        public static int qq;
        string typeno = "",printer_nam = "";
        SqlConnection con3 = BL.DAML.con; //new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        public SalesReport_m()
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
                //if (typeno == "")
                //{
               // string cndtyp = comboBox2.SelectedIndex == 1 ? " and type=1 " : comboBox2.SelectedIndex == 2 ? " and type=0 " : " ";
                string conds = cmb_salctr.SelectedIndex != -1 ? " and slno='" + cmb_salctr.SelectedValue + "' " : " ";
               // string condt = string.IsNullOrEmpty(txt_contr.Text) ? " " : " and contr=" + txt_contr.Text + " ";
               // string condam = string.IsNullOrEmpty(txt_salman.Text) ? " " : " and saleman=" + txt_salman.Text + " ";
                string conddr = string.IsNullOrEmpty(txt_driver.Text) ? " " : " and b.driver_id=" + txt_driver.Text + " ";
                string condcst = string.IsNullOrEmpty(txt_custno.Text) ? " " : " and a.cust_no=" + txt_custno.Text + " ";
                string condcncl = checkBox1.Checked ? " d_pos_hdr " : " pos_hdr ";
                string xxx;
                xxx = dateTimePicker1.Text.ToString();// maskedTextBox1.Text.ToString();

                string zzz;
                zzz = dateTimePicker2.Text.ToString();// maskedTextBox2.Text.ToString();
                //SqlDataAdapter da3 = new SqlDataAdapter("select a_type a,a_ref b,a_mdate c,a_hdate,a_text d,a_amt e  where date between '20180101' and '20190101'", con3);
                //SqlDataAdapter da4 = new SqlDataAdapter("select sum(total),sum(total_cost),sum(discount),sum(net_total) from pos_hdr where date between'" + xxx + "' and '" + zzz + "'", con3);
                //  SqlDataAdapter da3 = new SqlDataAdapter("select contr ,case [type] when 1 then 'نقدي' when 0 then 'مرتجع' end as [type],ref,(CONVERT(varchar, date, 25)) [date] ,[total] ,[count] ,[payed],[total_cost],[saleman] ,[req_no] ,[cust_no],[discount],[net_total] from "+condcncl+" where date between'" + xxx + "' and '" + zzz + "' " + conds + condt + condam + "", con3);
                //  SqlDataAdapter da3 = new SqlDataAdapter("select contr ,case [type] when 1 then 'بيع نقدي' when 0 then 'مرتجع نقدي' when 2 then 'مرتجع اجل' when 3 then 'بيع اجل' end as [type],ref,(CONVERT(varchar(10), date, 25)) [date],[count] ,round((case type when 1 then total  when 0 then -total end),2)[total],round((case type when 1 then discount  when 0 then -discount end),2)[discount],round(((case type when 1 then net_total  when 0 then -net_total end) - (case type when 1 then tax_amt  when 0 then -tax_amt end)),2) net1,round((case type when 1 then tax_amt  when 0 then -tax_amt end),2) tax,round((case type when 1 then net_total  when 0 then -net_total end),2)[net_total],payed,round((case type when 1 then total_cost  when 0 then -total_cost end),2)[total_cost],[saleman] from " + condcncl + " where  brno='" + BL.CLS_Session.brno + "' and date between'" + datval.convert_to_yyyy_MMddwith_dash(xxx) + " 05:00:00.000' and '" + datval.convert_to_yyyy_MMddwith_dash(zzz) + " 05:00:00.000' " + conds + condt + condam + cndtyp + "", con3);
                SqlDataAdapter da3 = new SqlDataAdapter(@"select a.contr ,a.ref,FORMAT ([date], 'dd-MM-yyyy hh:mm:ss tt') [date],[count] ,round( net_total,2)[net_total],c.cu_name,b.driver_nam,[saleman],b.dpayed,a.[type] at,b.driver_id moslno

                                                              FROM pos_hdr  a  join pos_hdr_r b on  a.[brno]=b.[dbrno] and a.[slno]=b.[dslno] and a.[ref]=b.[dref] and a.[contr] = b.[dcontr] 
                                                             left join customers c on a.[brno]=c.cu_brno and a.cust_no=c.cu_no where  a.brno='" + BL.CLS_Session.brno + "' and a.date between'" + datval.convert_to_yyyy_MMddwith_dash(xxx) + " " + maskedTextBox3.Text + "' and '" + datval.convert_to_yyyy_MMddwith_dash(zzz) + " " + maskedTextBox4.Text + "' " + conds + conddr + condcst + "", con3);

                //  SqlDataAdapter da3 = new SqlDataAdapter("select * from hdr where date like'%" + xxx + "%'", con3);
                // SqlDataAdapter da4 = new SqlDataAdapter("select sum(total) from hdr where date between'" + xxx + "' and '" + yyy + "'", con3);
               // SqlDataAdapter da4 = new SqlDataAdapter("select round(sum(case type when 5 then net_total  else 0 end),2),round(sum(case type when 1 then total_cost  when 0 then -total_cost end),2),round(sum(case type when 1 then discount  when 0 then -discount end),2),round(sum(case type when 1 then net_total  when 0 then -net_total end ),2),round(sum(case type when 1 then tax_amt  when 0 then -tax_amt end),2) tax ,round(sum(((case type when 1 then net_total  when 0 then -net_total end) - (case type when 1 then tax_amt  when 0 then -tax_amt end))),2) net1,round(sum(case type when 1 then net_total-card_amt  when 0 then -net_total end),2)cash ,round(isnull(sum(case when type=1 and card_type in(1,2,3) then card_amt  else 0 end),0),2) card,round(isnull(sum(case when type=1 and card_type in(4) then card_amt  else 0 end),0),2) othr from " + condcncl + " where  brno='" + BL.CLS_Session.brno + "' and date between'" + datval.convert_to_yyyy_MMddwith_dash(xxx) + " " + maskedTextBox3.Text + "' and '" + datval.convert_to_yyyy_MMddwith_dash(zzz) + " " + maskedTextBox4.Text + "' " + conds + "", con3);

                DataSet ds3 = new DataSet();
                da3.Fill(ds3, "0");
              //  da4.Fill(ds3, "1");
                dataGridView1.DataSource = ds3.Tables["0"];
                double sum1=0;
                for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                {
                    sum1 += Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value);
                }
                txt_total.Text = sum1.ToString();
               // txt_cost.Text = ds3.Tables[1].Rows[0][1].ToString();

               // txt_tax.Text = ds3.Tables[1].Rows[0]["tax"].ToString();
              //  txt_net1.Text = ds3.Tables[1].Rows[0]["net1"].ToString();

              //  txt_cash.Text = ds3.Tables[1].Rows[0]["cash"].ToString();
              //  txt_card.Text = ds3.Tables[1].Rows[0]["card"].ToString();

              //  textBox2.Text = ds3.Tables[1].Rows[0][2].ToString();
              //  textBox1.Text = ds3.Tables[1].Rows[0][3].ToString();
               // txt_other.Text = ds3.Tables[1].Rows[0]["othr"].ToString();

                // dataGridView1.Columns[9].Visible = false;
                // dataGridView1.Columns[10].Visible = false;
                dtt = ds3.Tables["0"];

                //}
                //else
                //{// and type = " + typeno + "
                //    string xxx;
                //    xxx = maskedTextBox1.Text.ToString();

                //    string zzz;
                //    zzz = maskedTextBox2.Text.ToString();
                //    SqlDataAdapter da3 = new SqlDataAdapter("select contr ,case [type] when 1 then 'نقدي' when 0 then 'مرتجع' end as [type],ref,(CONVERT(varchar, date, 25)) [date] ,[total] ,[count] ,[payed],[total_cost],[saleman] ,[req_no] ,[cust_no],[discount],[net_total] from pos_hdr where date between'" + xxx + "' and '" + zzz + "' and type = " + typeno + "", con3);
                //    SqlDataAdapter da4 = new SqlDataAdapter("select sum(total),sum(total_cost),sum(discount),sum(net_total) from pos_hdr where date between'" + xxx + "' and '" + zzz + "' and type = " + typeno + "", con3);
                //    DataSet ds3 = new DataSet();
                //    da3.Fill(ds3, "0");
                //    da4.Fill(ds3, "1");
                //    dataGridView1.DataSource = ds3.Tables["0"];
                //    txt_total.Text = ds3.Tables[1].Rows[0][0].ToString();
                //    txt_cost.Text = ds3.Tables[1].Rows[0][1].ToString();

                //    textBox1.Text = ds3.Tables[1].Rows[0][2].ToString();
                //    textBox2.Text = ds3.Tables[1].Rows[0][3].ToString();

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

        private void SalesReport_Load(object sender, EventArgs e)
        {
            //dateTimePicker1.Format = DateTimePickerFormat.Custom;
            //dateTimePicker2.Format = DateTimePickerFormat.Custom;
            //dateTimePicker1.CustomFormat = "dd-MM-yyyy";
            //dateTimePicker2.CustomFormat = "dd-MM-yyyy";

            FastReport.Utils.Config.ReportSettings.ShowProgress = false;

            string sl = BL.CLS_Session.slkey.Replace(" ", "','").Remove(0, 2) + "'";

            dtslctr = daml.SELECT_QUIRY_only_retrn_dt("select * from slcenters where sl_brno + sl_no in(" + sl + ") and sl_brno='" + BL.CLS_Session.brno + "'");
            cmb_salctr.DataSource = dtslctr;
            cmb_salctr.DisplayMember = "sl_name";
            cmb_salctr.ValueMember = "sl_no";
            cmb_salctr.SelectedIndex = -1;
            /*
            maskedTextBox1.Text = DateTime.Now.ToString("yyyy-MM-01") + "05:00:00.000";

            DateTime dt = new DateTime();

            dt=DateTime.Now.AddDays(1);

           // maskedTextBox2.Text = DateTime.Now.ToString("yyyy-MM-dd") + "05:00:00";
            maskedTextBox2.Text = dt.ToString("yyyy-MM-dd") + "05:00:00.000";
             * */
            PrinterSettings settings = new PrinterSettings();
            printer_nam = settings.PrinterName;

            maskedTextBox1.Text = DateTime.Now.ToString("dd-MM-yyyy", new CultureInfo("en-US"));
            maskedTextBox2.Text = DateTime.Now.AddDays(1).ToString("dd-MM-yyyy", new CultureInfo("en-US"));

            dateTimePicker1.Text = DateTime.Now.ToString("dd-MM-yyyy", new CultureInfo("en-US"));
            dateTimePicker2.Text = DateTime.Now.AddDays(1).ToString("dd-MM-yyyy", new CultureInfo("en-US"));

          //  maskedTextBox3.Text = "05:00:00.000";
           // maskedTextBox4.Text = "04:59:59.999";
            maskedTextBox3.Text = (BL.CLS_Session.posatrtday.ToString().Length == 1 ? "0" + BL.CLS_Session.posatrtday.ToString() : BL.CLS_Session.posatrtday.ToString()) + ":00:00.000";
           // maskedTextBox4.Text = ((BL.CLS_Session.posatrtday - 1).ToString().Length == 1 ? "0" + (BL.CLS_Session.posatrtday - 1).ToString() : (BL.CLS_Session.posatrtday - 1).ToString()) + ":59:59.999";
            if (BL.CLS_Session.posatrtday != 0)
                maskedTextBox4.Text = ((BL.CLS_Session.posatrtday - 1).ToString().Length == 1 ? "0" + (BL.CLS_Session.posatrtday - 1).ToString() : (BL.CLS_Session.posatrtday - 1).ToString()) + ":59:59.999";
            else
                maskedTextBox4.Text = "23:59:59.999";

            maskedTextBox1.Select();
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
                SalesDtlReport sdtr = new SalesDtlReport(checkBox1.Checked ? "0" : "1");
                //MAIN mn = new MAIN();
                //sdtr.MdiParent = mn;

                sdtr.ShowDialog();


            }
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {


            //// int selectedIndex = dataGridView1.CurrentCell.RowIndex;
            //DilySalesReport.cc = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            //// if (selectedIndex > -1)
            //// {
            ////dataGridView1.Rows.RemoveAt(selectedIndex);
            ////dataGridView1.Refresh(); // if needed


            //// }
            //SalesDtlReport sdtr = new SalesDtlReport(checkBox1.Checked ? "0" : "1");
            ////MAIN mn = new MAIN();
            ////sdtr.MdiParent = mn;

            //sdtr.ShowDialog();



        }

        private void label3_Click(object sender, EventArgs e)
        {

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

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            // dataGridView1_CellContentDoubleClick((object)sender, (DataGridViewCellEventArgs)e);

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

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {/*
            Reports.Report_Form rrf = new Reports.Report_Form("DR", dtt);
            rrf.MdiParent = ParentForm;
            rrf.Show();
          */
            try
            {
                Acc.rpt.Acc_frmPrint rf = new Acc.rpt.Acc_frmPrint();


                // DataSet ds1 = new DataSet();
                //  DataSet ds5 = new DataSet();
                DataTable dt = new DataTable();

                int cn = 1;
                foreach (DataGridViewColumn col in dataGridView1.Columns)
                {
                    /*
                    dt.Columns.Add(col.Name);
                    // dt.Columns.Add(col.HeaderText);
                     * */
                    dt.Columns.Add("c" + cn.ToString());
                    cn++;
                }

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    DataRow dRow = dt.NewRow();
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        dRow[cell.ColumnIndex] = cell.Value;
                    }
                    dt.Rows.Add(dRow);
                }




                rf.reportViewer1.LocalReport.DataSources.Clear();
                rf.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));
                rf.reportViewer1.LocalReport.ReportEmbeddedResource = "POS.Pos.rpt.Dily_Sal_Report_rpt.rdlc";



                // ReportParameter[] parameters = new ReportParameter[2];
                // parameters[0] = new ReportParameter("comp_name", BL.CLS_Session.cmp_name);
                // parameters[1] = new ReportParameter("mdate", dthdr.Rows[0][3].ToString().Substring(6, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(4, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(0, 4));

                // rf.reportViewer1.LocalReport.SetParameters(parameters);
                ReportParameter[] parameters = new ReportParameter[5];
                parameters[0] = new ReportParameter("comp_name", BL.CLS_Session.cmp_name);
                //  parameters[1] = new ReportParameter("mdate", dthdr.Rows[0][3].ToString().Substring(6, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(4, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(0, 4));
                parameters[1] = new ReportParameter("fmdate", maskedTextBox1.Text);
                parameters[2] = new ReportParameter("tmdate", maskedTextBox2.Text);
                parameters[3] = new ReportParameter("br_name", BL.CLS_Session.brname);
                parameters[4] = new ReportParameter("title", this.Text);

                rf.reportViewer1.LocalReport.SetParameters(parameters);

                rf.reportViewer1.RefreshReport();
                rf.MdiParent = ParentForm;
                rf.Show();
            }
            catch { }
        }

        private void dataGridView1_DoubleClick_1(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[6])
            {
                try
                {
                    Search_All f4 = new Search_All("5-r", "Cus");
                    f4.ShowDialog();
                    dataGridView1.CurrentRow.Cells[6].Value = f4.dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    dataGridView1.CurrentRow.Cells[10].Value = f4.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    //  txt_temp.Text = f4.dataGridView1.CurrentRow.Cells[3].Value.ToString();
                    // txt_taxid.Text = f4.dataGridView1.CurrentRow.Cells[5].Value.ToString();
                }
                catch { }
            }
            else
            {
                Pos.Resturant_ReSales dp = new Pos.Resturant_ReSales(dataGridView1.CurrentRow.Cells[0].Value.ToString(), dataGridView1.CurrentRow.Cells[9].Value.ToString(), dataGridView1.CurrentRow.Cells[1].Value.ToString());
                dp.MdiParent = MdiParent;
                dp.Show();
            }
            //try
            //{
            //    if (dataGridView1.CurrentRow.Cells[0].Value.ToString() != "" && dataGridView1.CurrentRow.Cells[2].Value.ToString() != "")
            //    {
            //        int toprt = Convert.ToInt32(dataGridView1.CurrentRow.Cells[2].Value.ToString());
            //        int toprtc = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            //        string tbld = checkBox1.Checked ? "d_pos_dtl" : "pos_dtl";
            //        string tblh = checkBox1.Checked ? "d_pos_hdr" : "pos_hdr";
            //        SqlDataAdapter dacr = new SqlDataAdapter("select [brno],[slno],[ref],[contr],[type],[barcode],[name],[unit],([price]-([price]*[discpc]/100)) price,[qty],[cost],[is_req],[itemno],[srno],[pkqty],[offr_amt] from " + tbld + " where ref=" + toprt + " and contr=" + toprtc + "", con3);
            //        SqlDataAdapter dacr1 = new SqlDataAdapter("select * from " + tblh + " where ref=" + toprt + " and contr=" + toprtc + "", con3);
            //        SqlDataAdapter chk = new SqlDataAdapter("select sum(isnull(is_req,0)) from " + tbld + " where ref=" + toprt + " and contr=" + toprtc + "", con3);

            //        DataSet1 ds = new DataSet1();

            //        ds.Tables["dtl"].Clear();
            //        ds.Tables["hdr"].Clear();
            //        ds.Tables["sum"].Clear();

            //        dacr.Fill(ds, "dtl");
            //        dacr1.Fill(ds, "hdr");
            //        chk.Fill(ds, "sum");

            //        //DataSet ds = new DataSet();



            //        //dacr.Fill(ds, "dtl");
            //        //dacr1.Fill(ds, "hdr");
            //        //chk.Fill(ds, "sum");

            //        // DataTable dtchk = new DataTable();
            //        //// dtchk.Clear();

            //        // chk.Fill(dtchk);

            //        if (BL.CLS_Session.prnt_type.Equals("1"))
            //        {
            //            try
            //            {
            //                //FastReport.Report rpt2 = new FastReport.Report();


            //                //rpt2.Load(Environment.CurrentDirectory + @"\reports\Pos_Sale_rpt.frx");

            //                // rpt2.SetParameterValue("br_name", BL.CLS_Session.brname);
            //                FastReport.Report rpt = new FastReport.Report();

            //                if (dataGridView1.CurrentRow.Cells[1].Value.ToString().Equals("بيع نقدي"))
            //                    rpt.Load(Environment.CurrentDirectory + @"\reports\Pos_Sale_rpt.frx");
            //                else
            //                    rpt.Load(Environment.CurrentDirectory + @"\reports\Pos_RSale_rpt.frx");
            //                //  rpt.SetParameterValue("br_name", BL.CLS_Session.brname);
            //                rpt.SetParameterValue("Comp_Name", BL.CLS_Session.cmp_name);
            //                rpt.SetParameterValue("Br_Name", BL.CLS_Session.brname);
            //                rpt.SetParameterValue("inv_bar", ds.Tables["hdr"].Rows[0][3].ToString() + "-" + ds.Tables["hdr"].Rows[0][2].ToString());
            //                rpt.SetParameterValue("Msg1", BL.CLS_Session.msg1.ToString());
            //                rpt.SetParameterValue("Msg2", BL.CLS_Session.msg2.ToString());
            //                rpt.SetParameterValue("taxper", BL.CLS_Session.tax_per.ToString());
            //                rpt.SetParameterValue("tax_type", BL.CLS_Session.autotax ? "3" : "0");

            //                rpt.RegisterData(ds.Tables["hdr"], "pos_hdr");
            //                rpt.RegisterData(ds.Tables["dtl"], "pos_dtl");

            //                FR_Viewer rptd = new FR_Viewer(rpt);

            //                rptd.MdiParent = MdiParent;
            //                rptd.Show();
            //            }
            //            catch (Exception ex)
            //            {
            //                MessageBox.Show(ex.Message);
            //            }

            //        }
            //        else
            //        {
            //            if (ds.Tables["sum"].Rows.Count > 0 && Convert.ToInt32(ds.Tables["sum"].Rows[0][0]) > 0)
            //            {
            //                // rpt.SalesReport_req report = new rpt.SalesReport_req();
            //                rpt.SalesReport111 report = new rpt.SalesReport111();
            //                //  CrystalReport1 report = new CrystalReport1();
            //                report.SetDataSource(ds);
            //                report.SetParameterValue("Comp_Name", BL.CLS_Session.cmp_name);
            //                report.SetParameterValue("br_name", BL.CLS_Session.brname);
            //                report.SetParameterValue("inv_bar", "*" + ds.Tables["hdr"].Rows[0][3].ToString() + "-" + ds.Tables["hdr"].Rows[0][2].ToString() + "*");
            //                report.SetParameterValue("msg1", BL.CLS_Session.msg1.ToString());
            //                report.SetParameterValue("msg2", BL.CLS_Session.msg2.ToString());
            //                report.SetParameterValue("taxper", BL.CLS_Session.tax_per.ToString());
            //                //    crystalReportViewer1.ReportSource = report;
            //                RPT.FRM_RPT_PRODUCT prt = new RPT.FRM_RPT_PRODUCT();

            //                prt.crystalReportViewer1.ReportSource = report;

            //                prt.ShowDialog();

            //                //  crystalReportViewer1.Refresh();

            //                // report.PrintOptions.PrinterName = "pos";

            //                // report.PrintToPrinter(1, false, 0, 0);
            //                // report.PrintToPrinter(0,true, 1, 1);
            //                report.Close();
            //                // report.Dispose();
            //            }
            //            else
            //            {
            //                // reports.SalesReport report = new reports.SalesReport();

            //                // rpt.SalesReport report = new rpt.SalesReport();
            //                rpt.SalesReport111 report = new rpt.SalesReport111();

            //                //  CrystalReport1 report = new CrystalReport1();
            //                report.SetDataSource(ds);
            //                report.SetParameterValue("Comp_Name", BL.CLS_Session.cmp_name);
            //                report.SetParameterValue("br_name", BL.CLS_Session.brname);
            //                report.SetParameterValue("inv_bar", "*" + ds.Tables["hdr"].Rows[0][3].ToString() + "-" + ds.Tables["hdr"].Rows[0][2].ToString() + "*");
            //                report.SetParameterValue("msg1", BL.CLS_Session.msg1.ToString());
            //                report.SetParameterValue("msg2", BL.CLS_Session.msg2.ToString());
            //                report.SetParameterValue("taxper", BL.CLS_Session.tax_per.ToString());

            //                RPT.FRM_RPT_PRODUCT prt = new RPT.FRM_RPT_PRODUCT();

            //                prt.crystalReportViewer1.ReportSource = report;

            //                prt.ShowDialog();
            //                //    crystalReportViewer1.ReportSource = report;

            //                //  crystalReportViewer1.Refresh();

            //                // report.PrintOptions.PrinterName = "pos";

            //                // report.PrintToPrinter(1, false, 0, 0);
            //                // report.PrintToPrinter(0, true, 1, 1);
            //                report.Close();
            //                // report.Dispose();
            //            }
            //        }
            //    }
            //    else
            //    {
            //        MessageBox.Show("لا يوجد فاتورة");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        private void maskedTextBox1_Enter(object sender, EventArgs e)
        {
            SendKeys.Send("{Home}");
        }

        private void maskedTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                maskedTextBox2.Focus();
            }
        }

        private void maskedTextBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.Focus();
            }
        }

        private void طباعةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Convert.ToDouble(datval.convert_to_yyyyDDdd(Convert.ToDateTime((BL.CLS_Session.minstart.Substring(4, 2) + "/" + BL.CLS_Session.minstart.Substring(6, 2) + "/" + BL.CLS_Session.minstart.Substring(0, 4)), new CultureInfo("en-US", false)).AddDays(365).ToString())) < Convert.ToDouble(DateTime.Now.ToString("yyyyMMdd", new CultureInfo("en-US", false))))
            {
                MessageBox.Show(" بيانات الصيانه مفقودة يرجى التواصل مع الدعم الفني لتحديثها ", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string tbld = checkBox1.Checked ? "d_pos_dtl" : "pos_dtl";
            string tblh = checkBox1.Checked ? "d_pos_hdr" : "pos_hdr";

            SqlDataAdapter dacr1 = new SqlDataAdapter("select a.[brno],a.[slno],a.[ref],a.[contr],a.[type],a.[date],a.[total],a.[count],a.[payed],a.[total_cost],b.sl_name [saleman],a.[req_no],a.[cust_no],a.[discount],a.[net_total],a.[sysdate],a.[gen_ref],a.[tax_amt],a.[card_amt],a.[note],a.[mobile] from " + tblh + " a join pos_salmen b on a.saleman=b.sl_id where a.ref=" + dataGridView1.CurrentRow.Cells[2].Value + " and a.[contr]=" + dataGridView1.CurrentRow.Cells[0].Value + " and  a.[brno]= '" + BL.CLS_Session.brno + "' ", con3);
            SqlDataAdapter dacr = new SqlDataAdapter("select [brno],[slno],[ref],[contr],[type],[barcode],[name],[unit],([price]-([price]*[discpc]/100)) price,[qty],[cost],[is_req],[itemno],[srno],[pkqty],[offr_amt] from " + tbld + " where ref=" + dataGridView1.CurrentRow.Cells[2].Value + " and [contr]=" + dataGridView1.CurrentRow.Cells[0].Value + " and  [brno]= '" + BL.CLS_Session.brno + "' ", con3);

            DataSet1 ds = new DataSet1();

            dacr1.Fill(ds, "hdr");
            dacr.Fill(ds, "dtl");

            if (BL.CLS_Session.prnt_type.Equals("1"))
            {
                try
                {
                    //FastReport.Report rpt2 = new FastReport.Report();


                    //rpt2.Load(Environment.CurrentDirectory + @"\reports\Pos_Sale_rpt.frx");

                    // rpt2.SetParameterValue("br_name", BL.CLS_Session.brname);
                    FastReport.Report rpt = new FastReport.Report();

                    if (dataGridView1.CurrentRow.Cells[1].Value.ToString().Equals("بيع نقدي"))
                        rpt.Load(Environment.CurrentDirectory + @"\reports\Pos_Sale_rpt.frx");
                    else
                        rpt.Load(Environment.CurrentDirectory + @"\reports\Pos_RSale_rpt.frx");
                    //  rpt.SetParameterValue("br_name", BL.CLS_Session.brname);
                    rpt.SetParameterValue("Comp_Name", BL.CLS_Session.cmp_name);
                    rpt.SetParameterValue("Br_Name", BL.CLS_Session.brname);
                    rpt.SetParameterValue("inv_bar", ds.Tables["hdr"].Rows[0][3].ToString() + "-" + ds.Tables["hdr"].Rows[0][2].ToString());
                    rpt.SetParameterValue("Msg1", BL.CLS_Session.msg1.ToString());
                    rpt.SetParameterValue("Msg2", BL.CLS_Session.msg2.ToString());
                    rpt.SetParameterValue("taxper", BL.CLS_Session.tax_per.ToString());
                    rpt.SetParameterValue("tax_type", BL.CLS_Session.autotax ? "3" : "0");
                    rpt.SetParameterValue("tax_id", BL.CLS_Session.tax_no);

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

            if (BL.CLS_Session.prnt_type.Equals("0"))
            {
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
            report.SetParameterValue("cashir",  BL.CLS_Session.contr_id);
            report.SetParameterValue("other_amt", txt_other.Text);
            report.SetParameterValue("tax_amt", txt_tax.Text);

            report.PrintOptions.PrinterName = settings.PrinterName.ToString();//"POS";
            report.PrintToPrinter(1, false, 0, 0);
            // report.PrintToPrinter(0,true, 1, 1);
            report.Close();
            }
            else
            {
                FastReport.Report rpt = new FastReport.Report();
                rpt.Load(Environment.CurrentDirectory + @"\reports\Final_Express_Rpt.frx");
                rpt.SetParameterValue("Comp_Name", BL.CLS_Session.cmp_name.ToString());
                rpt.SetParameterValue("date", maskedTextBox1.Text.Substring(0, 10) + " To " + maskedTextBox2.Text.Substring(0, 10));
                rpt.SetParameterValue("salman", "");

                rpt.SetParameterValue("total", txt_total.Text);
                rpt.SetParameterValue("disc", textBox2.Text);
                rpt.SetParameterValue("net", textBox1.Text);

                rpt.SetParameterValue("net_cash", txt_cash.Text);
                rpt.SetParameterValue("net_card", txt_card.Text);
                rpt.SetParameterValue("cashir",  BL.CLS_Session.contr_id );
                rpt.SetParameterValue("other_amt", txt_other.Text);
                rpt.SetParameterValue("tax_amt", txt_tax.Text);

                rpt.PrintSettings.Printer = printer_nam;// "pos";
                rpt.PrintSettings.ShowDialog = false;
               // FastReport.Utils.Config.ReportSettings.ShowProgress = false;
                rpt.Print();

            }
        }

        private void maskedTextBox1_Leave(object sender, EventArgs e)
        {
            try
            {
                string datestr = maskedTextBox1.Text.ToString().Replace("-", "").Trim();
                if (datestr.Length == 0)
                {
                    maskedTextBox1.Text = DateTime.Now.ToString("dd", new CultureInfo("en-US", false)) + DateTime.Now.ToString("MM", new CultureInfo("en-US", false)) + DateTime.Now.ToString("yyyy", new CultureInfo("en-US", false));
                    // txt_hdate.Text = convert_to_hdate(txt_hdate.Text.ToString());
                }
                else
                {
                    if (datestr.Length == 2)
                    {
                        maskedTextBox1.Text = datestr + DateTime.Now.ToString("MM", new CultureInfo("en-US", false)) + DateTime.Now.ToString("yyyy", new CultureInfo("en-US", false));
                        //  txt_hdate.Text = convert_to_hdate(txt_mdate.Text.ToString());
                    }
                    else
                    {
                        if (datestr.Length == 4)
                        {
                            maskedTextBox1.Text = datestr + DateTime.Now.ToString("yyyy", new CultureInfo("en-US", false));
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

        private void maskedTextBox2_Leave(object sender, EventArgs e)
        {
            try
            {
                string datestr = maskedTextBox2.Text.ToString().Replace("-", "").Trim();
                if (datestr.Length == 0)
                {
                    maskedTextBox2.Text = DateTime.Now.ToString("dd", new CultureInfo("en-US", false)) + DateTime.Now.ToString("MM", new CultureInfo("en-US", false)) + DateTime.Now.ToString("yyyy", new CultureInfo("en-US", false));
                    // txt_hdate.Text = convert_to_hdate(txt_hdate.Text.ToString());
                }
                else
                {
                    if (datestr.Length == 2)
                    {
                        maskedTextBox2.Text = datestr + DateTime.Now.ToString("MM", new CultureInfo("en-US", false)) + DateTime.Now.ToString("yyyy", new CultureInfo("en-US", false));
                        //  txt_hdate.Text = convert_to_hdate(txt_mdate.Text.ToString());
                    }
                    else
                    {
                        if (datestr.Length == 4)
                        {
                            maskedTextBox2.Text = datestr + DateTime.Now.ToString("yyyy", new CultureInfo("en-US", false));
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

        private void txt_driver_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F8)
            {
                // txt_custnam.Text = "";
                try
                {
                    Search_All f4 = new Search_All("5-r", "Cus");
                    f4.ShowDialog();
                    txt_driver.Text = f4.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    txt_drivernam.Text = f4.dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    //  txt_temp.Text = f4.dataGridView1.CurrentRow.Cells[3].Value.ToString();
                    // txt_taxid.Text = f4.dataGridView1.CurrentRow.Cells[5].Value.ToString();
                }
                catch { }

            }
        }

        private void txt_driver_Leave(object sender, EventArgs e)
        {
            DataTable dt2 = daml.SELECT_QUIRY_only_retrn_dt("select sp_id ,sp_name from pos_drivers a where a.sp_brno='" + BL.CLS_Session.brno + "' and sp_id='" + txt_driver.Text + "'");

            // dataGridView1.DataSource = daml.SELECT_QUIRY(qstr2);

            if (dt2.Rows.Count > 0)
            {
                txt_driver.Text = dt2.Rows[0][0].ToString();
                txt_drivernam.Text = dt2.Rows[0][1].ToString();
                // txt_temp.Text = dt2.Rows[0][3].ToString();
                //  txt_taxid.Text = dt2.Rows[0]["vndr_taxcode"].ToString();
            }
            else
            {
                txt_driver.Text = "";
                txt_drivernam.Text = "";
                //txt_temp.Text = "";
                //  txt_crlmt.Text = "";
            }
        }

        private void txt_drivernam_Click(object sender, EventArgs e)
        {
            try
            {
                Search_All f4 = new Search_All("5-r", "Cus");
                f4.ShowDialog();
                txt_driver.Text = f4.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                txt_drivernam.Text = f4.dataGridView1.CurrentRow.Cells[1].Value.ToString();
                //  txt_temp.Text = f4.dataGridView1.CurrentRow.Cells[3].Value.ToString();
                // txt_taxid.Text = f4.dataGridView1.CurrentRow.Cells[5].Value.ToString();
            }
            catch { }
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int testInt = 1;
                datval.formate_dgv(this.dataGridView1, testInt);
            }
            catch { }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_allp.Checked)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    row.Cells[8].Value = true;
                }
            }
            else
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    row.Cells[8].Value = false;
                }
            }

        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    using (SqlCommand cmd1 = new SqlCommand("update pos_hdr_r set dpayed=@a3,driver_id=@a4,driver_nam=@a5 where dref=" + row.Cells[1].Value + " and dcontr=" + row.Cells[0].Value + "", con3))
                    {

                        cmd1.Parameters.AddWithValue("@a3", string.IsNullOrEmpty(row.Cells[8].Value.ToString()) ? "0" : Convert.ToBoolean(row.Cells[8].Value)? "1" : "0");
                        cmd1.Parameters.AddWithValue("@a4", row.Cells[10].Value.ToString());
                        cmd1.Parameters.AddWithValue("@a5", row.Cells[6].Value.ToString());
                        if (con3.State == ConnectionState.Closed)
                            con3.Open();

                        cmd1.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("تم الحفظ", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            catch (Exception ex)
            {
                MessageBox.Show("لم يتم الحفظ" + ex.Message,"",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void txt_custnam_Click(object sender, EventArgs e)
        {
            try
            {
                Search_All f4 = new Search_All("5", "Cus");
                f4.ShowDialog();
                txt_custno.Text = f4.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                txt_custnam.Text = f4.dataGridView1.CurrentRow.Cells[1].Value.ToString();
             //   txt_temp.Text = f4.dataGridView1.CurrentRow.Cells[2].Value.ToString();
                // txt_taxid.Text = f4.dataGridView1.CurrentRow.Cells[5].Value.ToString();
            }
            catch { }
        }

        private void txt_custno_Leave(object sender, EventArgs e)
        {
            DataTable dt2 = daml.SELECT_QUIRY_only_retrn_dt("select cu_no ,cu_name ,cl_acc,cu_tel,vndr_taxcode from customers a join cuclass b on a.cu_class=b.cl_no  where a.inactive=0 and a.cu_brno='" + BL.CLS_Session.brno + "' and cu_no='" + txt_custno.Text + "'");

            // dataGridView1.DataSource = daml.SELECT_QUIRY(qstr2);

            if (dt2.Rows.Count > 0)
            {
                txt_custnam.Text = dt2.Rows[0][1].ToString();
                txt_custno.Text = dt2.Rows[0][0].ToString();
               // txt_temp.Text = dt2.Rows[0][3].ToString();
                //  txt_taxid.Text = dt2.Rows[0]["vndr_taxcode"].ToString();
            }
            else
            {
                txt_custnam.Text = "";
                txt_custno.Text = "";
               // txt_temp.Text = "";
                //  txt_crlmt.Text = "";
            }
        }

        private void txt_custno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F8)
            {
                // txt_custnam.Text = "";
                try
                {
                    Search_All f4 = new Search_All("5", "Cus");
                    f4.ShowDialog();
                    txt_custno.Text = f4.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    txt_custnam.Text = f4.dataGridView1.CurrentRow.Cells[1].Value.ToString();
                 //   txt_temp.Text = f4.dataGridView1.CurrentRow.Cells[2].Value.ToString();
                    // txt_taxid.Text = f4.dataGridView1.CurrentRow.Cells[5].Value.ToString();
                }
                catch { }

            }
        }
    }
}
