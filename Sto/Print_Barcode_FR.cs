using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
//using CrystalDecisions.CrystalReports.Engine;
using Microsoft.Reporting.WinForms;
//using System.Drawing.Printing;
using FastReport;
using System.Globalization;

namespace POS.Sto
{
    public partial class Print_Barcode_FR : BaseForm
    {
        string bprinter,ptype,itemno;
        SqlConnection con2 = BL.DAML.con;
        BL.DAML daml = new BL.DAML();
        BL.EncDec endc = new BL.EncDec();
        bool onebyone = false;
       // DataTable dt=null;
        string t1, t2, t3, t4, t5;
        public Print_Barcode_FR(string itm)
        {
            InitializeComponent();
            itemno = itm;
        }

        private void Print_Barcode_Load(object sender, EventArgs e)
        {
            txt_sdate.Text = DateTime.Now.ToString("dd-MM-yyyy", new CultureInfo("en-US", false));
            txt_edate.Text = DateTime.Now.AddYears(1).ToString("dd-MM-yyyy", new CultureInfo("en-US", false));

            if (BL.CLS_Session.islight)
            {
                checkBox1.Visible = false;
                cmb_type.Visible = false;
                txt_no.Visible = false;
                label10.Visible = false;
                label6.Visible = false;
            }
            FastReport.Utils.Config.ReportSettings.ShowProgress = false;

            SqlDataAdapter da = new SqlDataAdapter("select * from server", con2);
            DataTable dt = new DataTable();
            da.Fill(dt);

            t1 = dt.Rows[0][0].ToString();
            t2 = dt.Rows[0][1].ToString();
            t3 = dt.Rows[0][2].ToString();
            t4 = endc.Decrypt(dt.Rows[0][3].ToString(), true);
            t5 = dt.Rows[0][4].ToString();


            cmb_type.DataSource = daml.SELECT_QUIRY_only_retrn_dt("select * from trtypes where tr_no in('06','07')");
            cmb_type.DisplayMember = "tr_name";
            cmb_type.ValueMember = "tr_no";
            cmb_type.SelectedIndex = -1;

            var lines = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\bprinter.txt");
            bprinter = lines.First().ToString();
            ptype = string.IsNullOrEmpty(lines.Skip(1).First().ToString()) ? "1" : lines.Skip(1).First().ToString(); //lines.Skip(1).First().ToString();

            if (ptype.Equals("1"))
                rd_2838.Checked = true;

            if (ptype.Equals("2"))
                rd_2238.Checked = true;

            if (ptype.Equals("3"))
                rd_cstm.Checked = true;

            if (ptype.Equals("4"))
                rd_cstm2.Checked = true;

            numUpDwn1.Value = string.IsNullOrEmpty(lines.Skip(2).First().ToString()) ? 1 : Convert.ToInt32(lines.Skip(2).First().ToString());

            load_data(itemno);
            //txt_count.Focus();
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(txt_count.Text))
            //    txt_count.Text = "1";
        }


        private void load_data(string itx)
        {
            if (!string.IsNullOrEmpty(itx))
            {

                string condi = " where i.item_no='" + itx + "'";
                // select i.item_no , i.item_name,i.item_cost,b.price as item_price,b.barcode as  item_barcode,b.pack,i.item_obalance,i.item_cbalance,i.item_group,i.item_image,i.item_req,t.tax_percent,b.pk_qty pkqty,i.item_tax i_tax,i.item_image img from items i join items_bc b on b.item_no=i.item_no and i.item_no='" + textBox3.Text + "' join taxs t on i.item_tax=t.tax_id", con2);
                SqlDataAdapter da23 = new SqlDataAdapter("select i.item_no , i.item_name,i.item_cost,i.item_price as item_price,i.item_barcode as  item_barcode,i.item_obalance,i.item_cbalance,i.item_group,i.item_image,i.item_req,i.item_tax i_tax,i.item_image img,i.item_ename from items i " + condi + "", con2);
                DataTable dt = new DataTable();
                da23.Fill(dt);

                if (dt.Rows.Count == 1)
                {
                    //txt_name.Text = dt.Rows[0][1].ToString();
                    //txt_oldp.Text = dt.Rows[0][3].ToString();
                    //txt_newp.Select();
                    // txt_itemno.Text = f4.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    txt_itemno.Text = dt.Rows[0]["item_no"].ToString();
                    txt_barcode.Text = dt.Rows[0]["item_barcode"].ToString();
                    txt_name.Text = dt.Rows[0]["item_name"].ToString();
                    txt_ename.Text = dt.Rows[0]["item_ename"].ToString();
                    txt_newp.Text = dt.Rows[0]["item_price"].ToString();
                    txt_count.Focus();
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string[] createText = { bprinter, rd_2838.Checked ? "1" : rd_2238.Checked ? "2" :rd_cstm.Checked? "3":"4" , numUpDwn1.Value.ToString()};
            File.WriteAllLines(Directory.GetCurrentDirectory() + @"\bprinter.txt", createText);

            if (checkBox1.Checked && cmb_type.SelectedIndex != -1 && !string.IsNullOrEmpty(txt_no.Text))
            {
                
                SqlConnection con_dest = new SqlConnection("Data Source=" + t1 + ";Initial Catalog=" + t2 + ";User id=" + t3 + ";Password=" + t4 + ";Connection Timeout=" + t5 + "");
                SqlDataAdapter dads = new SqlDataAdapter("select itemno,(qty*pkqty) qty from pu_dtl where invtype='" + cmb_type.SelectedValue.ToString() + "' and ref=" + txt_no.Text + " and branch='" + BL.CLS_Session.brno + "'  order by folio", con_dest);
                DataTable dtds = new DataTable();
                dads.Fill(dtds);
                if (dtds.Rows.Count==0)
                {
                    MessageBox.Show("قد يكون رقم الحركة اللذي ادخلته غير صحيح", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txt_no.Focus();
                    return;
                }


                if (rd_2838.Checked || rd_2238.Checked)
                {
                    CrystalDecisions.CrystalReports.Engine.ReportDocument report = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                    foreach (DataRow dtr in dtds.Rows)
                    {
                        // ReportDocument report = new ReportDocument();
                        DataTable dtpb = daml.SELECT_QUIRY_only_retrn_dt(@"select  item_no, item_name, item_cost, item_price, item_barcode,unit_name item_unit, item_obalance, item_cbalance, item_group, item_image, item_req, item_tax, unit2, 
                         uq2, unit2p, unit3, uq3, unit3p, unit4, uq4, unit4p, item_ename, item_opencost, item_curcost, supno, note, last_updt, sgroup, price2 from items,units  where item_unit=unit_id and item_no ='" + dtr[0] + "'");


                        string filePath = Directory.GetCurrentDirectory() + @"\reports\BarcodeReports" + (rd_2838.Checked ? "1" : rd_2238.Checked ? "2" : "3") + ".rpt";
                        report.Load(filePath);

                        report.SetDataSource(dtpb);
                        report.SetParameterValue("cmp_name", POS.BL.CLS_Session.cmp_name);
                        // report.ReportDefinition.Areas.

                        // crystalReportViewer1.ReportSource = report;

                        // crystalReportViewer1.Refresh();

                        report.PrintOptions.PrinterName = bprinter;
                        int xxx = onebyone ? 1 : Convert.ToInt32(dtr[1]);



                        //int xxx = Convert.ToInt32(textBox8.Text);
                        //for (int i = 1; i <= xxx; i++)
                        //{
                        // report.PrintToPrinter(1, true, 1, 1);
                        // report.PrintToPrinter(1, false, 0, 0);
                        report.PrintToPrinter(xxx, true, 1, 1);

                        // }
                    }
                    report.Close();
                }
                else
                {
                    if (rd_cstm.Checked)
                    {
                        foreach (DataRow dtr in dtds.Rows)
                        {
                            // ReportDocument report = new ReportDocument();
                            DataTable dtpb = daml.SELECT_QUIRY_only_retrn_dt(@"select  item_no, item_name, item_cost, item_price, item_barcode,unit_name item_unit, item_obalance, item_cbalance, item_group, item_image, item_req, item_tax, unit2, 
                         uq2, unit2p, unit3, uq3, unit3p, unit4, uq4, unit4p, item_ename, item_opencost, item_curcost, supno, note, last_updt, sgroup, price2 from items,units  where item_unit=unit_id and item_no ='" + dtr[0] + "'");

                            LocalReport report = new LocalReport();
                            ///////////////////////////////////////////////   report.ReportEmbeddedResource = "POS.Sal.rpt.Sal_Tran_Tax_rpt.rdlc";
                            if (File.Exists(Directory.GetCurrentDirectory() + @"\reports\Barcode.rdlc"))
                                report.ReportPath = Directory.GetCurrentDirectory() + @"\reports\Barcode.rdlc";
                            else
                                report.ReportEmbeddedResource = "POS.Sto.rpt.Barcode.rdlc";
                            // report.DataSources.Add(new ReportDataSource("DataSet1", getYourDatasource()));
                            report.DataSources.Add(new ReportDataSource("dts", dtpb));
                            // rf.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("sdt", dt));

                            ReportParameter[] parameters = new ReportParameter[2];
                            parameters[0] = new ReportParameter("comp_name", BL.CLS_Session.cmp_name);
                            //  parameters[1] = new ReportParameter("mdate", dthdr.Rows[0][3].ToString().Substring(6, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(4, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(0, 4));
                            //  parameters[2] = new ReportParameter("aType", dthdr.Rows[0][1].ToString());
                            parameters[1] = new ReportParameter("br_name", BL.CLS_Session.brname);

                            report.SetParameters(parameters);
                         
                            
                            BL.Print_Rdlc_Direct.Print(report, Convert.ToInt16(txt_count.Text), bprinter);
                            //System.Drawing.Printing.PageSettings ps = new System.Drawing.Printing.PageSettings(); ps.Margins.Top = 20; ps.Margins.Left = 20; ps.Margins.Right = 20; ps.Margins.Bottom = 20;

                            //report.SetPageSettings(ps);


                            //var pageSettings = new PageSettings();
                            //pageSettings.PaperSize = report.GetDefaultPageSettings().PaperSize;
                            //pageSettings.Landscape = report.GetDefaultPageSettings().IsLandscape;
                            //pageSettings.Margins = report.GetDefaultPageSettings().Margins;
                            //BL.Print_Rdlc_Direct.Print(report, pageSettings);

                            //////for (int i = 1; i <= Convert.ToInt32(txt_count.Text); i++)
                            //////{
                            //////    BL.Print_Rdlc_Direct.Print(report);
                            //////}
                           // BL.Print_Rdlc_Direct.Print(report, Convert.ToInt16(txt_count.Text), bprinter);
                        }
                    }
                    else
                    {
                        foreach (DataRow dtr in dtds.Rows)
                        {
                            // ReportDocument report = new ReportDocument();
                            DataTable dtpb = daml.SELECT_QUIRY_only_retrn_dt(@"select  item_no, item_name, item_cost, item_price, item_barcode,unit_name item_unit, item_obalance, item_cbalance, item_group, item_image, item_req, item_tax, unit2, 
                         uq2, unit2p, unit3, uq3, unit3p, unit4, uq4, unit4p, item_ename, item_opencost, item_curcost, supno, note, last_updt, sgroup, price2 from items,units  where item_unit=unit_id and item_no ='" + dtr[0] + "'");

                            FastReport.Report rpt = new FastReport.Report();

                            rpt.Load(Environment.CurrentDirectory + @"\reports\Barcode" + numUpDwn1.Value + ".frx");
                            rpt.SetParameterValue("br_name", BL.CLS_Session.brname);
                            rpt.SetParameterValue("taxper", BL.CLS_Session.tax_per);
                            rpt.SetParameterValue("tax_type", BL.CLS_Session.autotax ? "3" : "0");
                            rpt.SetParameterValue("sdate", txt_sdate.Text);
                            rpt.SetParameterValue("edate", txt_edate.Text);
                            rpt.SetParameterValue("bc_price", txt_newp.Text);
                            rpt.SetParameterValue("bc", txt_barcode.Text);
                            rpt.RegisterData(dtpb, "items");

                            rpt.PrintSettings.ShowDialog = false;
                            rpt.PrintSettings.Printer = bprinter;
                            rpt.PrintSettings.Copies =onebyone? 1 : Convert.ToInt32(dtr[1]);
                           // rpt.PrintSettings.Copies = Convert.ToInt32(txt_count.Text);

                            rpt.Print();
                        }
                    }
                }

                
            }

            else
            {

                if (string.IsNullOrEmpty(txt_itemno.Text))
                {
                    MessageBox.Show("يرجى ادخال رقم الصنف", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txt_itemno.Focus();
                    return;
                }
                try
                {

                    if (rd_2838.Checked || rd_2238.Checked)
                    {

                        

                        int xxx = Convert.ToInt32(string.IsNullOrEmpty(txt_count.Text) ? "1" : txt_count.Text);
                        SqlDataAdapter da2 = new SqlDataAdapter(@"select  item_no, item_name, item_cost, item_price, item_barcode,unit_name item_unit, item_obalance, item_cbalance, item_group, item_image, item_req, item_tax, unit2, 
                         uq2, unit2p, unit3, uq3, unit3p, unit4, uq4, unit4p, item_ename, item_opencost, item_curcost, supno, note, last_updt, sgroup, price2 from items,units  where item_unit=unit_id and  item_no ='" + txt_itemno.Text + "'", con2);
                        //"item_no like '%" + textBox1.Text + "%' or item_name like '%"+textBox1.Text+"%'"
                        // SqlDataAdapter dacr1 = new SqlDataAdapter("select * from hdr where ref=(select max(ref) from hdr)", con);
                        // DataSet1 ds = new DataSet1();
                        DataTable dt2 = new DataTable();
                        da2.Fill(dt2);
                        // dacr.Fill(ds, "dtl");
                        // dacr1.Fill(ds, "hdr");

                        //if (File.Exists(Directory.GetCurrentDirectory() + @"\reports\BarcodeReports"+(rd_2838.Checked? "1" : rd_2238.Checked? "2" : "3")+".rpt"))
                        //{
                        CrystalDecisions.CrystalReports.Engine.ReportDocument report = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

                        string filePath = Directory.GetCurrentDirectory() + @"\reports\BarcodeReports" + (rd_2838.Checked ? "1" : rd_2238.Checked ? "2" : "3") + ".rpt";
                        report.Load(filePath);

                        report.SetDataSource(dt2);
                        report.SetParameterValue("cmp_name", POS.BL.CLS_Session.cmp_name);
                        // report.ReportDefinition.Areas.

                        // crystalReportViewer1.ReportSource = report;

                        // crystalReportViewer1.Refresh();

                        report.PrintOptions.PrinterName = bprinter;




                        //int xxx = Convert.ToInt32(textBox8.Text);
                        //for (int i = 1; i <= xxx; i++)
                        //{
                        // report.PrintToPrinter(1, true, 1, 1);
                        // report.PrintToPrinter(1, false, 0, 0);
                        report.PrintToPrinter(xxx, true, 1, 1);
                        report.Close();
                        // report.PrintToPrinter(1, false, 1, 1);
                        txt_count.Text = "1";
                        
                    }
                    else
                    {
                       
                        if (!string.IsNullOrEmpty(txt_itemno.Text) && rd_cstm.Checked)
                        {

                            SqlDataAdapter da3 = new SqlDataAdapter(@"select  item_no, item_name, item_cost, item_price, item_barcode,unit_name item_unit, item_obalance, item_cbalance, item_group, item_image, item_req, item_tax, unit2, 
                            uq2, unit2p, unit3, uq3, unit3p, unit4, uq4, unit4p, item_ename, item_opencost, item_curcost, supno, note, last_updt, sgroup, price2 from items,units  where item_unit=unit_id and item_no ='" + txt_itemno.Text + "'", con2);

                            DataTable dt3 = new DataTable();
                            da3.Fill(dt3);
                            /*
                            Acc.rpt.Acc_frmPrint rf = new Acc.rpt.Acc_frmPrint();

                            // LocalReport report = new LocalReport();
                            //  report.ReportPath = Directory.GetCurrentDirectory() + @"\reports\Barcode.rdlc";
                            rf.reportViewer1.LocalReport.ReportPath = Directory.GetCurrentDirectory() + @"\reports\Barcode.rdlc";
                            // report.DataSources.Add(new ReportDataSource("dts", dt3));
                            rf.reportViewer1.LocalReport.DataSources.Clear();
                            rf.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("dts", dt3));
                            // rf.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("sdt", dt));

                            ReportParameter[] parameters = new ReportParameter[2];
                            parameters[0] = new ReportParameter("comp_name", BL.CLS_Session.cmp_name);
                            //  parameters[1] = new ReportParameter("mdate", dthdr.Rows[0][3].ToString().Substring(6, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(4, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(0, 4));
                            //  parameters[2] = new ReportParameter("aType", dthdr.Rows[0][1].ToString());
                            parameters[1] = new ReportParameter("br_name", BL.CLS_Session.brname);



                            //report.SetParameters(parameters);
                            rf.reportViewer1.LocalReport.SetParameters(parameters);

                            //  BL.PrintReport.pagetype = "Sal";

                            // BL.PrintReport.PrintToPrinter(report);
                            rf.reportViewer1.RefreshReport();
                            rf.MdiParent = ParentForm;
                            rf.Show();
                            */

                            LocalReport report = new LocalReport();
                            ///////////////////////////////////////////////   report.ReportEmbeddedResource = "POS.Sal.rpt.Sal_Tran_Tax_rpt.rdlc";
                            if (File.Exists(Directory.GetCurrentDirectory() + @"\reports\Barcode.rdlc"))
                                report.ReportPath = Directory.GetCurrentDirectory() + @"\reports\Barcode.rdlc";
                            else
                                report.ReportEmbeddedResource = "POS.Sto.rpt.Barcode.rdlc";
                            // report.DataSources.Add(new ReportDataSource("DataSet1", getYourDatasource()));
                            report.DataSources.Add(new ReportDataSource("dts", dt3));
                            // rf.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("sdt", dt));

                            ReportParameter[] parameters = new ReportParameter[2];
                            parameters[0] = new ReportParameter("comp_name", BL.CLS_Session.cmp_name);
                            //  parameters[1] = new ReportParameter("mdate", dthdr.Rows[0][3].ToString().Substring(6, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(4, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(0, 4));
                            //  parameters[2] = new ReportParameter("aType", dthdr.Rows[0][1].ToString());
                            parameters[1] = new ReportParameter("br_name", BL.CLS_Session.brname);

                            report.SetParameters(parameters);

                            //System.Drawing.Printing.PageSettings ps = new System.Drawing.Printing.PageSettings(); ps.Margins.Top = 20; ps.Margins.Left = 20; ps.Margins.Right = 20; ps.Margins.Bottom = 20;

                            //report.SetPageSettings(ps);


                            //var pageSettings = new PageSettings();
                            //pageSettings.PaperSize = report.GetDefaultPageSettings().PaperSize;
                            //pageSettings.Landscape = report.GetDefaultPageSettings().IsLandscape;
                            //pageSettings.Margins = report.GetDefaultPageSettings().Margins;
                            //BL.Print_Rdlc_Direct.Print(report, pageSettings);

                            //////for (int i = 1; i <= Convert.ToInt32(txt_count.Text); i++)
                            //////{
                            //////    BL.Print_Rdlc_Direct.Print(report);
                            //////}
                            BL.Print_Rdlc_Direct.Print(report, Convert.ToInt16(txt_count.Text), bprinter);
                            //////BL.PrintReport.pagetype = "";
                            //////BL.PrintReport.pagewidth = "3in";
                            //////BL.PrintReport.pagehight = "2in";
                            //////BL.PrintReport.PrintToPrinter(report); // prtr = new BL.PrintReport();
                            // BL.PrintRdlcDirect.PrintToPrinter(report);
                            //   report.PrintToPrinter();
                            
                        }
                        else
                        {

                           // SqlDataAdapter da3 = new SqlDataAdapter("select i.item_no , i.item_name,i.item_cost,b.price as item_price,b.barcode as  item_barcode,i.item_unit,i.item_obalance,i.item_cbalance,i.item_group,i.item_image,i.item_req from items i join items_bc b on b.item_no=i.item_no and b.barcode='" + txt_barcode.Text + "'", con2);
                            SqlDataAdapter da3 = new SqlDataAdapter(@"select  items.item_no, item_name, item_cost, item_price, item_barcode,unit_name item_unit, item_obalance, item_cbalance, item_group, item_image, item_req, item_tax, unit2, 
                            uq2, unit2p, unit3, uq3, unit3p, unit4, uq4, unit4p, item_ename, item_opencost, item_curcost, supno, items.note, last_updt, sgroup, items.price2 from items join units on item_unit=unit_id join items_bc on items_bc.item_no=items.item_no and items_bc.barcode='" + txt_barcode.Text + "'", con2);


                            DataTable dt3 = new DataTable();
                            da3.Fill(dt3);


                            FastReport.Report rpt = new FastReport.Report();

                            rpt.Load(Environment.CurrentDirectory + @"\reports\Barcode" + numUpDwn1.Value + ".frx");
                            rpt.SetParameterValue("br_name", BL.CLS_Session.brname);
                            rpt.SetParameterValue("taxper", BL.CLS_Session.tax_per);
                            rpt.SetParameterValue("tax_type", BL.CLS_Session.autotax ? "3" : "0");
                            rpt.SetParameterValue("sdate", txt_sdate.Text);
                            rpt.SetParameterValue("edate", txt_edate.Text);
                            rpt.SetParameterValue("bc_price", txt_newp.Text);
                            rpt.SetParameterValue("bc", txt_barcode.Text);
                            rpt.RegisterData(dt3, "items");

                            rpt.PrintSettings.ShowDialog = false;
                            rpt.PrintSettings.Printer = bprinter;
                            rpt.PrintSettings.Copies = onebyone ? 1 : Convert.ToInt32(txt_count.Text);

                            rpt.Print();
                        
                        }
                    }


                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }

            txt_count.Text = "1";
        }

        private void txt_itemno_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F8)
                {
                    // var selectedCell = dataGridView1.SelectedCells[0];
                    // do something with selectedCell...
                    Search_All f4 = new Search_All("2", "Sto");
                    f4.ShowDialog();



                    txt_itemno.Text = f4.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    txt_barcode.Text = f4.dataGridView1.CurrentRow.Cells["i_b"].Value.ToString();
                    txt_name.Text = f4.dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    txt_ename.Text = f4.dataGridView1.CurrentRow.Cells[5].Value.ToString();
                    txt_newp.Text = f4.dataGridView1.CurrentRow.Cells[2].Value.ToString();
                    txt_count.Focus();
                    //  dataGridView1.CurrentCell = this.dataGridView1[3, dataGridView1.CurrentRow.Index];

                    /*
                   dataGridView1.CurrentRow.Cells[0].Value = f7.listView2.SelectedItems[0].Text;
                   dataGridView1.CurrentRow.Cells[1].Value = f7.listView2.SelectedItems[0].SubItems[1].Text.Replace("|","");
                   dataGridView1.CurrentCell = this.dataGridView1[3, dataGridView1.CurrentRow.Index];
                    */



                }
                if (e.KeyCode == Keys.Enter)
                {


                    if (!string.IsNullOrEmpty(txt_itemno.Text))
                    {
                        load_data(txt_itemno.Text);
                        //////string condi = " where i.item_no='" + txt_itemno.Text + "'";
                        //////// select i.item_no , i.item_name,i.item_cost,b.price as item_price,b.barcode as  item_barcode,b.pack,i.item_obalance,i.item_cbalance,i.item_group,i.item_image,i.item_req,t.tax_percent,b.pk_qty pkqty,i.item_tax i_tax,i.item_image img from items i join items_bc b on b.item_no=i.item_no and i.item_no='" + textBox3.Text + "' join taxs t on i.item_tax=t.tax_id", con2);
                        //////SqlDataAdapter da23 = new SqlDataAdapter("select i.item_no , i.item_name,i.item_cost,i.item_price as item_price,i.item_barcode as  item_barcode,i.item_obalance,i.item_cbalance,i.item_group,i.item_image,i.item_req,i.item_tax i_tax,i.item_image img from items i "+condi+"", con2);
                        //////DataTable dt = new DataTable();
                        //////da23.Fill(dt);

                        //////if (dt.Rows.Count == 1)
                        //////{
                        //////    //txt_name.Text = dt.Rows[0][1].ToString();
                        //////    //txt_oldp.Text = dt.Rows[0][3].ToString();
                        //////    //txt_newp.Select();
                        //////   // txt_itemno.Text = f4.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                        //////    txt_barcode.Text = dt.Rows[0]["item_barcode"].ToString();
                        //////    txt_name.Text = dt.Rows[0][1].ToString();
                        //////    txt_newp.Text = dt.Rows[0]["item_price"].ToString();
                        //////    txt_count.Focus();
                        //////}
                    }
                    else
                    {
                        MessageBox.Show("ادخل رقم الصنف");

                    }

                }
            }
            catch
            { }
        }

        private void txt_barcode_KeyDown(object sender, KeyEventArgs e)
        {
             if (e.KeyCode == Keys.Enter)
                {


                    if (!string.IsNullOrEmpty(txt_barcode.Text))
                    {

                        string condi = " where b.barcode='" + txt_barcode.Text + "'";
                        // select i.item_no , i.item_name,i.item_cost,b.price as item_price,b.barcode as  item_barcode,b.pack,i.item_obalance,i.item_cbalance,i.item_group,i.item_image,i.item_req,t.tax_percent,b.pk_qty pkqty,i.item_tax i_tax,i.item_image img from items i join items_bc b on b.item_no=i.item_no and i.item_no='" + textBox3.Text + "' join taxs t on i.item_tax=t.tax_id", con2);
                        SqlDataAdapter da23 = new SqlDataAdapter("select i.item_no , i.item_name,i.item_cost,b.price as item_price,b.barcode as  item_barcode,i.item_obalance,i.item_cbalance,i.item_group,i.item_image,i.item_req,i.item_tax i_tax,i.item_image img,i.item_ename from items i join items_bc b on i.item_no=b.item_no " + condi + "", con2);
                        DataTable dt = new DataTable();
                        da23.Fill(dt);

                        if (dt.Rows.Count == 1)
                        {
                            //txt_name.Text = dt.Rows[0][1].ToString();
                            //txt_oldp.Text = dt.Rows[0][3].ToString();
                            //txt_newp.Select();
                            txt_itemno.Text = dt.Rows[0]["item_no"].ToString(); // f4.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                           // txt_barcode.Text = dt.Rows[0]["item_barcode"].ToString();
                            txt_name.Text = dt.Rows[0][1].ToString();
                            txt_ename.Text = dt.Rows[0]["item_ename"].ToString();
                            txt_newp.Text = dt.Rows[0]["item_price"].ToString();

                            txt_count.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("ادخل رقم باركود");

                    }

                }
            
        }

        private void txt_count_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
               // button1_Click(sender, e);
                button1.Focus();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                cmb_type.Enabled = true;
            else
                cmb_type.Enabled = false;
        }

        private void cmb_type_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_no.Focus();
            }
        }

        private void txt_no_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.Focus();
            }
        }

        private void designToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                FastReport.Report rpt2 = new FastReport.Report();

                rpt2.Load(Environment.CurrentDirectory + @"\reports\Barcode" + numUpDwn1.Value + ".frx");
                // rpt2.SetParameterValue("br_name", BL.CLS_Session.brname);
                // rpt2.RegisterData(dt3, "items");

                //rpt.PrintSettings.ShowDialog = false;
                //rpt.PrintSettings.Printer = bprinter;
                //rpt.PrintSettings.Copies = Convert.ToInt32(txt_count.Text);

                //rpt.Print();
                // rpt2.Design();

                FR_Designer rptd = new FR_Designer(rpt2);
                // Report_Designer rptd = new Report_Designer();
                // rptd.designerControl1.Report = rpt;
                // rptd.Text = rptd.Text + report1.FileName;
                rptd.MdiParent = MdiParent;
                rptd.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void حبةمنكلصنفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            onebyone = true;
            button1_Click(sender, e);
        }

        private void كلالكمياتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            onebyone = false;
            button1_Click(sender, e);
        }

        private void txt_count_Leave(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(txt_count.Text))
            //    txt_count.Text = "1";
        }

        private void button1_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_count.Text))
                txt_count.Text = "1";
        }
    }
}
