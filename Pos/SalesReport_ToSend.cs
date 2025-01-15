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
using ZatcaIntegrationSDK;
using ZatcaIntegrationSDK.BLL;
using ZatcaIntegrationSDK.HelperContracts;
using ZatcaIntegrationSDK.APIHelper;

namespace POS.Pos
{
    public partial class SalesReport_ToSend : BaseForm
    {
        int countt = 0;
        BL.DAML daml = new BL.DAML();
        BL.Date_Validate datval = new BL.Date_Validate();
        DataTable dtt, dtslctr;
        public static int qq;
        public static string allll = "";
        string typeno = "",printer_nam = "";
        SqlConnection con3 = BL.DAML.con; //new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        public SalesReport_ToSend(string alll)
        {
            InitializeComponent();
            allll = alll;
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
                string cndtdv = chk_delvr.Checked ? " and type=5 " : "";
                string cndtyp = comboBox2.SelectedIndex == 1 ? " and type=1 " : comboBox2.SelectedIndex == 2 ? " and type=0 " : " ";
                string conds = cmb_salctr.SelectedIndex != -1 ? " and slno='" + cmb_salctr.SelectedValue + "' " : " ";
                string condt = string.IsNullOrEmpty(txt_contr.Text) ? " " : " and contr=" + txt_contr.Text + " ";
                string condam = string.IsNullOrEmpty(txt_salman.Text) ? " " : " and saleman=" + txt_salman.Text + " ";
                string condcncl = checkBox1.Checked ? " d_pos_hdr " : " pos_hdr ";
                string tos = checkBox2.Checked ? " and zatka_status is null or zatka_status=0 " : " ";
                string tos3 = checkBox3.Checked ? " and wrning_msg<>'' " : " ";
                string xxx;
                xxx = maskedTextBox1.Text.ToString();

                string zzz;
                zzz = maskedTextBox2.Text.ToString();
                //SqlDataAdapter da3 = new SqlDataAdapter("select a_type a,a_ref b,a_mdate c,a_hdate,a_text d,a_amt e  where date between '20180101' and '20190101'", con3);
                //SqlDataAdapter da4 = new SqlDataAdapter("select sum(total),sum(total_cost),sum(discount),sum(net_total) from pos_hdr where date between'" + xxx + "' and '" + zzz + "'", con3);
                //  SqlDataAdapter da3 = new SqlDataAdapter("select contr ,case [type] when 1 then 'نقدي' when 0 then 'مرتجع' end as [type],ref,(CONVERT(varchar, date, 25)) [date] ,[total] ,[count] ,[payed],[total_cost],[saleman] ,[req_no] ,[cust_no],[discount],[net_total] from "+condcncl+" where date between'" + xxx + "' and '" + zzz + "' " + conds + condt + condam + "", con3);
                //  SqlDataAdapter da3 = new SqlDataAdapter("select contr ,case [type] when 1 then 'بيع نقدي' when 0 then 'مرتجع نقدي' when 2 then 'مرتجع اجل' when 3 then 'بيع اجل' end as [type],ref,(CONVERT(varchar(10), date, 25)) [date],[count] ,round((case type when 1 then total  when 0 then -total end),2)[total],round((case type when 1 then discount  when 0 then -discount end),2)[discount],round(((case type when 1 then net_total  when 0 then -net_total end) - (case type when 1 then tax_amt  when 0 then -tax_amt end)),2) net1,round((case type when 1 then tax_amt  when 0 then -tax_amt end),2) tax,round((case type when 1 then net_total  when 0 then -net_total end),2)[net_total],payed,round((case type when 1 then total_cost  when 0 then -total_cost end),2)[total_cost],[saleman] from " + condcncl + " where  brno='" + BL.CLS_Session.brno + "' and date between'" + datval.convert_to_yyyy_MMddwith_dash(xxx) + " 05:00:00.000' and '" + datval.convert_to_yyyy_MMddwith_dash(zzz) + " 05:00:00.000' " + conds + condt + condam + cndtyp + "", con3);
                SqlDataAdapter da3 = new SqlDataAdapter("SELECT slno, ref, contr, type, uuid, hash, qr_code, file_name, xml_err_msg, status_code, wrning_msg, zatka_err_msg, (case zatka_status when 1 then 'مرسلة' else 'غير مرسلة' end) 'الحالة' FROM  pos_esend where  brno='" + BL.CLS_Session.brno + "' " + tos + tos3 + "", con3);

                //  SqlDataAdapter da3 = new SqlDataAdapter("select * from hdr where date like'%" + xxx + "%'", con3);
                // SqlDataAdapter da4 = new SqlDataAdapter("select sum(total) from hdr where date between'" + xxx + "' and '" + yyy + "'", con3);
               // SqlDataAdapter da4 = new SqlDataAdapter("select round(sum(case type when 1 then total when 5 then total when 0 then -total end),2),round(sum(case type when 1 then total_cost when 5 then total_cost when 0 then -total_cost end),2),round(sum(case type when 1 then discount when 5 then discount when 0 then -discount end),2),round(sum(case type when 1 then net_total when 5 then net_total when 0 then -net_total end ),2),round(sum(case type when 1 then tax_amt when 5 then tax_amt when 0 then -tax_amt end),2) tax ,round(sum(((case type when 1 then net_total when 5 then net_total when 0 then -net_total end) - (case type when 1 then tax_amt when 5 then tax_amt  when 0 then -tax_amt end))),2) net1,round(sum(case type when 1 then net_total-card_amt when 5 then net_total-card_amt when 0 then -net_total end),2)cash ,round(isnull(sum(case when type in (1,5) and card_type in(1,2,3) then card_amt  else 0 end),0),2) card,round(isnull(sum(case when type in(1,5) and card_type in(4) then card_amt  else 0 end),0),2) othr,round(sum(case type when 3 then net_total  else 0 end ),2) netagl,round(isnull(sum(case when type in(1,5) and card_type in(2) then card_amt  else 0 end),0),2) master,round(isnull(sum(case when type in(1,5) and card_type in(3) then card_amt  else 0 end),0),2) visa from " + condcncl + " where  brno='" + BL.CLS_Session.brno + "' and date between'" + datval.convert_to_yyyy_MMddwith_dash(xxx) + " " + maskedTextBox3.Text + "' and '" + datval.convert_to_yyyy_MMddwith_dash(zzz) + " " + maskedTextBox4.Text + "' " + conds + condt + condam + cndtyp + cndtdv + "", con3);

                DataSet ds3 = new DataSet();
                da3.Fill(ds3, "0");
               ////// da4.Fill(ds3, "1");
                dataGridView1.DataSource = ds3.Tables["0"];
               //// txt_total.Text = ds3.Tables[1].Rows[0][0].ToString();
               //// txt_cost.Text = ds3.Tables[1].Rows[0][1].ToString();

               //// txt_tax.Text = ds3.Tables[1].Rows[0]["tax"].ToString();
               //// txt_net1.Text = ds3.Tables[1].Rows[0]["net1"].ToString();

               //// txt_cash.Text = ds3.Tables[1].Rows[0]["cash"].ToString();
               //// txt_card.Text = ds3.Tables[1].Rows[0]["card"].ToString();

               //// textBox2.Text = ds3.Tables[1].Rows[0][2].ToString();
               //// textBox1.Text = ds3.Tables[1].Rows[0][3].ToString();
               //// txt_other.Text = ds3.Tables[1].Rows[0]["othr"].ToString();
               //// txt_agl.Text = ds3.Tables[1].Rows[0]["netagl"].ToString();
               //// txt_master.Text = ds3.Tables[1].Rows[0]["master"].ToString();
               //// txt_visa.Text = ds3.Tables[1].Rows[0]["visa"].ToString();

               //// // dataGridView1.Columns[9].Visible = false;
               //// // dataGridView1.Columns[10].Visible = false;
               //// dtt = ds3.Tables["0"];

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
            if (!BL.CLS_Session.cmp_type.Equals("m"))
                button8.Visible = false;
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

            maskedTextBox1.Text = DateTime.Now.ToString("01-MM-yyyy", new CultureInfo("en-US"));
            maskedTextBox2.Text = DateTime.Now.AddDays(1).ToString("dd-MM-yyyy", new CultureInfo("en-US"));
            maskedTextBox3.Text =  (BL.CLS_Session.posatrtday.ToString().Length == 1 ? "0" + BL.CLS_Session.posatrtday.ToString() : BL.CLS_Session.posatrtday.ToString()) + ":00:00.000";

            if(BL.CLS_Session.posatrtday!=0)
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
                ReportParameter[] parameters = new ReportParameter[4];
                parameters[0] = new ReportParameter("comp_name", BL.CLS_Session.cmp_name);
                //  parameters[1] = new ReportParameter("mdate", dthdr.Rows[0][3].ToString().Substring(6, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(4, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(0, 4));
                parameters[1] = new ReportParameter("fmdate", maskedTextBox1.Text);
                parameters[2] = new ReportParameter("tmdate", maskedTextBox2.Text);
                parameters[3] = new ReportParameter("br_name", BL.CLS_Session.brname);

                rf.reportViewer1.LocalReport.SetParameters(parameters);

                rf.reportViewer1.RefreshReport();
                rf.MdiParent = ParentForm;
                rf.Show();
            }
            catch { }
        }

        private void dataGridView1_DoubleClick_1(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow.Cells[0].Value.ToString() != "" && dataGridView1.CurrentRow.Cells[2].Value.ToString() != "")
                {
                    int toprt = Convert.ToInt32(dataGridView1.CurrentRow.Cells[1].Value.ToString());
                    int toprtc = Convert.ToInt32(dataGridView1.CurrentRow.Cells[2].Value.ToString());
                    string tbld = checkBox1.Checked ? "d_pos_dtl" : "pos_dtl";
                    string tblh = checkBox1.Checked ? "d_pos_hdr" : "pos_hdr";
                    SqlDataAdapter dacr = new SqlDataAdapter("select [brno],[slno],[ref],[contr],[type],[barcode],[name],[unit],([price]-([price]*[discpc]/100)) price,[qty],[cost],[is_req],[itemno],[srno],[pkqty],[offr_amt] from " + tbld + " where ref=" + toprt + " and contr=" + toprtc + "", con3);
                    SqlDataAdapter dacr1 = new SqlDataAdapter("select * from " + tblh + " where ref=" + toprt + " and contr=" + toprtc + "", con3);
                    SqlDataAdapter chk = new SqlDataAdapter("select sum(isnull(is_req,0)) from " + tbld + " where ref=" + toprt + " and contr=" + toprtc + "", con3);

                    DataSet1 ds = new DataSet1();

                    ds.Tables["dtl"].Clear();
                    ds.Tables["hdr"].Clear();
                    ds.Tables["sum"].Clear();

                    dacr.Fill(ds, "dtl");
                    dacr1.Fill(ds, "hdr");
                    chk.Fill(ds, "sum");

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

                            if (dataGridView1.CurrentRow.Cells[3].Value.ToString().Equals("بيع نقدي"))
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

                           // rpt.SetParameterValue("qr", Convert.ToBase64String(tlvs.GetQRCode()));
                            rpt.SetParameterValue("qr", dataGridView1.CurrentRow.Cells[6].Value);
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
                    else
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
                    MessageBox.Show("لا يوجد فاتورة");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
            string tbld = checkBox1.Checked ? "d_pos_dtl" : "pos_dtl";
            string tblh = checkBox1.Checked ? "d_pos_hdr" : "pos_hdr";

            SqlDataAdapter dacr1 = new SqlDataAdapter("select a.[brno],a.[slno],a.[ref],a.[contr],a.[type],a.[date],a.[total],a.[count],a.[payed],a.[total_cost],b.sl_name [saleman],a.[req_no],a.[cust_no],a.[discount],a.[net_total],a.[sysdate],a.[gen_ref],a.[tax_amt],a.[card_amt],a.[note],a.[mobile] from " + tblh + " a join pos_salmen b on a.saleman=b.sl_id where a.ref=" + dataGridView1.CurrentRow.Cells[1].Value + " and a.[contr]=" + dataGridView1.CurrentRow.Cells[2].Value + " and  a.[brno]= '" + BL.CLS_Session.brno + "' ", con3);
            SqlDataAdapter dacr = new SqlDataAdapter("select [brno],[slno],[ref],[contr],[type],[barcode],[name],[unit],([price]-([price]*[discpc]/100)) price,[qty],[cost],[is_req],[itemno],[srno],[pkqty],[offr_amt] from " + tbld + " where ref=" + dataGridView1.CurrentRow.Cells[1].Value + " and [contr]=" + dataGridView1.CurrentRow.Cells[2].Value + " and  [brno]= '" + BL.CLS_Session.brno + "' ", con3);

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

                    if (dataGridView1.CurrentRow.Cells[3].Value.ToString().Equals("بيع نقدي"))
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

                   // rpt.SetParameterValue("qr", Convert.ToBase64String(tlvs.GetQRCode()));
                    rpt.SetParameterValue("qr", dataGridView1.CurrentRow.Cells[6].Value);

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
            report.SetParameterValue("cashir", string.IsNullOrEmpty(txt_contr.Text) ? BL.CLS_Session.contr_id : txt_contr.Text);
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
                rpt.SetParameterValue("salman", txt_salman.Text);

                rpt.SetParameterValue("total", txt_total.Text);
                rpt.SetParameterValue("disc", textBox2.Text);
                rpt.SetParameterValue("net", textBox1.Text);

                rpt.SetParameterValue("net_cash", txt_cash.Text);
                rpt.SetParameterValue("net_card", txt_card.Text);
                rpt.SetParameterValue("cashir", string.IsNullOrEmpty(txt_contr.Text) ? BL.CLS_Session.contr_id : txt_contr.Text);
                rpt.SetParameterValue("other_amt", txt_other.Text);
                rpt.SetParameterValue("tax_amt", txt_tax.Text);
                rpt.SetParameterValue("agl", txt_agl.Text);
                rpt.SetParameterValue("master", txt_master.Text);
                rpt.SetParameterValue("visa", txt_visa.Text);

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

        private void button8_Click(object sender, EventArgs e)
        {
            //CultureInfo culture = new CultureInfo("en-US");
            //DateTime tempDate = Convert.ToDateTime(datval.convert_to_yyyy_MMddwith_dash(txt_mdate.Text) + " " + (BL.CLS_Session.posatrtday.ToString().Length == 1 ? "0" + BL.CLS_Session.posatrtday.ToString() : BL.CLS_Session.posatrtday.ToString()) + ":00:00.000", culture).AddHours(24);
            //// string.
            ////  DateTime yyy = DateTime.TryParse(xxx).AddHours(24);
            ////    MessageBox.Show(tempDate.ToString("dd-MM-yyyy", new CultureInfo("en-US")));
            ////   MessageBox.Show(datval.convert_to_yyyy_MMddwith_dash(txt_mdate.Text) + " 05:00:00.000");

            //zzz = tempDate.ToString("dd-MM-yyyy", new CultureInfo("en-US"));
            //xxx = txt_mdate.Text;

            FastReport.Report rpt = new FastReport.Report();
            rpt.Load(Environment.CurrentDirectory + @"\reports\Final_Express_Gr_Rpt.frx");
            rpt.SetParameterValue("Comp_Name", BL.CLS_Session.cmp_name.ToString());
            rpt.SetParameterValue("date", maskedTextBox1.Text.ToString() + " - " + maskedTextBox2.Text.ToString());
            rpt.SetParameterValue("salman", txt_salman.Text);

            rpt.SetParameterValue("total", txt_total.Text);
            rpt.SetParameterValue("disc", textBox2.Text);
            rpt.SetParameterValue("net", textBox1.Text);

            rpt.SetParameterValue("net_cash", txt_cash.Text);
            rpt.SetParameterValue("net_card", txt_card.Text);
            rpt.SetParameterValue("cashir", string.IsNullOrEmpty(txt_contr.Text) ? BL.CLS_Session.contr_id : txt_contr.Text);
            rpt.SetParameterValue("other_amt", txt_other.Text);
            rpt.SetParameterValue("tax_amt", txt_tax.Text);
            rpt.SetParameterValue("agl", txt_agl.Text);
            rpt.SetParameterValue("master", txt_master.Text);
            rpt.SetParameterValue("visa", txt_visa.Text);

            string qstr = "  Select g.group_id,g.group_name,printer= isnull(round(sum((case when b.type in (1,3,4,5) then 1 else -1 end)*(((b.qty*(b.price-(b.price*b.discpc/100))))-offr_amt))-sum((case when b.type in (1,3,4,5) then 1 else -1 end)*(((b.qty*(b.price-(b.price*b.discpc/100))))-offr_amt)*c.dscper/100),2),0)  "
                // + ",tax=round(sum((case when b.invtype in ('06','07') then 1 else -1 end)*(b.qty*b.pkqty)),2) "
                // + "+sum((case when b.invtype in ('06','07') then 1 else -1 end)*(b.tax_amt))-sum((case when b.invtype in ('06','07') then 1 else -1 end)*(b.qty*(b.price-(b.price*b.discpc/100))*(a.invdsvl/(a.nettotal+a.invdsvl)))),2),0)"
                // + ",qty=isnull(round(sum((case when b.type in (1,3,4,5) then 1 else -1 end)*(b.qty*b.pkqty)),2),0) "
                // + ",per = 0.00  from groups g with (NOLOCK) left Join items s with (NOLOCK) inner Join pos_hdr c with (NOLOCK) inner Join pos_dtl b with (NOLOCK) "
                 + " from groups g with (NOLOCK) left Join items s with (NOLOCK) inner Join pos_hdr c with (NOLOCK) inner Join pos_dtl b with (NOLOCK) "
                 + "on  c.brno = b.brno and c.contr = b.contr and c.ref = b.ref and c.slno=b.slno and c.date between '" + datval.convert_to_yyyy_MMddwith_dash(maskedTextBox1.Text.ToString()) + " " + (BL.CLS_Session.posatrtday.ToString().Length == 1 ? "0" + BL.CLS_Session.posatrtday.ToString() : BL.CLS_Session.posatrtday.ToString()) + ":00:00.000' and '" + datval.convert_to_yyyy_MMddwith_dash(maskedTextBox2.Text.ToString()) + " " + (BL.CLS_Session.posatrtday.ToString().Length == 1 ? "0" + BL.CLS_Session.posatrtday.ToString() : BL.CLS_Session.posatrtday.ToString()) + ":00:00.000' on s.item_no=b.itemno on g.group_id=s.item_group "
                //+ "  where 1=1 and g.group_pid is null group by g.group_id,g.group_name order by cast(g.group_id as int) ";
                 + "   where 1=1 and g.group_pid is null group by g.group_id,g.group_name having isnull(round(sum((case when b.type in (1,3,4,5) then 1 else -1 end)*(((b.qty*(b.price-(b.price*b.discpc/100))))-offr_amt))-sum((case when b.type in (1,3,4,5) then 1 else -1 end)*(((b.qty*(b.price-(b.price*b.discpc/100))))-offr_amt)*c.dscper/100),2),0) <>0 order by cast(g.group_id as int) "
                 + "  ";
            // System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.DataTable dt = daml.CMD_SELECT_QUIRY_only_retrn_dt(qstr);

            rpt.RegisterData(dt, "groups");

            // rpt.PrintSettings.Printer = printer_nam;// "pos";
            //// rpt.PrintSettings.ShowDialog = false;
            // // FastReport.Utils.Config.ReportSettings.ShowProgress = false;
            // rpt.Print();

            FR_Viewer rptd = new FR_Viewer(rpt);

            rptd.MdiParent = MdiParent;
            rptd.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (BL.CLS_Session.is_einv_p2 && Convert.ToDouble(datval.convert_to_yyyyDDdd(maskedTextBox1.Text)) <= Convert.ToDouble(BL.CLS_Session.einv_p2_date))
                 {
                     MessageBox.Show(" لا يمكن تجهيز ومشاركة فواتير قديمة ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                     return;
                 }
                DataTable dth = daml.SELECT_QUIRY_only_retrn_dt("select * from pos_hdr a join pos_esend b on  a.[brno]=a.[brno] and a.[slno]=b.[slno] and a.[ref]=b.[ref] and a.[contr]=b.[contr]  where b.zatka_status is null or b.zatka_status=0");
               // DataTable dth = daml.SELECT_QUIRY_only_retrn_dt("select * from pos_hdr a join pos_esend b on  a.[brno]=a.[brno] and a.[slno]=b.[slno] and a.[ref]=b.[ref] and a.[contr]=b.[contr]  where b.status_code<>200");
               // DataTable dth = daml.SELECT_QUIRY_only_retrn_dt(" select * from pos_hdr where ref>44266");
                progressBar1.Value = 0;
                progressBar1.Minimum = 0;
                progressBar1.Maximum = dth.Rows.Count;
                progressBar1.Visible = true;
                
                //MessageBox.Show(dth.Rows.Count.ToString());
                foreach (DataRow dtrh in dth.Rows)
                {
                    UBLXML ubl = new UBLXML();
                    Invoice inv = new Invoice();
                    Result res = new Result();

                   // DataTable dthash = daml.SELECT_QUIRY_only_retrn_dt("select hash from pos_esend where ref=" + (Convert.ToInt32(dtrh[2].ToString()) - 1) + "");
                    DataTable dthash = daml.SELECT_QUIRY_only_retrn_dt("select [inv_hash],[zref]+1 from pos_hash");
                    inv.ID = dtrh[2].ToString(); //"1230"; // مثال SME00010
                    inv.UUID = Guid.NewGuid().ToString();
                    inv.IssueDate = Convert.ToDateTime(dtrh[5]).ToString("yyyy-MM-dd", new CultureInfo("en-US", false));
                    inv.IssueTime = Convert.ToDateTime(dtrh[5]).ToString("HH:mm:ss", new CultureInfo("en-US", false));
                    //388 فاتورة  
                    //383 اشعار مدين
                    //381 اشعار دائن
                    inv.invoiceTypeCode.id = dtrh[4].ToString().Equals("1") && dtrh[21].ToString().Equals("0") ? 388 : dtrh[4].ToString().Equals("1") && !dtrh[21].ToString().Equals("0") ? 383 : 381;
                    //inv.invoiceTypeCode.Name based on format NNPNESB
                    //NN 01 فاتورة عادية
                    //NN 02 فاتورة مبسطة
                    //P فى حالة فاتورة لطرف ثالث نكتب 1 فى الحالة الاخرى نكتب 0
                    //N فى حالة فاتورة اسمية نكتب 1 وفى الحالة الاخرى نكتب 0
                    // E فى حالة فاتورة للصادرات نكتب 1 وفى الحالة الاخرى نكتب 0
                    //S فى حالة فاتورة ملخصة نكتب 1 وفى الحالة الاخرى نكتب 0
                    //B فى حالة فاتورة ذاتية نكتب 1
                    //B فى حالة ان الفاتورة صادرات=1 لايمكن ان تكون الفاتورة ذاتية =1
                    //
                    inv.invoiceTypeCode.Name = "0200000";
                    inv.DocumentCurrencyCode = "SAR";//العملة
                    inv.TaxCurrencyCode = "SAR"; ////فى حالة الدولار لابد ان تكون عملة الضريبة بالريال السعودى

                    if (inv.invoiceTypeCode.id == 383 || inv.invoiceTypeCode.id == 381)
                    {
                        // فى حالة ان اشعار دائن او مدين فقط هانكتب رقم الفاتورة اللى اصدرنا الاشعار ليها
                        // in case of return sales invoice or debit notes we must mention the original sales invoice number
                        InvoiceDocumentReference invoiceDocumentReference = new InvoiceDocumentReference();
                        invoiceDocumentReference.ID = "Invoice Number: " + dtrh[21].ToString() + " "; // mandatory in case of return sales invoice or debit notes
                        inv.billingReference.invoiceDocumentReferences.Add(invoiceDocumentReference);
                    }
                    // inv.CurrencyRate = decimal.Parse("3.75"); // قيمة الدولار مقابل الريال
                    // فى حالة ان اشعار دائن او مدين فقط هانكتب رقم الفاتورة اللى اصدرنا الاشعار ليها
                    //inv.billingReference.InvoiceDocumentReferenceID = "123654"; رقم فاتورة البيع في حال ارجاع الفاتورة 
                    // هنا ممكن اضيف ال pih من قاعدة البيانات  
                    if (dthash.Rows.Count > 0)
                        inv.AdditionalDocumentReferencePIH.EmbeddedDocumentBinaryObject = string.IsNullOrEmpty(dthash.Rows[0][0].ToString()) ? "NWZlY2ViNjZmZmM4NmYzOGQ5NTI3ODZjNmQ2OTZjNzljMmRiYzIzOWRkNGU5MWI0NjcyOWQ3M2EyN2ZiNTdlOQ==" : dthash.Rows[0][0].ToString();
                    else
                        inv.AdditionalDocumentReferencePIH.EmbeddedDocumentBinaryObject = "NWZlY2ViNjZmZmM4NmYzOGQ5NTI3ODZjNmQ2OTZjNzljMmRiYzIzOWRkNGU5MWI0NjcyOWQ3M2EyN2ZiNTdlOQ==";
                    // قيمة عداد الفاتورة
                   // inv.AdditionalDocumentReferenceICV.UUID = Convert.ToInt32(dtrh[2].ToString()); // لابد ان يكون ارقام فقط
                    inv.AdditionalDocumentReferenceICV.UUID = string.IsNullOrEmpty(dthash.Rows[0][1].ToString()) ? 1 : Convert.ToInt32(dthash.Rows[0][1].ToString()); //Convert.ToInt32(dtrh[2].ToString()); // لابد ان يكون ارقام فقط
                    //فى حالة فاتورة مبسطة وفاتورة ملخصة هانكتب تاريخ التسليم واخر تاريخ التسليم
                    // inv.delivery.ActualDeliveryDate = "2022-10-22"; للضريبية فقط اجباري
                    // inv.delivery.LatestDeliveryDate = "2022-10-23"; للضريبية فقط اختياري
                    //
                    //بيانات الدفع 
                    string paymentcode = "10";
                    if (!string.IsNullOrEmpty(paymentcode))
                    {
                        PaymentMeans paymentMeans = new PaymentMeans();
                        paymentMeans.PaymentMeansCode = paymentcode; // optional for invoices - mandatory for return invoice - debit notes
                        if (inv.invoiceTypeCode.id == 383 || inv.invoiceTypeCode.id == 381)
                        {
                            paymentMeans.InstructionNote =inv.invoiceTypeCode.id == 381? "return items": "add items"; //the reason of return invoice - debit notes // manatory only for return invoice - debit notes 
                        }
                        inv.paymentmeans.Add(paymentMeans);
                    }
                    // اكواد معين
                    // اختيارى كود الدفع
                    //inv.paymentmeans.PaymentMeansCode = "42";//اختيارى
                    //inv.paymentmeans.InstructionNote = "Payment Notes"; //اجبارى فى الاشعارات
                    //inv.paymentmeans.payeefinancialaccount.ID = "";//اختيارى
                    //inv.paymentmeans.payeefinancialaccount.paymentnote = "Payment by credit";//اختيارى

                    //بيانات البائع 
                    inv.SupplierParty.partyIdentification.ID = BL.CLS_Session.dtcomp.Rows[0]["ownr_mob"].ToString();// "123456"; // رقم السجل التجارى الخاض بالبائع
                    inv.SupplierParty.partyIdentification.schemeID = BL.CLS_Session.cmpschem;// "CRN"; //رقم السجل التجارى
                    inv.SupplierParty.postalAddress.StreetName = BL.CLS_Session.dtcomp.Rows[0]["street"].ToString();// "streetnumber";// اجبارى
                    inv.SupplierParty.postalAddress.AdditionalStreetName = "";// "ststtstst"; //اختيارى
                    inv.SupplierParty.postalAddress.BuildingNumber = BL.CLS_Session.dtcomp.Rows[0]["bulding_no"].ToString();// "3724"; // اجبارى رقم المبنى
                    //inv.SupplierParty.postalAddress.PlotIdentification = "9833";//اختيارى رقم القطعة
                    inv.SupplierParty.postalAddress.CityName = BL.CLS_Session.dtcomp.Rows[0]["city"].ToString();// "gaddah"; //اسم المدينة
                    inv.SupplierParty.postalAddress.PostalZone = BL.CLS_Session.dtcomp.Rows[0]["postal_code"].ToString();// "15385";//الرقم البريدي
                    inv.SupplierParty.postalAddress.CountrySubentity = "";// BL.CLS_Session.dtcomp.Rows[0]["city"].ToString();// "makka";//اسم المحافظة او المدينة مثال (مكة) اختيارى
                    inv.SupplierParty.postalAddress.CitySubdivisionName = BL.CLS_Session.dtcomp.Rows[0]["site_name"].ToString();// "flassk";// اسم المنطقة او الحى 
                    inv.SupplierParty.postalAddress.country.IdentificationCode = "SA";
                    inv.SupplierParty.partyLegalEntity.RegistrationName = BL.CLS_Session.cmp_name;// "على ابراهيم"; // اسم الشركة المسجل فى الهيئة
                    inv.SupplierParty.partyTaxScheme.CompanyID = BL.CLS_Session.tax_no;// "300300868600003";// رقم التسجيل الضريبي

                    ////inv.legalMonetaryTotal.PayableAmount = decimal.Parse(dtrh[14].ToString());// اجمالي الفاتورة
                    ////inv.legalMonetaryTotal.TaxInclusiveAmount = decimal.Parse(dtrh[14].ToString());
                    ////inv.legalMonetaryTotal.TaxExclusiveAmount = decimal.Parse(dtrh[14].ToString()) - decimal.Parse(dtrh[17].ToString());
                    ////inv.legalMonetaryTotal.LineExtensionAmount = decimal.Parse(dtrh[14].ToString()) - decimal.Parse(dtrh[17].ToString());

                    // inv.legalMonetaryTotal.TaxInclusiveAmount = decimal.Parse(dtrh[14].ToString());
                    ////if (decimal.Parse(dtrh[13].ToString()) > 0)
                    ////{
                    ////    //this code incase of there is a discount in invoice level 
                    ////    AllowanceCharge allowance = new AllowanceCharge();
                    ////    //ChargeIndicator = false means that this is discount
                    ////    //ChargeIndicator = true means that this is charges(like cleaning service - transportation)
                    ////    allowance.ChargeIndicator = false;// يعني خصم وليس رسوم
                    ////    //write this lines in case you will make discount as percentage
                    ////    allowance.MultiplierFactorNumeric = 0; //dscount percentage like 10
                    ////    allowance.BaseAmount = 0; // the amount we will apply percentage on example (MultiplierFactorNumeric=10 ,BaseAmount=1000 then AllowanceAmount will be 100 SAR)

                    ////    // in case we will make discount as Amount 
                    ////    allowance.Amount = decimal.Parse(dtrh[13].ToString()); // 
                    ////    allowance.AllowanceChargeReasonCode = ""; //discount or charge reason code
                    ////    allowance.AllowanceChargeReason = "discount"; //discount or charge reson
                    ////    allowance.taxCategory.ID = "S";// كود الضريبة tax code (S Z O E )
                    ////    allowance.taxCategory.Percent = decimal.Parse(BL.CLS_Session.tax_per.ToString()); //;// نسبة الضريبة tax percentage (0 - 15 - 5 )
                    ////    //فى حالة عندى اكثر من خصم بعمل loop على الاسطر السابقة
                    ////    inv.allowanceCharges.Add(allowance);
                    ////}
                    ////if (decimal.Parse(dtrh[13].ToString()) > 0)
                    ////{
                    ////    //this code incase of there is a discount in invoice level 
                    ////    AllowanceCharge allowance = new AllowanceCharge();
                    ////    //ChargeIndicator = false means that this is discount
                    ////    //ChargeIndicator = true means that this is charges(like cleaning service - transportation)
                    ////    allowance.ChargeIndicator = false;// يعني خصم وليس رسوم
                    ////    //write this lines in case you will make discount as percentage
                    ////    allowance.MultiplierFactorNumeric = 0; //dscount percentage like 10
                    ////    allowance.BaseAmount = 0; // the amount we will apply percentage on example (MultiplierFactorNumeric=10 ,BaseAmount=1000 then AllowanceAmount will be 100 SAR)

                    ////    // in case we will make discount as Amount 
                    ////    allowance.Amount = decimal.Parse(dtrh[13].ToString()); // 
                    ////    allowance.AllowanceChargeReasonCode = ""; //discount or charge reason code
                    ////    allowance.AllowanceChargeReason = "discount"; //discount or charge reson
                    ////    allowance.taxCategory.ID = "Z";// كود الضريبة tax code (S Z O E )
                    ////    allowance.taxCategory.Percent = 0; //;// نسبة الضريبة tax percentage (0 - 15 - 5 )
                    ////    //فى حالة عندى اكثر من خصم بعمل loop على الاسطر السابقة
                    ////    inv.allowanceCharges.Add(allowance);
                    ////}

                    DataTable dtdtl = daml.SELECT_QUIRY_only_retrn_dt("select a.*,b.tax_id,b.tax_percent,b.zatka_code,b.zatka_reason from pos_dtl a join taxs b on a.tax_id=b.tax_id where a.ref=" + dtrh[2].ToString() + "");
                    // فى حالة فى اكتر من منتج فى الفاتورة هانعمل ليست من invoiceline مثال الكود التالى
                    // for (int i = 1; i <= dtdtl.Rows.Count; i++)
                   // MessageBox.Show(dtdtl.Rows.Count.ToString());
                    foreach (DataRow dtrd in dtdtl.Rows)
                    {
                      //  MessageBox.Show(dtdtl.Rows.Count.ToString());
                        InvoiceLine invline = new InvoiceLine();
                        invline.InvoiceQuantity = decimal.Parse(dtrd[9].ToString());
                        //invline.allowanceCharge.AllowanceChargeReason = "discount"; //سبب الخصم على مستوى المنتج
                        //invline.allowanceCharge.Amount = 10;//قيم الخصم

                        //if the price is including vat set EncludingVat=true;
                        //invline.price.EncludingVat = true;
                        invline.price.EncludingVat = false;

                        // invline.price.PriceAmount = decimal.Parse(dtrd[8].ToString());// سعر المنتج بعد الخصم 
                        invline.price.PriceAmount = decimal.Parse(dtrd[8].ToString()) - (decimal.Parse(dtrd[17].ToString()) / decimal.Parse(dtrd[9].ToString()));// سعر المنتج بعد الخصم 

                        invline.item.Name = dtrd[6].ToString();


                        //invline.price.allowanceCharge.AllowanceChargeReason = "discount"; //سبب الخصم على مستوى المنتج
                        //invline.price.allowanceCharge.Amount = 0;//قيم الخصم
                        if (decimal.Parse(dtrd[17].ToString()) == 0)
                        {
                            //item Tax code
                            invline.item.classifiedTaxCategory.ID = dtrd[22].ToString().Equals("VATEX-SA-35") ? "Z" : "O"; // كود الضريبة
                            //item Tax code
                            invline.taxTotal.TaxSubtotal.taxCategory.ID = dtrd[22].ToString().Equals("VATEX-SA-35") ? "Z" : "O"; // كود الضريبة
                            //item Tax Exemption Reason Code mentioned in zatca pdf page(32-33)
                            invline.taxTotal.TaxSubtotal.taxCategory.TaxExemptionReasonCode = dtrd[22].ToString();// "VATEX-SA-35"; // كود الضريبة
                            //item Tax Exemption Reason mentioned in zatca pdf page(32-33)
                            invline.taxTotal.TaxSubtotal.taxCategory.TaxExemptionReason = dtrd[23].ToString();// "Medicines and medical equipment"; // كود الضريبة
                            invline.item.classifiedTaxCategory.Percent = 0; // نسبة الضريبة
                            invline.taxTotal.TaxSubtotal.taxCategory.Percent = 0; // نسبة الضريبة

                        }
                        else
                        {
                            //item Tax code
                            invline.item.classifiedTaxCategory.ID = "S"; // كود الضريبة
                            //item Tax code
                            invline.taxTotal.TaxSubtotal.taxCategory.ID = "S"; // كود الضريبة
                            invline.item.classifiedTaxCategory.Percent = decimal.Parse(BL.CLS_Session.tax_per.ToString()); // نسبة الضريبة
                            invline.taxTotal.TaxSubtotal.taxCategory.Percent = decimal.Parse(BL.CLS_Session.tax_per.ToString()); // نسبة الضريبة
                        }
                        // invline.item.classifiedTaxCategory.ID = "S";// كود الضريبة
                        //// invline.item.classifiedTaxCategory.Percent = decimal.Parse(BL.CLS_Session.tax_per.ToString());// نسبة الضريبة

                        // invline.taxTotal.TaxSubtotal.taxCategory.ID = "S";//كود الضريبة
                        //// invline.taxTotal.TaxSubtotal.taxCategory.Percent = decimal.Parse(BL.CLS_Session.tax_per.ToString());//نسبة الضريبة
                        //invline.taxTotal.TaxSubtotal.taxCategory.TaxExemptionReason = "Private healthcare to citizen";
                        //invline.taxTotal.TaxSubtotal.taxCategory.TaxExemptionReasonCode = "VATEX-SA-HEA";
                        //invline.taxTotal.TaxSubtotal.taxCategory.TaxExemptionReason = "";
                        //invline.taxTotal.TaxSubtotal.taxCategory.TaxExemptionReasonCode = "";
                        if (decimal.Parse(dtrh[18].ToString()) > 0)
                        // if (0 > 1)
                        {
                            // incase there is discount in invoice line level
                            AllowanceCharge allowanceCharge = new AllowanceCharge();
                            // فى حالة الرسوم incase of charges
                            // allowanceCharge.ChargeIndicator = true;
                            // فى حالة الخصم incase of discount
                            allowanceCharge.ChargeIndicator = false;

                            allowanceCharge.AllowanceChargeReason = "discount"; // سبب الخصم على مستوى المنتج
                            // allowanceCharge.AllowanceChargeReasonCode = "90"; // سبب الخصم على مستوى المنتج
                            //allowanceCharge.Amount = decimal.Parse(dtrd[19].ToString()); // قيم الخصم discount amount or charge amount

                            // allowanceCharge.MultiplierFactorNumeric = decimal.Parse(dtrh[18].ToString()); //0;
                            allowanceCharge.MultiplierFactorNumeric = ((decimal.Parse(dtrd[19].ToString()) / ((decimal.Parse(dtrd[8].ToString()) * decimal.Parse(dtrd[9].ToString())) - decimal.Parse(dtrd[17].ToString()) - decimal.Parse(dtrd[19].ToString()))) * 100) + decimal.Parse(dtrh[18].ToString()); //0;
                            // allowanceCharge.BaseAmount = decimal.Parse(dtrd[8].ToString()) - (decimal.Parse(dtrd[17].ToString()) / decimal.Parse(dtrd[9].ToString())); //0;
                            // allowanceCharge.BaseAmount = (decimal.Parse(dtrd[8].ToString()) - (decimal.Parse(dtrd[17].ToString()) / decimal.Parse(dtrd[9].ToString()))) * decimal.Parse(dtrd[9].ToString()); //0;
                            allowanceCharge.BaseAmount = (decimal.Parse(dtrd[8].ToString()) * decimal.Parse(dtrd[9].ToString())) - decimal.Parse(dtrd[17].ToString()) - decimal.Parse(dtrd[19].ToString()); //0;
                            invline.allowanceCharges.Add(allowanceCharge);
                        }
                        else
                        {
                            // incase there is discount in invoice line level
                            AllowanceCharge allowanceCharge = new AllowanceCharge();
                            // فى حالة الرسوم incase of charges
                            // allowanceCharge.ChargeIndicator = true;
                            // فى حالة الخصم incase of discount
                            allowanceCharge.ChargeIndicator = false;

                            allowanceCharge.AllowanceChargeReason = "discount"; // سبب الخصم على مستوى المنتج
                            // allowanceCharge.AllowanceChargeReasonCode = "90"; // سبب الخصم على مستوى المنتج
                            allowanceCharge.Amount = decimal.Parse(dtrd[19].ToString()); // قيم الخصم discount amount or charge amount

                            allowanceCharge.MultiplierFactorNumeric = 0;
                            allowanceCharge.BaseAmount = 0;
                            invline.allowanceCharges.Add(allowanceCharge);
                        }
                        inv.InvoiceLines.Add(invline);
                    }

                    res = ubl.GenerateInvoiceXML(inv, Directory.GetCurrentDirectory());


                    if (res.IsValid)
                    {
                       // daml.Insert_Update_Delete_Only(@"INSERT INTO [dbo].[pos_esend]([brno],[slno],[ref],[contr],[type],[uuid],[hash],[qr_code],[file_name],[encoded_xml],[z_ref]) VALUES ('" + dtrh[0] + "','" + dtrh[1] + "','" + dtrh[2] + "','" + dtrh[3] + "','" + dtrh[4] + "','" + inv.UUID + "','" + res.InvoiceHash + "','" + res.QRCode + "','" + res.SingedXMLFileName + "','" + res.EncodedInvoice + "'," + dthash.Rows[0][1] + ") ", false);
                        daml.Insert_Update_Delete_Only(@"update [dbo].[pos_esend] set [uuid]='" + inv.UUID + "', [hash]='" + res.InvoiceHash + "' ,[qr_code]='" + res.QRCode + "',[file_name]='" + res.SingedXMLFileName + "',[encoded_xml]='" + res.EncodedInvoice + "',[z_ref]=" + dthash.Rows[0][1] + " where [brno]='" + dtrh[0] + "' and [slno]='" + dtrh[1] + "' and [ref]=" + dtrh[2] + " and [contr]=" + dtrh[3] + " and [type]=" + dtrh[4] + "", false);
                        
                        daml.Insert_Update_Delete_Only(@"update pos_hash set [ref]=" + dtrh[2] + ",[zref]=" + dthash.Rows[0][1] + ",[inv_hash]='" + res.InvoiceHash + "' ", false);
                        //القيم التالية تحتاج ان تحفظها فى سطر الفاتورة فى قاعدة البيانات الخاصة بكم  كي تكون مرجع لكم لاحقاً
                        //MessageBox.Show(res.InvoiceHash);
                        //MessageBox.Show(res.SingedXML);
                        //MessageBox.Show(res.EncodedInvoice);
                        //MessageBox.Show(res.UUID);
                        //MessageBox.Show(res.QRCode);
                        //MessageBox.Show(res.PIH);
                        //MessageBox.Show(res.SingedXMLFileName);
                    }
                    progressBar1.Increment(1);
                }
                MessageBox.Show("تم التجهيز بنجاح", "Erroe", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                progressBar1.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erroe", MessageBoxButtons.OK, MessageBoxIcon.Error);
                progressBar1.Visible = false;
            }
            
        }

        private void button6_Click(object sender, EventArgs e)
        {

//            UBLXML ubl = new UBLXML();
//            Invoice inv = new Invoice();
//            ZatcaIntegrationSDK.Result res = new ZatcaIntegrationSDK.Result();

//            inv.ID = TextBox8.Text; // invoice number in your system example= SME00010

//            inv.IssueDate = DateTimePicker1.Value.ToString("yyyy-MM-dd"); //invoice issue date with format yyyy-MM-dd example "2023-02-07"
//            inv.IssueTime = DateTimePicker2.Value.ToString("HH:mm:ss"); // invoice issue date with format HH:mm:ss example "09:32:40"
//            //all needed codes for invoiceTypeCode id
//            // 388 sales invoice  
//            // 383 debit note
//            // 381 credit note
//            // get invoice type 
//            inv.invoiceTypeCode.id = GetInvoiceType();
//            // inv.invoiceTypeCode.Name based on format NNPNESB
//            // NN 01 standard invoice 
//            // NN 02 simplified invoice
//            // P فى حالة فاتورة لطرف ثالث نكتب 1 فى الحالة الاخرى نكتب 0
//            // N فى حالة فاتورة اسمية نكتب 1 وفى الحالة الاخرى نكتب 0
//            // E فى حالة فاتورة للصادرات نكتب 1 وفى الحالة الاخرى نكتب 0
//            // S فى حالة فاتورة ملخصة نكتب 1 وفى الحالة الاخرى نكتب 0
//            // B فى حالة فاتورة ذاتية نكتب 1
//            // B فى حالة ان الفاتورة صادرات=1 لايمكن ان تكون الفاتورة ذاتية =1
//            // 
//            inv.invoiceTypeCode.Name = GetInvoiceTypeName();
//            inv.DocumentCurrencyCode = "SAR"; // Document Currency Code (invoice currency example SAR or USD) 
//            inv.TaxCurrencyCode = "SAR";// Tax Currency Code it must be with SAR
//            //inv.CurrencyRate = 3.75m; // incase of DocumentCurrencyCode equal any currency code not SAR we must mention CurrencyRate value
//            if (inv.invoiceTypeCode.id == 383 || inv.invoiceTypeCode.id == 381)
//            {
//                // فى حالة ان اشعار دائن او مدين فقط هانكتب رقم الفاتورة اللى اصدرنا الاشعار ليها
//                // in case of return sales invoice or debit notes we must mention the original sales invoice number
//                InvoiceDocumentReference invoiceDocumentReference = new InvoiceDocumentReference();
//                invoiceDocumentReference.ID = "Invoice Number: 354; Invoice Issue Date: 2021-02-10"; // mandatory in case of return sales invoice or debit notes
//                inv.billingReference.invoiceDocumentReferences.Add(invoiceDocumentReference);
//            }


//            // هنا ممكن اضيف ال pih من قاعدة البيانات  
//            //this is previous invoice hash (the invoice hash of last invoice ) res.InvoiceHash
//            // for the first invoice and because there is no previous hash we must write this code "NWZlY2ViNjZmZmM4NmYzOGQ5NTI3ODZjNmQ2OTZjNzljMmRiYzIzOWRkNGU5MWI0NjcyOWQ3M2EyN2ZiNTdlOQ=="
//            inv.AdditionalDocumentReferencePIH.EmbeddedDocumentBinaryObject = "NWZlY2ViNjZmZmM4NmYzOGQ5NTI3ODZjNmQ2OTZjNzljMmRiYzIzOWRkNGU5MWI0NjcyOWQ3M2EyN2ZiNTdlOQ==";
//            // قيمة عداد الفاتورة
//            // Invoice counter (1,2,3,4) this counter must start from 1 for each CSID
//            inv.AdditionalDocumentReferenceICV.UUID = Int32.Parse(TextBox7.Text); // لابد ان يكون ارقام فقط must be numbers only

//            if (inv.invoiceTypeCode.Name.Substring(0, 2) == "01")
//            {
//                //supply date mandatory only for standard invoices
//                // فى حالة فاتورة مبسطة وفاتورة ملخصة هانكتب تاريخ التسليم واخر تاريخ التسليم
//                inv.delivery.ActualDeliveryDate = "2022-10-22";
//                inv.delivery.LatestDeliveryDate = "2022-10-23";
//            }
//            // 
//            // بيانات الدفع 
//            // اكواد معين
//            // اختيارى كود الدفع
//            // payment methods mandatory for return invoice and debit notes and optional for invoices
//            string paymentcode = GetPaymentMethod();
//            if (!string.IsNullOrEmpty(paymentcode))
//            {
//                PaymentMeans paymentMeans = new PaymentMeans();
//                paymentMeans.PaymentMeansCode = paymentcode; // optional for invoices - mandatory for return invoice - debit notes
//                if (inv.invoiceTypeCode.id == 383 || inv.invoiceTypeCode.id == 381)
//                {
//                    paymentMeans.InstructionNote = "dameged items"; //the reason of return invoice - debit notes // manatory only for return invoice - debit notes 
//                }
//                inv.paymentmeans.Add(paymentMeans);
//            }

//            // بيانات البائع 
//            //seller date
//            //other identifier for seller like commercial registration number
//            inv.SupplierParty.partyIdentification.ID = textBox4.Text; // رقم السجل التجارى الخاض بالبائع
//            //other identifier scheme id example CRN for commercial registration number
//            inv.SupplierParty.partyIdentification.schemeID = cmb_seller_scheme.SelectedValue.ToString(); // رقم السجل التجارى
//            //seller street name mandatory
//            inv.SupplierParty.postalAddress.StreetName = textBox11.Text; // اجبارى
//            //inv.SupplierParty.postalAddress.AdditionalStreetName = ""; // اختيارى
//            //seller buliding number mandatory must be 4 digits
//            inv.SupplierParty.postalAddress.BuildingNumber = textBox12.Text; // اجبارى رقم المبنى
//            // inv.SupplierParty.postalAddress.PlotIdentification = "9833"; //اختيارى
//            //seller city name 
//            inv.SupplierParty.postalAddress.CityName = "taif"; // اسم المدينة
//            //seller postal zone must be 5 digits 
//            inv.SupplierParty.postalAddress.PostalZone = textBox14.Text; // الرقم البريدي
//            //inv.SupplierParty.postalAddress.CountrySubentity = "Riyadh Region"; // اسم المحافظة او المدينة مثال (مكة) اختيارى
//            //seller City Subdivision Name
//            inv.SupplierParty.postalAddress.CitySubdivisionName = textBox13.Text; // اسم المنطقة او الحى 
//            //SA for Saudi it must be SA with seller data
//            inv.SupplierParty.postalAddress.country.IdentificationCode = "SA";
//            // seller company name
//            inv.SupplierParty.partyLegalEntity.RegistrationName = textBox10.Text; // اسم الشركة المسجل فى الهيئة
//            //seller vat registration number must be 15 digits and start with 3 and end with 3
//            inv.SupplierParty.partyTaxScheme.CompanyID = textBox9.Text;  // رقم التسجيل الضريبي

//            //if (inv.invoiceTypeCode.Name == "0100000")
//            //{
//            // بيانات المشترى
//            inv.CustomerParty.partyIdentification.ID = textBox21.Text; // رقم السجل التجارى الخاص بالمشترى
//            inv.CustomerParty.partyIdentification.schemeID = cmb_buyer_scheme.SelectedValue.ToString(); //رقم السجل التجارى
//            inv.CustomerParty.postalAddress.StreetName = textBox18.Text; // اجبارى
//            //inv.CustomerParty.postalAddress.AdditionalStreetName = "street name"; // اختيارى
//            inv.CustomerParty.postalAddress.BuildingNumber = textBox17.Text; // اجبارى رقم المبنى
//            // inv.CustomerParty.postalAddress.PlotIdentification = "9833"; // اختيارى رقم القطعة
//            inv.CustomerParty.postalAddress.CityName = "Jeddah"; // اسم المدينة
//            inv.CustomerParty.postalAddress.PostalZone = textBox15.Text; // الرقم البريدي
//            //inv.CustomerParty.postalAddress.CountrySubentity = "Makkah"; // اسم المحافظة او المدينة مثال (مكة) اختيارى
//            inv.CustomerParty.postalAddress.CitySubdivisionName = textBox16.Text; // اسم المنطقة او الحى 
//            inv.CustomerParty.postalAddress.country.IdentificationCode = "SA";
//            inv.CustomerParty.partyLegalEntity.RegistrationName = textBox19.Text; // اسم الشركة المسجل فى الهيئة
//            inv.CustomerParty.partyTaxScheme.CompanyID = textBox20.Text; // رقم التسجيل الضريبي
//            //}
//            //inv.CustomerParty.contact.Name = "Amr Sobhy";  
//            //inv.CustomerParty.contact.Telephone = "0555252";
//            //inv.CustomerParty.contact.ElectronicMail = "amr@amr.com";
//            //inv.CustomerParty.contact.Note = "notes other notes";
//            decimal invoicediscount = 0;
//            Decimal.TryParse(TextBox2.Text, out invoicediscount);
//            if (invoicediscount > 0)
//            {
//                //this code incase of there is a discount in invoice level 
//                AllowanceCharge allowance = new AllowanceCharge();
//                //ChargeIndicator = false means that this is discount
//                //ChargeIndicator = true means that this is charges(like cleaning service - transportation)
//                allowance.ChargeIndicator = false;// يعني خصم وليس رسوم
//                //write this lines in case you will make discount as percentage
//                allowance.MultiplierFactorNumeric = 0; //dscount percentage like 10
//                allowance.BaseAmount = 0; // the amount we will apply percentage on example (MultiplierFactorNumeric=10 ,BaseAmount=1000 then AllowanceAmount will be 100 SAR)

//                // in case we will make discount as Amount 
//                allowance.Amount = invoicediscount; // 
//                allowance.AllowanceChargeReasonCode = ""; //discount or charge reason code
//                allowance.AllowanceChargeReason = "discount"; //discount or charge reson
//                allowance.taxCategory.ID = "S";// كود الضريبة tax code (S Z O E )
//                allowance.taxCategory.Percent = 15;// نسبة الضريبة tax percentage (0 - 15 - 5 )
//                //فى حالة عندى اكثر من خصم بعمل loop على الاسطر السابقة
//                inv.allowanceCharges.Add(allowance);
//            }

//            decimal payableamount = 0;
//            Decimal.TryParse(textBox23.Text, out payableamount);
//            //this is the invoice total amount (invoice total with vat) and you can set its value with Zero and i will calculate it from sdk
//            inv.legalMonetaryTotal.PayableAmount = payableamount;
//            // فى حالة فى اكتر من منتج فى الفاتورة هانعمل ليست من invoiceline مثال الكود التالى
//            //here we will mention all invoice lines data
//            foreach (InvoiceItems item in invlines)
//            {
//                InvoiceLine invline = new InvoiceLine();
//                //Product Quantity
//                invline.InvoiceQuantity = item.ProductQuantity;
//                //Product Name
//                invline.item.Name = item.ProductName;

//                //if (item.VatPercentage == 0)
//                //{
//                //    //item Tax code
//                //    invline.item.classifiedTaxCategory.ID = "Z"; // كود الضريبة
//                //    //item Tax code
//                //    invline.taxTotal.TaxSubtotal.taxCategory.ID = "Z"; // كود الضريبة
//                //     //item Tax Exemption Reason Code mentioned in zatca pdf page(32-33)
//                //    invline.taxTotal.TaxSubtotal.taxCategory.TaxExemptionReasonCode = "VATEX-SA-HEA"; // كود الضريبة
//                //     //item Tax Exemption Reason mentioned in zatca pdf page(32-33)
//                //    invline.taxTotal.TaxSubtotal.taxCategory.TaxExemptionReason = "Private healthcare to citizen"; // كود الضريبة

//                //}
//                //else
//                //{
//                //item Tax code
//                invline.item.classifiedTaxCategory.ID = "S"; // كود الضريبة
//                //item Tax code
//                invline.taxTotal.TaxSubtotal.taxCategory.ID = "S"; // كود الضريبة
//                // }
//                //item Tax percentage
//                invline.item.classifiedTaxCategory.Percent = item.VatPercentage; // نسبة الضريبة
//                invline.taxTotal.TaxSubtotal.taxCategory.Percent = item.VatPercentage; // نسبة الضريبة
//                //EncludingVat = false this flag will be false in case you will give me Product Price not including vat
//                //EncludingVat = true this flag will be true in case you will give me Product Price including vat
//                invline.price.EncludingVat = false;
//                //Product Price
//                invline.price.PriceAmount = item.ProductPrice;

//                if (item.DiscountValue > 0)
//                {
//                    // incase there is discount in invoice line level
//                    AllowanceCharge allowanceCharge = new AllowanceCharge();
//                    // فى حالة الرسوم incase of charges
//                    // allowanceCharge.ChargeIndicator = true;
//                    // فى حالة الخصم incase of discount
//                    allowanceCharge.ChargeIndicator = false;

//                    allowanceCharge.AllowanceChargeReason = "discount"; // سبب الخصم على مستوى المنتج
//                    // allowanceCharge.AllowanceChargeReasonCode = "90"; // سبب الخصم على مستوى المنتج
//                    allowanceCharge.Amount = item.DiscountValue; // قيم الخصم discount amount or charge amount

//                    allowanceCharge.MultiplierFactorNumeric = 0;
//                    allowanceCharge.BaseAmount = 0;
//                    invline.allowanceCharges.Add(allowanceCharge);
//                }
//                inv.InvoiceLines.Add(invline);
//            }
//            //this calculation just for test invoice calculation and you may don't need this lines
//            //start
//            InvoiceTotal invoiceTotal = ubl.CalculateInvoiceTotal(inv.InvoiceLines, inv.allowanceCharges);
//            TextBox1.Text = invoiceTotal.LineExtensionAmount.ToString("0.00");
//            TextBox3.Text = invoiceTotal.TaxExclusiveAmount.ToString("0.00");
//            TextBox6.Text = invoiceTotal.TaxInclusiveAmount.ToString("0.00");
//            TextBox5.Text = (invoiceTotal.TaxInclusiveAmount - invoiceTotal.TaxExclusiveAmount).ToString("0.00");
//            //end

//            // here you can pass csid data
//            //this is csid or publickey
//            //inv.cSIDInfo.CertPem = @"MIIFADCCBKWgAwIBAgITbQAAGw/UXgsmTms9LgABAAAbDzAKBggqhkjOPQQDAjBiMRUwEwYKCZImiZPyLGQBGRYFbG9jYWwxEzARBgoJkiaJk/IsZAEZFgNnb3YxFzAVBgoJkiaJk/IsZAEZFgdleHRnYXp0MRswGQYDVQQDExJQRVpFSU5WT0lDRVNDQTItQ0EwHhcNMjMwOTIxMDgxODAyWhcNMjUwOTIxMDgyODAyWjBcMQswCQYDVQQGEwJTQTEMMAoGA1UEChMDVFNUMRYwFAYDVQQLEw1SaXlhZGggQnJhbmNoMScwJQYDVQQDEx5UU1QtMjA1MDAxMjA5NS0zMDAwMDAxMzUyMjAwMDMwVjAQBgcqhkjOPQIBBgUrgQQACgNCAASbUK/x5nG7tMATY9Z/u60/eKzfGtdM2WbAFe654OPM1Fb1aBj/JEqgSp5dJQtuahldiKPfJ8aCH8I1tN0cbRxBo4IDQTCCAz0wJwYJKwYBBAGCNxUKBBowGDAKBggrBgEFBQcDAjAKBggrBgEFBQcDAzA8BgkrBgEEAYI3FQcELzAtBiUrBgEEAYI3FQiBhqgdhND7EobtnSSHzvsZ08BVZoGc2C2D5cVdAgFkAgETMIHNBggrBgEFBQcBAQSBwDCBvTCBugYIKwYBBQUHMAKGga1sZGFwOi8vL0NOPVBFWkVJTlZPSUNFU0NBMi1DQSxDTj1BSUEsQ049UHVibGljJTIwS2V5JTIwU2VydmljZXMsQ049U2VydmljZXMsQ049Q29uZmlndXJhdGlvbixEQz1leHRnYXp0LERDPWdvdixEQz1sb2NhbD9jQUNlcnRpZmljYXRlP2Jhc2U/b2JqZWN0Q2xhc3M9Y2VydGlmaWNhdGlvbkF1dGhvcml0eTAdBgNVHQ4EFgQU6PKLogVxfkECr0gYpM0CSaBn1m8wDgYDVR0PAQH/BAQDAgeAMIGtBgNVHREEgaUwgaKkgZ8wgZwxOzA5BgNVBAQMMjEtVFNUfDItVFNUfDMtOTVjNjRhZjgtYTI4NS00ZGFlLTg4MDMtYWYwNzNhZmU4ZjBkMR8wHQYKCZImiZPyLGQBAQwPMzAwMDAwMTM1MjIwMDAzMQ0wCwYDVQQMDAQxMTAwMQ4wDAYDVQQaDAVNYWtrYTEdMBsGA1UEDwwUTWVkaWNhbCBMYWJvcmF0b3JpZXMwgeQGA1UdHwSB3DCB2TCB1qCB06CB0IaBzWxkYXA6Ly8vQ049UEVaRUlOVk9JQ0VTQ0EyLUNBKDEpLENOPVBFWkVpbnZvaWNlc2NhMixDTj1DRFAsQ049UHVibGljJTIwS2V5JTIwU2VydmljZXMsQ049U2VydmljZXMsQ049Q29uZmlndXJhdGlvbixEQz1leHRnYXp0LERDPWdvdixEQz1sb2NhbD9jZXJ0aWZpY2F0ZVJldm9jYXRpb25MaXN0P2Jhc2U/b2JqZWN0Q2xhc3M9Y1JMRGlzdHJpYnV0aW9uUG9pbnQwHwYDVR0jBBgwFoAUgfKje3J7vVCjap/x6NON1nuccLUwHQYDVR0lBBYwFAYIKwYBBQUHAwIGCCsGAQUFBwMDMAoGCCqGSM49BAMCA0kAMEYCIQD52GbWVIWpbdu7B4BnDe+fIKlrAxRUjnGtcc8HiKCEDAIhAJqHLuv0Krp5+HiNCB6w5VPXBPhTKbKidRkZHeb2VTJ+";
//            //inv.cSIDInfo.PrivateKey = @"MHQCAQEEIFMxGrBBfmGxmv3rAmuAKgGrqnyNQYAfKqr7OVKDzgDYoAcGBSuBBAAKoUQDQgAEm1Cv8eZxu7TAE2PWf7utP3is3xrXTNlmwBXuueDjzNRW9WgY/yRKoEqeXSULbmoZXYij3yfGgh/CNbTdHG0cQQ==";
//            //string secretkey = "lHntHtEGWi+ZJtssv167Dy+R64uxf/PTMXg3CEGYfvM=";
//            CSIDInfo info = MainSettings.GetCSIDInfo();
//            //this keys is CSID information you can generate it from AutoGenerateCSR form
//            //this keys you will generate it one time and it will be valid in simulation for 2 years - for production 5 years

//            inv.cSIDInfo.CertPem = info.PublicKey;
//            inv.cSIDInfo.PrivateKey = info.PrivateKey;
//            string secretkey = info.SecretKey;
//            try
//            {
//                //string g=Guid.NewGuid().ToString();
//                //if you need to save xml file true if not false;
//                bool savexmlfile = true;
//                // this method is used to generate xml file with invoice data 
//                //Directory.GetCurrentDirectory() or Directory that you need to save xml file 
//                res = ubl.GenerateInvoiceXML(inv, Directory.GetCurrentDirectory(), savexmlfile);
//                // res.IsValid must equal true to be ready to send to zatca api
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(ex.Message.ToString() + "\n\n" + ex.InnerException.ToString());
//            }
//            //
//            if (!res.IsValid)
//            {
//                //
//                MessageBox.Show(res.ErrorMessage);
//                return;
//            }
//            ////Sending modes
//            //// developer mode (for developers only)
//            //if (rdb_simulation.Checked)
//            //    mode = Mode.Simulation; //simulation mode (for test)
//            //else if (rdb_production.Checked)
//            //    mode = Mode.Production;//production mode for live
//            //else
//            //    mode = Mode.developer;

//            // zatca call api
//            ApiRequestLogic apireqlogic = new ApiRequestLogic(mode, Directory.GetCurrentDirectory(), true);

//            InvoiceReportingRequest invrequestbody = new InvoiceReportingRequest();
//            invrequestbody.invoice = res.EncodedInvoice;
//            invrequestbody.invoiceHash = res.InvoiceHash;
//            invrequestbody.uuid = res.UUID;
//            //if (rdb_complaice.Checked)
//            if (1==1)
//            {
//                ComplianceCsrResponse tokenresponse = new ComplianceCsrResponse();
//                string csr = @"-----BEGIN CERTIFICATE REQUEST-----
//MIIB5DCCAYoCAQAwVTELMAkGA1UEBhMCU0ExFjAUBgNVBAsMDUVuZ2F6YXRCcmFu
//Y2gxEDAOBgNVBAoMB0VuZ2F6YXQxHDAaBgNVBAMME1RTVC0zMDAzMDA4Njg2MDAw
//MDMwVjAQBgcqhkjOPQIBBgUrgQQACgNCAARYvqwxwBzinhARQZYQnWBoSr8wMmmw
//CdfTSleD+rZoh/NeJMF8reXaBFrMCrlPK0hTRXmCyXuc6nFUfjSvZU/goIHVMIHS
//BgkqhkiG9w0BCQ4xgcQwgcEwIgYJKwYBBAGCNxQCBBUTE1RTVFpBVENBQ29kZVNp
//Z25pbmcwgZoGA1UdEQSBkjCBj6SBjDCBiTE7MDkGA1UEBAwyMS1UU1R8Mi1UU1R8
//My1lZDIyZjFkOC1lNmEyLTExMTgtOWI1OC1kOWE4ZjExZTQ0NWYxHzAdBgoJkiaJ
//k/IsZAEBDA8zMDAzMDA4Njg2MDAwMDMxDTALBgNVBAwMBDExMDAxDDAKBgNVBBoM
//A1RTVDEMMAoGA1UEDwwDVFNUMAoGCCqGSM49BAMCA0gAMEUCIQDRroaukEGwwRXW
//RhOudGrd/OGrcUnnn2ftb6Jk4dDGFgIgaV+sXmaZlKbxR7k/lMhnf/2j95XHDkso
//hup1ROPc+cc=
//-----END CERTIFICATE REQUEST-----
//";
//                tokenresponse = apireqlogic.GetComplianceCSIDAPI("12345", csr);
//                if (String.IsNullOrEmpty(tokenresponse.ErrorMessage))
//                {
//                    InvoiceReportingResponse responsemodel = apireqlogic.CallComplianceInvoiceAPI(tokenresponse.BinarySecurityToken, tokenresponse.Secret, invrequestbody);
//                    if (responsemodel.IsSuccess)
//                    {
//                        if (responsemodel.StatusCode == 202)
//                        {
//                            //save warning message in database to solve for next invoices
//                            //responsemodel.WarningMessage
//                        }
//                        MessageBox.Show(responsemodel.ReportingStatus + responsemodel.ClearanceStatus); //REPORTED
//                       // PictureBox1.Image = QrCodeImage(res.QRCode, 200, 200);

//                    }
//                    else
//                    {
//                        MessageBox.Show(responsemodel.ErrorMessage);
//                    }
//                }
//                else
//                {
//                    MessageBox.Show(tokenresponse.ErrorMessage);
//                }
//            }
//            else
//            {

//                //this code is for simulation and production mode

//                if (inv.invoiceTypeCode.Name.Substring(0, 2) == "01")
//                {
//                    // to send standard invoices for clearing
//                    //this this the calling of api 
//                    InvoiceClearanceResponse responsemodel = apireqlogic.CallClearanceAPI(Utility.ToBase64Encode(inv.cSIDInfo.CertPem), secretkey, invrequestbody);
//                    //if responsemodel.IsSuccess = true this means that your xml is successfully sent to zatca 
//                    if (responsemodel.IsSuccess)
//                    {
//                        ///////////
//                        //if status code =202 it means that xml accepted but with warning 
//                        //no need to sent xml again but you must solve that warning messages for the next invoices
//                        if (responsemodel.StatusCode == 202)
//                        {
//                            //save warning message in database to solve for next invoices
//                            //responsemodel.WarningMessage
//                        }
//                        MessageBox.Show(responsemodel.ClearanceStatus); // Cleared
//                        MessageBox.Show(responsemodel.QRCode);
//                       // PictureBox1.Image = QrCodeImage(responsemodel.QRCode);
//                    }
//                    else
//                    {
//                        MessageBox.Show(responsemodel.ErrorMessage);
//                    }
//                }
//                else
//                {
//                    //to send simplified invoices for reporting
//                    //this this the calling of api 
//                    InvoiceReportingResponse responsemodel = apireqlogic.CallReportingAPI(Utility.ToBase64Encode(inv.cSIDInfo.CertPem), secretkey, invrequestbody);

//                    if (responsemodel.IsSuccess)
//                    {
//                        //if status code =202 it means that xml accespted but with warning 
//                        //no need to sent xml again but you must solve that warning messages for the next invoices
//                        if (responsemodel.StatusCode == 202)
//                        {
//                            //save warning message in database to solve for next invoices
//                            //responsemodel.WarningMessage
//                        }
//                        MessageBox.Show(responsemodel.ReportingStatus);// Reported
//                        PictureBox1.Image = QrCodeImage(res.QRCode);
//                    }
//                    else
//                        MessageBox.Show(responsemodel.ErrorMessage);
//                }


//            }
        }

        public void button7_Click(object sender, EventArgs e)
        {
            try
            {
                string PublicKey = string.IsNullOrEmpty(BL.CLS_Session.z_certpem) ? File.ReadAllText(Directory.GetCurrentDirectory() + "\\cert\\cert.pem") : BL.CLS_Session.z_certpem;
                string PrivateKey = string.IsNullOrEmpty(BL.CLS_Session.z_keypem) ?File.ReadAllText(Directory.GetCurrentDirectory() + "\\cert\\key.pem"): BL.CLS_Session.z_keypem;
                string SecretKey = string.IsNullOrEmpty(BL.CLS_Session.z_secrettxt) ? File.ReadAllText(Directory.GetCurrentDirectory() + "\\cert\\secret.txt") : BL.CLS_Session.z_secrettxt;

                DataTable dthtosnd = daml.SELECT_QUIRY_only_retrn_dt("select * from pos_esend where (zatka_status is null or zatka_status=0) "+ (string.IsNullOrEmpty(allll)? "" : " and contr <> 0 " ) +" ");
               // DataTable dthtosnd = daml.SELECT_QUIRY_only_retrn_dt("select * from pos_esend where status_code<>200");
                progressBar1.Value = 0;
                progressBar1.Minimum = 0;
                progressBar1.Maximum = dthtosnd.Rows.Count;
                progressBar1.Visible = true;

                foreach (DataRow dtrh in dthtosnd.Rows)
                {
                    ApiRequestLogic apireqlogic = new ApiRequestLogic(BL.CLS_Session.is_production? Mode.Production : Mode.Simulation, Directory.GetCurrentDirectory(), true);

                    InvoiceReportingRequest invrequestbody = new InvoiceReportingRequest();
                    invrequestbody.invoice = dtrh[9].ToString(); //res.EncodedInvoice;
                    invrequestbody.invoiceHash = dtrh[6].ToString();// res.InvoiceHash;
                    invrequestbody.uuid = dtrh[5].ToString();// res.UUID;

                    //this code is for simulation and production mode

                    // if (inv.invoiceTypeCode.Name.Substring(0, 2) == "01")
                    if (Convert.ToInt32(dtrh[3])==0)
                    {
                        // to send standard invoices for clearing
                        //this this the calling of api 
                        InvoiceClearanceResponse responsemodel = apireqlogic.CallClearanceAPI(Utility.ToBase64Encode(PublicKey), SecretKey, invrequestbody);
                        //if responsemodel.IsSuccess = true this means that your xml is successfully sent to zatca 
                        if (responsemodel.IsSuccess)
                        {
                            ///////////
                            //if status code =202 it means that xml accepted but with warning 
                            //no need to sent xml again but you must solve that warning messages for the next invoices
                            if (responsemodel.StatusCode == 202)
                            {
                                //save warning message in database to solve for next invoices
                                //responsemodel.WarningMessage
                            }
                           // responsemodel.QRCode;
                           // responsemodel.ClearedInvoice;
                            daml.SELECT_QUIRY_only_retrn_dt("update pos_esend  set status_code = " + responsemodel.StatusCode + ", zatka_status=1 ,wrning_msg='" + responsemodel.WarningMessage + "',zatka_err_msg='" + responsemodel.ErrorMessage + "',clrnce_status='" + responsemodel.ClearanceStatus + "',snd_date=getdate(),cleard_inv='" + responsemodel.ClearedInvoice + "',qr_code='" + responsemodel.QRCode + "'  where ref=" + dtrh[2].ToString() + "");
                            ////MessageBox.Show(responsemodel.ClearanceStatus); // Cleared
                            ////MessageBox.Show(responsemodel.QRCode);
                            // PictureBox1.Image = QrCodeImage(responsemodel.QRCode);
                        }
                        else
                        {
                           // MessageBox.Show(responsemodel.ErrorMessage);
                            daml.SELECT_QUIRY_only_retrn_dt("update pos_esend  set status_code = " + responsemodel.StatusCode + ", zatka_status=0 ,wrning_msg='" + responsemodel.WarningMessage + "',zatka_err_msg='" + responsemodel.ErrorMessage + "',clrnce_status='" + responsemodel.ClearanceStatus + "'  where ref=" + dtrh[2].ToString() + "");
                        }
                    }
                    else
                    {
                        //to send simplified invoices for reporting
                        //this this the calling of api 
                        InvoiceReportingResponse responsemodel = apireqlogic.CallReportingAPI(Utility.ToBase64Encode(PublicKey), SecretKey, invrequestbody);

                        if (responsemodel.IsSuccess)
                        {
                            //if status code =202 it means that xml accespted but with warning 
                            //no need to sent xml again but you must solve that warning messages for the next invoices
                            if (responsemodel.StatusCode == 202)
                            {
                                //save warning message in database to solve for next invoices
                                //responsemodel.WarningMessage
                                //  daml.SELECT_QUIRY_only_retrn_dt("update pos_esend  set status_code = " + responsemodel.StatusCode + ", zatka_status=0 ,wrning_msg='" + responsemodel.WarningMessage + "',zatka_err_msg='" + responsemodel .ErrorMessage+ "'  where ref=" + dtrh[2].ToString() + "");
                            }
                            // MessageBox.Show(responsemodel.ReportingStatus);// Reported
                            // PictureBox1.Image = QrCodeImage(res.QRCode);

                            daml.SELECT_QUIRY_only_retrn_dt("update pos_esend  set status_code = " + responsemodel.StatusCode + ", zatka_status=1 ,wrning_msg='" + responsemodel.WarningMessage + "',zatka_err_msg='" + responsemodel.ErrorMessage + "',rport_status='" + responsemodel.ReportingStatus + "',snd_date=getdate()  where ref=" + dtrh[2].ToString() + "");
                        }
                        else

                            daml.SELECT_QUIRY_only_retrn_dt("update pos_esend  set status_code = " + responsemodel.StatusCode + ", zatka_status=0 ,wrning_msg='" + responsemodel.WarningMessage + "',zatka_err_msg='" + responsemodel.ErrorMessage + "',rport_status='" + responsemodel.ReportingStatus + "'  where ref=" + dtrh[2].ToString() + "");
                        // MessageBox.Show(responsemodel.ErrorMessage);
                    }
                    progressBar1.Increment(1);
                }
                //if (!string.IsNullOrEmpty(allll))
                MessageBox.Show("تم مزامنة الفواتير مع الزكاة والدخل بنجاح", "Erroe", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                

                progressBar1.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erroe", MessageBoxButtons.OK, MessageBoxIcon.Error);
                progressBar1.Visible = false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox2.Checked)
                checkBox3.Checked=false;
            
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
                checkBox2.Checked = false;
        }

        private void SalesReport_ToSend_Click(object sender, EventArgs e)
        {
            countt = countt + 1;

            if (countt == 10)
                button4.Visible = true;
        }
    }
}
