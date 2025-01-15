using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Globalization;

namespace POS.Pos
{
    public partial class RE_PRT_FRM : BaseForm
    {
        List<BL.CurrencyInfo> currencies = new List<BL.CurrencyInfo>();

        BL.DAML daml = new BL.DAML();
       // string cmp_name = "";
        SqlConnection con2 = BL.DAML.con; //new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);

        public RE_PRT_FRM()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int toprt = Convert.ToInt32(txt_ref.Text);
            int toprtc = Convert.ToInt32(txt_contr.Text);

            SqlDataAdapter dacr = new SqlDataAdapter("select [brno],[slno],[ref],[contr],[type],[barcode], [name],[unit_name][unit],([price]-([price]*[discpc]/100)) price,[qty],[cost],[is_req],[itemno],[srno],[pkqty],[offr_amt] from pos_dtl join units on pos_dtl.[unit]=units.unit_id where ref=" + toprt + " and contr=" + toprtc + "", con2);
            SqlDataAdapter dacr1 = new SqlDataAdapter("select * from pos_hdr where ref=" + toprt + " and contr=" + toprtc + "", con2);
            SqlDataAdapter chk = new SqlDataAdapter("select sum(isnull(is_req,0)) from pos_dtl where ref=" + toprt + " and contr=" + toprtc + "", con2);
           
            DataSet1 ds = new DataSet1();

            ds.Tables["dtl"].Clear();
            ds.Tables["hdr"].Clear();
            ds.Tables["sum"].Clear();

            dacr.Fill(ds, "dtl");
            dacr1.Fill(ds, "hdr");
            chk.Fill(ds, "sum");
            DataTable dtcust = daml.SELECT_QUIRY_only_retrn_dt("select cu_no ,cu_name ,cl_acc,cu_crlmt,vndr_taxcode,cu_alwcr,c_bulding_no,  c_street, c_site_name, c_city, c_cuntry, c_postal_code, c_ex_postalcode, c_other_id,cu_kind,cu_addrs from customers a join cuclass b on a.cu_class=b.cl_no  where a.inactive=0 and a.cu_brno='" + BL.CLS_Session.brno + "' and cu_no='" + ds.Tables["hdr"].Rows[0]["cust_no"].ToString() + "'");
                
            if(BL.CLS_Session.prnt_type.Equals("1"))
            {
                try
                {
                    //FastReport.Report rpt2 = new FastReport.Report();

                    
                    //rpt2.Load(Environment.CurrentDirectory + @"\reports\Pos_Sale_rpt.frx");

                    // rpt2.SetParameterValue("br_name", BL.CLS_Session.brname);
                     FastReport.Report rpt = new FastReport.Report();

                    if(ds.Tables["hdr"].Rows[0][4].ToString().Equals("1"))
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

                     BL.ToWord toWord = new BL.ToWord(Convert.ToDecimal(ds.Tables["hdr"].Rows[0]["net_total"].ToString()), currencies[Convert.ToInt32(BL.CLS_Session.cur)]);
                     rpt.SetParameterValue("type", ds.Tables["hdr"].Rows[0][4].ToString());
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
            try
            {
            if (txt_ref.Text != "" && txt_contr.Text != "")
            {
              

                //DataSet ds = new DataSet();



                //dacr.Fill(ds, "dtl");
                //dacr1.Fill(ds, "hdr");
                //chk.Fill(ds, "sum");

                // DataTable dtchk = new DataTable();
                //// dtchk.Clear();

                // chk.Fill(dtchk);

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
                   // rpt.SetParameterValue("tax_type", BL.CLS_Session.autotax ? "3" : "0");
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
        }

        private void RE_PRT_FRM_Load(object sender, EventArgs e)
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
           // var lines = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\temp.txt");
         //   cmp_name = BL.CLS_Session.cmp_name;// lines.Skip(4).First().ToString();

        }

        private void txt_contr_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                    txt_ref.Focus();
            }
        }

        private void txt_ref_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.Focus();
            }
        }

    }
}
