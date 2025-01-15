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
//using CrystalDecisions.CrystalReports.Engine;


namespace POS.Pos
{
    public partial class PRT_DIALOG : BaseForm
    {
        SqlConnection con2 =BL.DAML.con;// new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        string cmp_name = "";
        private string refno;

        //Resturant_sales rs = new Resturant_sales();


        public PRT_DIALOG(string refnumber)
        {
            this.refno = refnumber;
            InitializeComponent();

            POS2.Visible = false;
            POS3.Visible = false;
            POS4.Visible = false;
            POS5.Visible = false;

        }

        private void PRT_DIALOG_Load(object sender, EventArgs e)
        {

            ////////SqlDataAdapter daa = new SqlDataAdapter("select max(ref) as eee from hdr", con2);
            //SELECT TOP 1 CustomerName FROM Customers ORDER BY CustomerID DESC;
            //SELECT TOP 1 ref FROM hdr ORDER BY ref DESC

            ////////////SqlDataAdapter daa = new SqlDataAdapter("SELECT TOP 1 ref FROM hdr ORDER BY ref DESC", con2);
            ////////////DataTable dss = new DataTable();
            ////////////daa.Fill(dss);
            ////////////label1.Text = dss.Rows[0][0].ToString();

            label1.Text = refno;

            startup();

            var lines = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\temp.txt");
            
          //  cmp_name = lines.Skip(4).First().ToString();

           
        }
        private void startup()
        {
            string pos2, pos3, pos4, pos5;
            var lines_prt = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\prttype.txt");
            pos2 = lines_prt.Skip(1).First().ToString();
            pos3 = lines_prt.Skip(2).First().ToString();
            pos4 = lines_prt.Skip(3).First().ToString();
            pos5 = lines_prt.Skip(4).First().ToString();

            if (pos2 != "0")
            {
                POS2.Visible = true;
                POS2.Text = pos2;
            }
            if (pos3 != "0")
            {
                POS3.Visible = true;
                POS3.Text = pos3;
            }
            if (pos4 != "0")
            {
                POS4.Visible = true;
                POS4.Text = pos4;
            }
            if (pos5 != "0")
            {
                POS5.Visible = true;
                POS5.Text = pos5;
            }
        }

        private void POS2_Click(object sender, EventArgs e)
        {
            if (label1.Text != "")
            {
                 if (BL.CLS_Session.prnt_type.Equals("0"))
                print_ref("POS2");
            else
                print_ref_fr("POS2");
            //    int toprt = Convert.ToInt32(label1.Text);


            //    SqlDataAdapter dacr = new SqlDataAdapter("select * from sales_dtl where ref=" + toprt, con2);
            //    SqlDataAdapter dacr1 = new SqlDataAdapter("select * from sales_hdr where ref=" + toprt, con2);
            //    SqlDataAdapter chk = new SqlDataAdapter("select sum(isnull(is_req,0)) from sales_dtl where ref=" + toprt, con2);

            //    DataSet1 ds = new DataSet1();

            //    ds.Tables["dtl"].Clear();
            //    ds.Tables["hdr"].Clear();
            //    ds.Tables["sum"].Clear();

            //    dacr.Fill(ds, "dtl");
            //    dacr1.Fill(ds, "hdr");
            //    chk.Fill(ds, "sum");

            //    if (Convert.ToInt32(ds.Tables["sum"].Rows[0][0]) > 0)
            //    {
            //        Pos.rpt.SalesReport_req report = new Pos.rpt.SalesReport_req();

            //        report.SetDataSource(ds);
            //        report.SetParameterValue("Comp_Name", cmp_name);
            //        report.SetParameterValue("msg1", BL.CLS_Session.msg1.ToString());
            //        report.SetParameterValue("msg2", BL.CLS_Session.msg2.ToString());

            //        report.PrintOptions.PrinterName = "POS2";

            //        report.PrintToPrinter(1, false, 0, 0);
            //        // report.PrintToPrinter(0,true, 1, 1);
            //        report.Close();
            //        // report.Dispose();

            //    }
            //    else
            //    {
            //        Pos.rpt.SalesReport report = new Pos.rpt.SalesReport();

            //        report.SetDataSource(ds);
            //        report.SetParameterValue("Comp_Name", cmp_name);
            //        report.SetParameterValue("msg1", BL.CLS_Session.msg1.ToString());
            //        report.SetParameterValue("msg2", BL.CLS_Session.msg2.ToString());

            //        report.PrintOptions.PrinterName = "POS2";

            //        report.PrintToPrinter(1, false, 0, 0);
            //        // report.PrintToPrinter(0,true, 1, 1);
            //        report.Close();
            //    }

               POS2.BackColor = Color.GreenYellow;
               POS2.Text = " تم الطباعة " + POS2.Text;
            }
        }

        private void POS3_Click(object sender, EventArgs e)
        {
            if (label1.Text != "")
            {
               
                 if (BL.CLS_Session.prnt_type.Equals("0"))
                print_ref("POS3");
            else
                print_ref_fr("POS3");
                //int toprt = Convert.ToInt32(label1.Text);


                //SqlDataAdapter dacr = new SqlDataAdapter("select * from sales_dtl where ref=" + toprt, con2);
                //SqlDataAdapter dacr1 = new SqlDataAdapter("select * from sales_hdr where ref=" + toprt, con2);
                //SqlDataAdapter chk = new SqlDataAdapter("select sum(isnull(is_req,0)) from sales_dtl where ref=" + toprt, con2);

                //DataSet1 ds = new DataSet1();

                //ds.Tables["dtl"].Clear();
                //ds.Tables["hdr"].Clear();
                //ds.Tables["sum"].Clear();

                //dacr.Fill(ds, "dtl");
                //dacr1.Fill(ds, "hdr");
                //chk.Fill(ds, "sum");

                //if (Convert.ToInt32(ds.Tables["sum"].Rows[0][0]) > 0)
                //{
                //    Pos.rpt.SalesReport_req report = new Pos.rpt.SalesReport_req();

                //    report.SetDataSource(ds);
                //    report.SetParameterValue("Comp_Name", cmp_name);
                //    report.SetParameterValue("msg1", BL.CLS_Session.msg1.ToString());
                //    report.SetParameterValue("msg2", BL.CLS_Session.msg2.ToString());

                //    report.PrintOptions.PrinterName = "POS3";

                //    report.PrintToPrinter(1, false, 0, 0);
                //    // report.PrintToPrinter(0,true, 1, 1);
                //    report.Close();
                //    // report.Dispose();

                //}
                //else
                //{
                //    Pos.rpt.SalesReport report = new Pos.rpt.SalesReport();

                //    report.SetDataSource(ds);
                //    report.SetParameterValue("Comp_Name", cmp_name);
                //    report.SetParameterValue("msg1", BL.CLS_Session.msg1.ToString());
                //    report.SetParameterValue("msg2", BL.CLS_Session.msg2.ToString());

                //    report.PrintOptions.PrinterName = "POS3";

                //    report.PrintToPrinter(1, false, 0, 0);
                //    // report.PrintToPrinter(0,true, 1, 1);
                //    report.Close();
                //}
                POS3.BackColor = Color.GreenYellow;
                POS3.Text =  " تم الطباعة " + POS3.Text;
            }
        }

        private void POS4_Click(object sender, EventArgs e)
        {
            if (label1.Text != "")
            {

                if (BL.CLS_Session.prnt_type.Equals("0"))
                    print_ref("POS4");
                else
                    print_ref_fr("POS4");
                //int toprt = Convert.ToInt32(label1.Text);


                //SqlDataAdapter dacr = new SqlDataAdapter("select * from sales_dtl where ref=" + toprt, con2);
                //SqlDataAdapter dacr1 = new SqlDataAdapter("select * from sales_hdr where ref=" + toprt, con2);
                //SqlDataAdapter chk = new SqlDataAdapter("select sum(isnull(is_req,0)) from sales_dtl where ref=" + toprt, con2);

                //DataSet1 ds = new DataSet1();

                //ds.Tables["dtl"].Clear();
                //ds.Tables["hdr"].Clear();
                //ds.Tables["sum"].Clear();

                //dacr.Fill(ds, "dtl");
                //dacr1.Fill(ds, "hdr");
                //chk.Fill(ds, "sum");

                //if (Convert.ToInt32(ds.Tables["sum"].Rows[0][0]) > 0)
                //{
                //    Pos.rpt.SalesReport_req report = new Pos.rpt.SalesReport_req();

                //    report.SetDataSource(ds);
                //    report.SetParameterValue("Comp_Name", cmp_name);
                //    report.SetParameterValue("msg1", BL.CLS_Session.msg1.ToString());
                //    report.SetParameterValue("msg2", BL.CLS_Session.msg2.ToString());

                //    report.PrintOptions.PrinterName = "POS4";

                //    report.PrintToPrinter(1, false, 0, 0);
                //    // report.PrintToPrinter(0,true, 1, 1);
                //    report.Close();
                //    // report.Dispose();

                //}
                //else
                //{
                //    Pos.rpt.SalesReport report = new Pos.rpt.SalesReport();

                //    report.SetDataSource(ds);
                //    report.SetParameterValue("Comp_Name", cmp_name);
                //    report.SetParameterValue("msg1", BL.CLS_Session.msg1.ToString());
                //    report.SetParameterValue("msg2", BL.CLS_Session.msg2.ToString());

                //    report.PrintOptions.PrinterName = "POS4";

                //    report.PrintToPrinter(1, false, 0, 0);
                //    // report.PrintToPrinter(0,true, 1, 1);
                //    report.Close();
                //}
                POS4.BackColor = Color.GreenYellow;
                POS4.Text = " تم الطباعة " + POS4.Text;
            }
        }

        private void POS5_Click(object sender, EventArgs e)
        {
            if (label1.Text != "")
            {
                if (BL.CLS_Session.prnt_type.Equals("0"))
                    print_ref("POS5");
                else
                    print_ref_fr("POS5");

                //int toprt = Convert.ToInt32(label1.Text);


                //SqlDataAdapter dacr = new SqlDataAdapter("select * from sales_dtl where ref=" + toprt, con2);
                //SqlDataAdapter dacr1 = new SqlDataAdapter("select * from sales_hdr where ref=" + toprt, con2);
                //SqlDataAdapter chk = new SqlDataAdapter("select sum(isnull(is_req,0)) from sales_dtl where ref=" + toprt, con2);

                //DataSet1 ds = new DataSet1();

                //ds.Tables["dtl"].Clear();
                //ds.Tables["hdr"].Clear();
                //ds.Tables["sum"].Clear();

                //dacr.Fill(ds, "dtl");
                //dacr1.Fill(ds, "hdr");
                //chk.Fill(ds, "sum");

                //if (Convert.ToInt32(ds.Tables["sum"].Rows[0][0]) > 0)
                //{
                //    Pos.rpt.SalesReport_req report = new Pos.rpt.SalesReport_req();

                //    report.SetDataSource(ds);
                //    report.SetParameterValue("Comp_Name", cmp_name);
                //    report.SetParameterValue("msg1", BL.CLS_Session.msg1.ToString());
                //    report.SetParameterValue("msg2", BL.CLS_Session.msg2.ToString());

                //    report.PrintOptions.PrinterName = "POS4";

                //    report.PrintToPrinter(1, false, 0, 0);
                //    // report.PrintToPrinter(0,true, 1, 1);
                //    report.Close();
                //    // report.Dispose();

                //}
                //else
                //{
                //    Pos.rpt.SalesReport report = new Pos.rpt.SalesReport();

                //    report.SetDataSource(ds);
                //    report.SetParameterValue("Comp_Name", cmp_name);
                //    report.SetParameterValue("msg1", BL.CLS_Session.msg1.ToString());
                //    report.SetParameterValue("msg2", BL.CLS_Session.msg2.ToString());

                //    report.PrintOptions.PrinterName = "POS5";

                //    report.PrintToPrinter(1, false, 0, 0);
                //    // report.PrintToPrinter(0,true, 1, 1);
                //    report.Close();
                //}
                POS5.BackColor = Color.GreenYellow;
                POS5.Text =  " تم الطباعة " + POS5.Text;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void print_ref(string prntr)
        {
            if (label1.Text != "")
            {
                int toprt = Convert.ToInt32(label1.Text);


                SqlDataAdapter dacr = new SqlDataAdapter("select * from pos_dtl where ref=" + toprt, con2);
                SqlDataAdapter dacr1 = new SqlDataAdapter("select * from pos_hdr where ref=" + toprt, con2);
                SqlDataAdapter chk = new SqlDataAdapter("select sum(isnull(is_req,0)) from pos_dtl where ref=" + toprt, con2);

                DataSet1 ds = new DataSet1();

                ds.Tables["dtl"].Clear();
                ds.Tables["hdr"].Clear();
                ds.Tables["sum"].Clear();

                dacr.Fill(ds, "dtl");
                dacr1.Fill(ds, "hdr");
                chk.Fill(ds, "sum");

               // label5.Text = (Convert.ToDouble(ds.Tables["hdr"].Rows[0]["net_total"]) - Convert.ToDouble(ds.Tables["hdr"].Rows[0]["net_total"])).ToString() + "   " + "المتبقي للزبون";
                // DataTable dtchk = new DataTable();
                //// dtchk.Clear();

                // chk.Fill(dtchk);

                // var lines = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\temp.txt");
                // string[] lines = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\printers.txt");

                CrystalDecisions.CrystalReports.Engine.ReportDocument report1 = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                if (File.Exists(Directory.GetCurrentDirectory() + @"\reports\SalesReport.rpt"))
                {
                    string filePath = Directory.GetCurrentDirectory() + @"\reports\SalesReport.rpt";
                    report1.Load(filePath);
                    report1.SetDataSource(ds);
                    report1.SetParameterValue("Comp_Name", BL.CLS_Session.cmp_name);
                    report1.SetParameterValue("br_name", BL.CLS_Session.brname);
                    report1.SetParameterValue("msg2", BL.CLS_Session.msg2.ToString());
                    report1.SetParameterValue("msg1", BL.CLS_Session.msg1.ToString());
                    report1.SetParameterValue("taxper", BL.CLS_Session.tax_per.ToString());
                    // Use a tab to indent each line of the file.
                    // Console.WriteLine("\t" + line);
                    
                        report1.PrintOptions.PrinterName = prntr;

                        report1.PrintToPrinter(1, false, 0, 0);
                        // report.PrintToPrinter(0,true, 1, 1);

                   
                    report1.Close();
                }
                else
                {
                    rpt.SalesReport report = new rpt.SalesReport();
                    // report.SetParameterValue("Comp_Name", cmp_name);

                    // report.DataDefinition.FormulaFields["Comp_Name"].Text = " '" + cmp_name + "'";
                    report.SetDataSource(ds);
                    report.SetParameterValue("Comp_Name", BL.CLS_Session.cmp_name);
                    report.SetParameterValue("br_name", BL.CLS_Session.brname);
                    report.SetParameterValue("msg2", BL.CLS_Session.msg2.ToString());
                    report.SetParameterValue("msg1", BL.CLS_Session.msg1.ToString());
                    report.SetParameterValue("taxper", BL.CLS_Session.tax_per.ToString());

                    //  CrystalReport1 report = new CrystalReport1();
                    //  report.SetDataSource(ds);

                    //  report.SetParameterValue("Comp_Name", cmp_name);
                    //    crystalReportViewer1.ReportSource = report;

                    //  crystalReportViewer1.Refresh();
                    /*
                    report.PrintOptions.PrinterName = "pos";

                    report.PrintToPrinter(1, false, 0, 0);
                    // report.PrintToPrinter(0, true, 1, 1);
                    report.Close();
                    // report.Dispose();
                     * */

                   
                        // Use a tab to indent each line of the file.
                        // Console.WriteLine("\t" + line);


                        report.PrintOptions.PrinterName = prntr;

                        report.PrintToPrinter(1, false, 0, 0);
                        // report.PrintToPrinter(0,true, 1, 1);

                        // report.Dispose();
                    
                    report.Close();
                }
                //   crystalReportViewer.ReportSource = reportDocument; 


            }
            else
            {
                MessageBox.Show(this, "لا يوجد فاتورة");
            }


            /*
            BL.CLS_PRINT clsprt = new BL.CLS_PRINT();

            clsprt.Run();
             * */

            /*
            if (lblref.Text == "")
            {
                MessageBox.Show("لا توجد فاتورة للطباعة");
            }
            else
            {
                Run();
            }
             * */

        }

        private void print_ref_fr(string prntr)
        {
            if (string.IsNullOrEmpty(label1.Text))
            {
                MessageBox.Show("لا يوجد فاتورة للطباعة", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // SqlDataAdapter dacr = new SqlDataAdapter("select [brno],[slno],[ref],[contr],[type],[barcode],[name],[unit],([price]-([price]*[discpc]/100)) price,[qty],[cost],[is_req],[itemno],[srno],[pkqty] from pos_dtl where ref=(select max(ref) from pos_dtl)", con2);
            //  string chktyp1 = isocu ? " r_pos_hdr " : " pos_hdr ";
            //  string chktyp2 = isocu ? " r_pos_dtl " : " pos_dtl ";
            // SqlDataAdapter dacr1 = new SqlDataAdapter("select * from pos_hdr where ref=(select max(ref) from pos_hdr)", con2);//" + lblref.Text + ""
            SqlDataAdapter dacr1 = new SqlDataAdapter("select a.[brno],a.[slno],a.[ref],a.[contr],a.[type],FORMAT (a.[date], 'dd-MM-yyyy hh:mm:ss tt', 'en-us') date,a.[total],a.[count],a.[payed],a.[total_cost],b.sl_name [saleman],a.[req_no],a.[cust_no],a.[discount],a.[net_total],a.[sysdate],a.[gen_ref],a.[tax_amt],a.[card_amt],a.[note],a.[mobile] from pos_hdr a join pos_salmen b on a.saleman=b.sl_id where a.ref=" + label1.Text + " and a.[contr]=" + BL.CLS_Session.ctrno + " and  a.[brno]= '" + BL.CLS_Session.brno + "' ", con2);
            SqlDataAdapter dacr = new SqlDataAdapter("select [brno],[slno],[ref],[contr],[type],[barcode],[name],[unit],([price]-([price]*[discpc]/100)) price,[qty],[cost],[is_req],[itemno],[srno],[pkqty],[offr_amt] from pos_dtl where ref=" + label1.Text + " and [contr]=" + BL.CLS_Session.ctrno + " and  [brno]= '" + BL.CLS_Session.brno + "' ", con2);

            DataSet1 ds = new DataSet1();

            dacr1.Fill(ds, "hdr");
            dacr.Fill(ds, "dtl");

            if (ds.Tables["hdr"].Rows.Count == 0)
            {
                MessageBox.Show("لا يوجد فاتورة للطباعة", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            FastReport.Report rpt = new FastReport.Report();

            // if (isocu == false)
            rpt.Load(Environment.CurrentDirectory + @"\reports\Pos_Sale_rpt.frx");
            //  else
            //      rpt.Load(Environment.CurrentDirectory + @"\reports\Pos_Ocu_rpt.frx");
            //  rpt.SetParameterValue("br_name", BL.CLS_Session.brname);
            rpt.SetParameterValue("Comp_Name", BL.CLS_Session.cmp_name);
            rpt.SetParameterValue("Br_Name", BL.CLS_Session.brname);
            //rpt.SetParameterValue("inv_bar", ds.Tables["hdr"].Rows[0][3].ToString() + "-" + ds.Tables["hdr"].Rows[0][2].ToString());
            rpt.SetParameterValue("inv_bar", ds.Tables["hdr"].Rows[0][3].ToString() + "-" + ds.Tables["hdr"].Rows[0][2].ToString());
            rpt.SetParameterValue("Msg1", BL.CLS_Session.msg1.ToString());
            rpt.SetParameterValue("Msg2", BL.CLS_Session.msg2.ToString());
            rpt.SetParameterValue("taxper", BL.CLS_Session.tax_per.ToString());
            rpt.SetParameterValue("tax_type", BL.CLS_Session.autotax ? "3" : BL.CLS_Session.isshamltax.Equals("2") ? "2" : "0");
            rpt.SetParameterValue("tax_id", BL.CLS_Session.tax_no);
            rpt.RegisterData(ds.Tables["hdr"], "pos_hdr");
            rpt.RegisterData(ds.Tables["dtl"], "pos_dtl");
            // rpt.PrintSettings.ShowDialog = false;
            // rpt.PrintSettings.Copies =Convert.ToInt32(txt_count.Text);

            // rpt.Print();


           
                rpt.PrintSettings.Printer = prntr;// "pos";
                rpt.PrintSettings.ShowDialog = false;
                // FastReport.Utils.Config.ReportSettings.ShowProgress = false;
                // rpt.PrintSettings.Copies =Convert.ToInt32(txt_count.Text);
                rpt.Print();
           

        }
    }
}