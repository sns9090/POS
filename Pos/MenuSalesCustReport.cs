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
//using CrystalDecisions.CrystalReports.Engine;
using System.Drawing.Printing;

namespace POS.Pos
{
    public partial class MenuSalesCustReport : BaseForm
    {
        List<BL.CurrencyInfo> currencies = new List<BL.CurrencyInfo>();
        BL.DAML daml = new BL.DAML();
        BL.Date_Validate datval = new BL.Date_Validate();
        DataTable dtt, dtslctr;
        public static int qq;
        string typeno = "", printer_nam;
        SqlConnection con3 = BL.DAML.con; //new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        public MenuSalesCustReport()
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
                if (string.IsNullOrEmpty(txt_code.Text))
                {
                    MessageBox.Show(BL.CLS_Session.lang.Equals("E") ? "Enter Item NO" : "يجب ادخال رقم الصنف", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txt_code.Focus();
                    return;

                }

                SqlDataAdapter darown = new SqlDataAdapter("select cu_name a_name,round(cu_opnbal,2) a_opnbal,cu_no a_key  from customers where cu_no='" + txt_code.Text + "' and cu_brno='"+BL.CLS_Session.brno+"'", con3);
                // darown.Fill(dth);
                DataTable dtn = new DataTable();
                darown.Fill(dtn);
                if (dtn.Rows.Count > 0)
                {
                    txt_name.Text = dtn.Rows[0][0].ToString();
                    txt_code.Text = dtn.Rows[0][2].ToString();
                   // txt_opnbal.Text = dtn.Rows[0][1].ToString();
                   // dth = dtn;
                }

                else
                {
                    //    MessageBox.Show("الحساب غير موجود");
                    txt_name.Text = "";
                    txt_code.Text = "";
                    //  txt_code.Text = dt.Rows[0][2].ToString();
                  //  txt_opnbal.Text = "0";
                }
                //if (typeno == "")
                //{
                string cndtyp = comboBox2.SelectedIndex == 1 ? " and a.type=1 " : comboBox2.SelectedIndex == 2 ? " and a.type=0 " : " ";
                string conds = cmb_salctr.SelectedIndex != -1 ? " and a.slno='" + cmb_salctr.SelectedValue + "' " : " ";
                string condt = string.IsNullOrEmpty(txt_contr.Text) ? " " : " and a.contr=" + txt_contr.Text + " ";
                string condam = string.IsNullOrEmpty(txt_salman.Text) ? " " : " and a.saleman=" + txt_salman.Text + " ";
              //  string condcncl = checkBox1.Checked ? " d_pos_hdr " : " pos_hdr ";
              //  string condcncl = checkBox1.Checked ? " d_pos_hdr " : " pos_hdr ";
                    string xxx;
                    xxx = maskedTextBox1.Text.ToString();

                    string zzz;
                    zzz = maskedTextBox2.Text.ToString();
                    //SqlDataAdapter da3 = new SqlDataAdapter("select a_type a,a_ref b,a_mdate c,a_hdate,a_text d,a_amt e  where date between '20180101' and '20190101'", con3);
                    //SqlDataAdapter da4 = new SqlDataAdapter("select sum(total),sum(total_cost),sum(discount),sum(net_total) from pos_hdr where date between'" + xxx + "' and '" + zzz + "'", con3);
                  //  SqlDataAdapter da3 = new SqlDataAdapter("select contr ,case [type] when 1 then 'نقدي' when 0 then 'مرتجع' end as [type],ref,(CONVERT(varchar, date, 25)) [date] ,[total] ,[count] ,[payed],[total_cost],[saleman] ,[req_no] ,[cust_no],[discount],[net_total] from "+condcncl+" where date between'" + xxx + "' and '" + zzz + "' " + conds + condt + condam + "", con3);
                    SqlDataAdapter da3 = new SqlDataAdapter("select a.contr ,case a.[type] when 1 then 'بيع نقدي' when 0 then 'مرتجع نقدي' when 2 then 'مرتجع اجل' when 3 then 'بيع اجل' end as [type],a.ref,(CONVERT(varchar, a.date, 25)) [date],round(a.[count],2)  count,round((case a.type when 1 then a.total  when 0 then -a.total end),2) [total],round( (case a.type when 1 then a.discount  when 0 then -a.discount end),2) [discount],round(((case a.type when 1 then a.net_total  when 0 then -a.net_total end) - (case a.type when 1 then a.tax_amt  when 0 then -a.tax_amt end)),2) net1,round((case a.type when 1 then a.tax_amt  when 0 then -a.tax_amt end),2) tax ,round((case a.type when 1 then a.net_total  when 0 then -a.net_total end),2) [net_total],round(a.[payed],2) payed,round((case a.type when 1 then a.total_cost  when 0 then -a.total_cost end),2) [total_cost],a.[saleman]  from pos_hdr a join pos_dtl b on a.[brno]=b.[brno] and a.[slno]=b.[slno] and a.[ref]=b.[ref] and a.[contr]=b.[contr] and a.[type]=b.[type] and a.brno='" + BL.CLS_Session.brno + "' where date between'" + datval.convert_to_yyyy_MMddwith_dash(xxx) + " 05:00:00.000' and '" + datval.convert_to_yyyy_MMddwith_dash(zzz) + " 05:00:00.000' " + conds + condt + condam + cndtyp + " and a.[cust_no]='" + txt_code.Text + "' group by a.contr,a.type,a.ref,a.date,a.count,a.total,a.discount,a.net_total,a.tax_amt,a.payed,a.total_cost,a.saleman", con3);
                
                //  SqlDataAdapter da3 = new SqlDataAdapter("select * from hdr where date like'%" + xxx + "%'", con3);
                    // SqlDataAdapter da4 = new SqlDataAdapter("select sum(total) from hdr where date between'" + xxx + "' and '" + yyy + "'", con3);
                    SqlDataAdapter da4 = new SqlDataAdapter("select sum(case type when 1 then total  when 0 then -total end),sum(case type when 1 then total_cost  when 0 then -total_cost end),sum(case type when 1 then discount  when 0 then -discount end),sum(case type when 1 then net_total  when 0 then -net_total end ),sum(case type when 1 then count  when 0 then -count end ) sumcont from pos_hdr where  brno='" + BL.CLS_Session.brno + "' and date between'" + datval.convert_to_yyyy_MMddwith_dash(xxx) + "' and '" + datval.convert_to_yyyy_MMddwith_dash(zzz) + "' " + conds + condt + condam + "", con3);

                    DataSet ds3 = new DataSet();
                    da3.Fill(ds3, "0");
                    da4.Fill(ds3, "1");

                /*
                    DataRow dr = ds3.Tables["0"].NewRow();
                    double sumopnd = 0, sumopnm = 0, sumtrd = 0, sumtrm = 0, sumttld = 0, sumttlm = 0, sumttld8 = 0, sumttlm9 = 0;
                    foreach (DataRow dtr in ds3.Tables["0"].Rows)
                    {
                       // sumopnd = sumopnd + Convert.ToDouble(dtr[2]);
                       // sumopnm = sumopnm + Convert.ToDouble(dtr[3]);
                        sumtrd = sumtrd + Convert.ToDouble(dtr[4]);
                        sumtrm = sumtrm + Convert.ToDouble(dtr[5]);
                        sumttld = sumttld + Convert.ToDouble(dtr[6]);
                        sumttlm = sumttlm + Convert.ToDouble(dtr[7]);
                        sumttld8 = sumttld8 + Convert.ToDouble(dtr[8]);
                        sumttlm9 = sumttlm9 + Convert.ToDouble(dtr[9]);
                    }
                   // dr[0] = "";
                   // dr[3] = "الاجمالي";
                   // dr[2] = sumopnd;
                   // dr[3] = sumopnm;
                    dr[4] = sumtrd;
                    dr[5] = sumtrm;
                    dr[6] = sumttld;
                    dr[7] = sumttlm;
                    dr[8] = sumttld8;
                    dr[9] = sumttlm9;

                   // ds3.Tables["0"].Rows.Add(dr);
                */

                    dataGridView1.DataSource = ds3.Tables["0"];
                    txt_total.Text = ds3.Tables[1].Rows[0][0].ToString();
                    txt_cost.Text = ds3.Tables[1].Rows[0][1].ToString();
                    txt_cont.Text = ds3.Tables[1].Rows[0]["sumcont"].ToString();

                    textBox1.Text = ds3.Tables[1].Rows[0][2].ToString();
                    textBox2.Text = ds3.Tables[1].Rows[0][3].ToString();

                    dataGridView1.Columns[9].Visible = false;
                    dataGridView1.Columns[10].Visible = false;
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
            currencies.Add(new BL.CurrencyInfo(BL.CurrencyInfo.Currencies.Syria));
            currencies.Add(new BL.CurrencyInfo(BL.CurrencyInfo.Currencies.UAE));
            currencies.Add(new BL.CurrencyInfo(BL.CurrencyInfo.Currencies.SaudiArabia));
            currencies.Add(new BL.CurrencyInfo(BL.CurrencyInfo.Currencies.Tunisia));
            currencies.Add(new BL.CurrencyInfo(BL.CurrencyInfo.Currencies.Gold));
            currencies.Add(new BL.CurrencyInfo(BL.CurrencyInfo.Currencies.Yemen));
            currencies.Add(new BL.CurrencyInfo(BL.CurrencyInfo.Currencies.nul));
            currencies.Add(new BL.CurrencyInfo(BL.CurrencyInfo.Currencies.Kuwait));
            currencies.Add(new BL.CurrencyInfo(BL.CurrencyInfo.Currencies.Qatar));

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
            maskedTextBox1.Text = DateTime.Now.ToString("01-MM-yyyy 05:00:00.000", new CultureInfo("en-US"));
            maskedTextBox2.Text = DateTime.Now.AddDays(1).ToString("dd-MM-yyyy 05:00:00.000", new CultureInfo("en-US"));

            txt_code.Select();
            PrinterSettings settings = new PrinterSettings();
            printer_nam = settings.PrinterName;
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
        {
            /*
            Reports.Report_Form rrf = new Reports.Report_Form("DR", dtt);
            rrf.MdiParent = ParentForm;
            rrf.Show();
             * */
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
                parameters[4] = new ReportParameter("title", txt_name.Text);

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
                    int toprt = Convert.ToInt32(dataGridView1.CurrentRow.Cells[2].Value.ToString());
                    int toprtc = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                    string tbld = checkBox1.Checked ? "d_pos_dtl" : "pos_dtl";
                    string tblh = checkBox1.Checked ? "d_pos_hdr" : "pos_hdr";
                    SqlDataAdapter dacr = new SqlDataAdapter("select [brno],[slno],[ref],[contr],[type],[barcode], [name],[unit_name][unit],([price]-([price]*[discpc]/100)) price,[qty],[cost],[is_req],[itemno],[srno],[pkqty],[offr_amt] from " + tbld + " join units on " + tbld + ".[unit]=units.unit_id where ref=" + toprt + " and contr=" + toprtc + "", con3);
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

        private void txt_code_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F8)
                {
                    // var selectedCell = dataGridView1.SelectedCells[0];
                    // do something with selectedCell...
                    Search_All f4 = new Search_All("5", "Cus");
                    f4.ShowDialog();

                    try
                    {

                        txt_code.Text = f4.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                        txt_name.Text = f4.dataGridView1.CurrentRow.Cells[1].Value.ToString();
                        //  dataGridView1.CurrentCell = this.dataGridView1[3, dataGridView1.CurrentRow.Index];

                        /*
                       dataGridView1.CurrentRow.Cells[0].Value = f7.listView2.SelectedItems[0].Text;
                       dataGridView1.CurrentRow.Cells[1].Value = f7.listView2.SelectedItems[0].SubItems[1].Text.Replace("|","");
                       dataGridView1.CurrentCell = this.dataGridView1[3, dataGridView1.CurrentRow.Index];
                        */


                    }
                    catch { }
                }

                if (e.KeyCode == Keys.Enter)
                {

                    button1_Click(sender, e);


                }
                /*
                if (e.KeyCode == Keys.Enter)
                {
                    if (e.KeyCode == Keys.Enter)
                    {

                        button1_Click(sender, e);


                    }
                    // button1_Click( sender,  e);
                    DataTable dta = daml.SELECT_QUIRY_only_retrn_dt("select item_name,round(item_obalance,2) a_opnbal,item_no  from items where item_no='" + txt_code.Text + "'");
                    if (dta.Rows.Count > 0)
                    {
                        txt_name.Text = dta.Rows[0][0].ToString();
                        txt_code.Text = dta.Rows[0][2].ToString();
                        txt_opnbal.Text = dta.Rows[0][1].ToString();
                    }
                    else
                    {
                        //    MessageBox.Show("الحساب غير موجود");
                        txt_name.Text = "";
                        txt_code.Text = "";
                        //  txt_code.Text = dt.Rows[0][2].ToString();
                        txt_opnbal.Text = "0";
                    }

                }
                 * */
            }
            catch { }
        }

        private void txt_code_DoubleClick(object sender, EventArgs e)
        {
            Search_All f4 = new Search_All("2", "Sto");
            f4.ShowDialog();

            try
            {

                txt_code.Text = f4.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                txt_name.Text = f4.dataGridView1.CurrentRow.Cells[1].Value.ToString();
                //  dataGridView1.CurrentCell = this.dataGridView1[3, dataGridView1.CurrentRow.Index];

                /*
               dataGridView1.CurrentRow.Cells[0].Value = f7.listView2.SelectedItems[0].Text;
               dataGridView1.CurrentRow.Cells[1].Value = f7.listView2.SelectedItems[0].SubItems[1].Text.Replace("|","");
               dataGridView1.CurrentCell = this.dataGridView1[3, dataGridView1.CurrentRow.Index];
                */


            }
            catch { }
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
            SqlDataAdapter dacr = new SqlDataAdapter("select [brno],[slno],[ref],[contr],[type],[barcode], [name],[unit_name] [unit],([price]-([price]*[discpc]/100)) price,[qty],[cost],[is_req],[itemno],[srno],[pkqty] from " + tbld + " join units on " + tbld + ".[unit]=units.unit_id where ref=" + dataGridView1.CurrentRow.Cells[2].Value + " and [contr]=" + dataGridView1.CurrentRow.Cells[0].Value + " and  [brno]= '" + BL.CLS_Session.brno + "' ", con3);

            DataSet1 ds = new DataSet1();

            dacr1.Fill(ds, "hdr");
            dacr.Fill(ds, "dtl");

           DataTable dtcust = daml.SELECT_QUIRY_only_retrn_dt("select cu_no ,cu_name ,cl_acc,cu_crlmt,vndr_taxcode,cu_alwcr,c_bulding_no,  c_street, c_site_name, c_city, c_cuntry, c_postal_code, c_ex_postalcode, c_other_id,cu_kind,cu_addrs from customers a join cuclass b on a.cu_class=b.cl_no  where a.inactive=0 and a.cu_brno='" + BL.CLS_Session.brno + "' and cu_no='" + ds.Tables["hdr"].Rows[0]["cust_no"].ToString() + "'");
               
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
    }
}
