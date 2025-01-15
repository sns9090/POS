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
//using CrystalDecisions.CrystalReports.Engine;
using Microsoft.Reporting.WinForms;

namespace POS.Sto
{
    public partial class Items_Bc : BaseForm
    {
        string itmno, bprinter, ptype, prpt;
        SqlConnection con2 = BL.DAML.con; // new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        BL.DAML daml = new BL.DAML();
        private SqlConnection sqlConnection = null;

        private SqlDataAdapter sqlDataAdapter = null;

        private SqlCommandBuilder sqlCommandBuilder = null;

        private DataTable dataTable = null;

        private BindingSource bindingSource = null;

        double cst = 0;

        public Items_Bc(string itemno,double cost)
        {
            InitializeComponent();
            itmno = itemno;
            cst = cost;
        }

        private void Items_Bc_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.TsIcon;
            try
            {
                DataTable dtunits = daml.SELECT_QUIRY_only_retrn_dt("select unit_id,unit_name from units");
                cmb_unit.DataSource = dtunits;
                cmb_unit.DisplayMember = "unit_name";
                cmb_unit.ValueMember = "unit_id";


                SqlDataAdapter da = new SqlDataAdapter("select item_no,item_name from items where item_no='" + itmno + "'", con2);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("لا يوجد صنف مرتبط", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    panel1.Enabled = false;
                }
                else
                {
                    // label1.Text = dt.Rows[0][0].ToString();
                    // label2.Text = dt.Rows[0][1].ToString();
                    txt_itemno.Text = dt.Rows[0][0].ToString();
                    txt_itemname.Text = dt.Rows[0][1].ToString();
                }

                // var lines = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\bprinter.txt");
                // bprinter = lines.First().ToString();


                var lines2 = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\bprinter.txt");
                bprinter = lines2.First().ToString();
                ptype = string.IsNullOrEmpty(lines2.Skip(1).First().ToString()) ? "1" : lines2.Skip(1).First().ToString();
                prpt = lines2.Skip(2).First().ToString();
                //  MessageBox.Show(bprinter);
            }
            catch { }
            Load_Data();

           
        }
    
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmb_unit.Focus();
            }

        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_price.Focus();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_price.Text) || string.IsNullOrEmpty(txt_shdqty.Text) || string.IsNullOrEmpty(txt_mprice.Text))
            {
                MessageBox.Show("يجب ملئ الحقول المطلوبة","",MessageBoxButtons.OK,MessageBoxIcon.Error);
                txt_barcode.Focus();
                return;
            }
            SqlDataAdapter da1 = new SqlDataAdapter("select item_no from items where item_no='" + txt_barcode.Text.Trim() + "'", con2);
            DataTable dtb1 = new DataTable();

            da1.Fill(dtb1);
           // if (dtb1.Rows.Count > 0)
            if (dtb1.Rows.Count > 0 && !txt_barcode.Text.Trim().Equals(txt_itemno.Text))
            {
                MessageBox.Show("لايمكن اضافة باركود رقم صنف اساسي لصنف اخر", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_barcode.Focus();
                return;
            }
            SqlDataAdapter da = new SqlDataAdapter("select barcode from items_bc where barcode='" + txt_barcode.Text.Trim() + "' and sl_no='" + BL.CLS_Session.sl_prices + "'", con2);
            DataTable dtb = new DataTable();

            da.Fill(dtb);
            if (dtb.Rows.Count > 0)
            {

                MessageBox.Show("الصنف موجود من سابق", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_barcode.Focus();
            }
            else
            {
                //if (dataGridView1.RowCount > 0)
                //{
                //    foreach (DataGridViewRow row in dataGridView1.Rows)
                //    {
                //        if (textBox1.Text == row.Cells[0].ToString().Trim())
                //        {
                //            MessageBox.Show("item exist");
                //        }
                //        else
                //        {
                //            string[] rowst = new string[] { textBox1.Text, textBox2.Text, "1", "1" };
                //            dataGridView1.Rows.Add(rowst);
                //        }

                //    }
                //}
                //else
                //{
                //    string[] rowst = new string[] { textBox1.Text, textBox2.Text, "1", "1" };
                //    dataGridView1.Rows.Add(rowst);
                //}
                var exists = dataGridView1.Rows.Cast<DataGridViewRow>()
                             .Where(row => !row.IsNewRow)
                             .Select(row => row.Cells[0].Value.ToString())
                             .Any(x => this.txt_barcode.Text == x);

                if (!exists)
                {
                    //Add rows here
                    //select a.barcode as barcode,b.unit_name as unit,a.pk_qty as shdqty,a.price as price,a.pkorder as serial,a.pack as u_i from items_bc a,units b
                  //  string[] rowst = new string[] { txt_barcode.Text.Equals("") ? txt_itemno.Text + "-" + (dataGridView1.RowCount + 1).ToString() : txt_barcode.Text, cmb_unit.Text, txt_shdqty.Text, txt_price.Text, (dataGridView1.RowCount + 1).ToString(), cmb_unit.SelectedValue.ToString(), (Math.Round((Convert.ToDouble(txt_shdqty.Text) * cst), 4)).ToString() };
                    string[] rowst = new string[] { txt_barcode.Text.Equals("") ? txt_itemno.Text + "-" + (dataGridView1.RowCount + 1).ToString() : txt_barcode.Text, cmb_unit.Text, txt_shdqty.Text, txt_price.Text, (dataGridView1.RowCount + 1).ToString(), cmb_unit.SelectedValue.ToString(), (Math.Round((Convert.ToDouble(txt_shdqty.Text) * cst), 4)).ToString(),txt_mprice.Text };
                   
                    //  dataGridView1.Rows.Add(rowst);
                    dataTable.Rows.Add(rowst);
                    dataGridView1.DataSource = dataTable;
                    txt_barcode.Text = "";
                    txt_shdqty.Text = "";
                    txt_barcode.Focus();

                    int rowNumber = 1;
                    foreach (DataGridViewRow r in dataGridView1.Rows)
                    {
                        // dataGridView1.Rows.Add(r);
                        // dataGridView1.Rows[e.RowIndex].HeaderCell.Value = (e.RowIndex + 1).ToString();
                        if (r.IsNewRow) continue;
                        //r.HeaderCell.Value = (e.RowIndex + 1).ToString();
                        r.HeaderCell.Value = rowNumber.ToString();
                        rowNumber = rowNumber + 1;
                    }
                }
                else
                {
                    MessageBox.Show("item exist");
                }
            }
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int rowNumber = 1;
            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                // dataGridView1.Rows.Add(r);
                // dataGridView1.Rows[e.RowIndex].HeaderCell.Value = (e.RowIndex + 1).ToString();
                if (r.IsNewRow) continue;
                //r.HeaderCell.Value = (e.RowIndex + 1).ToString();
                r.HeaderCell.Value = rowNumber.ToString();
                rowNumber = rowNumber + 1;
            }

            //string StrQuery;
            //try
            //{
            //    using (SqlConnection conn = new SqlConnection(con2))
            //    {
            //        using (SqlCommand comm = new SqlCommand())
            //        {
            //            comm.Connection = conn;
            //            conn.Open();
            //            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            //            {
            //                StrQuery = @"INSERT INTO tableName VALUES ("
            //                    + dataGridView1.Rows[i].Cells["ColumnName"].Text + ", "
            //                    + dataGridView1.Rows[i].Cells["ColumnName"].Text + ");";
            //                comm.CommandText = StrQuery;
            //                comm.ExecuteNonQuery();
            //            }
            //        }
            //    }
            //}

            ////////for (int i = 0; i < dataGridView1.Rows.Count; i++)
            ////////{
            ////////    // string StrQuery = @"INSERT INTO items_bc VALUES (" + dataGridView1.Rows[i].Cells["ColumnName"].Value + ", " + dataGridView1.Rows[i].Cells["ColumnName"].Value + ");";
            ////////    string StrQuery = @"INSERT INTO items_bc(item_no,barcode,pack,pk_qty,price) VALUES ('" + label1.Text + "','" + dataGridView1.Rows[i].Cells[0].Value + "', " + dataGridView1.Rows[i].Cells[2].Value + "," + dataGridView1.Rows[i].Cells[3].Value + "," + dataGridView1.Rows[i].Cells[1].Value + ");";

            ////////    try
            ////////    {
            ////////        //SqlConnection conn = new SqlConnection();
            ////////        con2.Open();

            ////////        using (SqlCommand comm = new SqlCommand(StrQuery, con2))
            ////////        {
            ////////            comm.ExecuteNonQuery();
            ////////        }
            ////////        con2.Close();

            ////////    }
            ////////    catch (Exception ex)
            ////////    {
            ////////        MessageBox.Show(ex.ToString());
            ////////    }

            ////////}

            
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                string StrQuery = "merge items_bc as T"
                                + " using (select '" + txt_itemno.Text.Trim() + "' as item_no,"
                                + " '" + dataGridView1.Rows[i].Cells[0].Value.ToString().Trim() + "' as barcode,"
                                + " '" + dataGridView1.Rows[i].Cells[5].Value + "' as pack,"
                                + " '" + dataGridView1.Rows[i].Cells[2].Value + "' as pk_qty,"
                              //  + " " + dataGridView1.Rows[i].Cells[3].Value + " as price,(select max(pkorder) from items_bc where item_no='" + txt_itemno.Text + "' and sl_no='" + BL.CLS_Session.sl_prices + "') as pkorder) as S"
                               + " " + dataGridView1.Rows[i].Cells[3].Value + " as price," + dataGridView1.Rows[i].HeaderCell.Value + " as pkorder," + dataGridView1.Rows[i].Cells[7].Value + " as min_price) as S"
                    // + " on T.barcode = S.barcode and"
                                + " on T.barcode = S.barcode and T.sl_no='" + BL.CLS_Session.sl_prices + "' and T.br_no='" + BL.CLS_Session.brno + "'"
                    //   + " T.report_date = S.report_date"
                                + " when matched then"
                                + " update set T.price = S.price,T.pk_qty=S.pk_qty,T.pkorder=S.pkorder,T.min_price=S.min_price"
                                + " when not matched then"
                                + " insert (item_no, barcode,pack,pk_qty, price,pkorder,br_no,sl_no,min_price)"
                              //  + " values(S.item_no, S.barcode,S.pack,S.pk_qty, S.price,S.pkorder + 1,'" + BL.CLS_Session.sl_prices + "');";
                              + " values(S.item_no, S.barcode,S.pack,S.pk_qty, S.price," + dataGridView1.Rows[i].HeaderCell.Value + ",'" + BL.CLS_Session.brno + "','" + BL.CLS_Session.sl_prices + "'," + (string.IsNullOrEmpty(dataGridView1.Rows[i].Cells[7].ToString()) ? 0 : dataGridView1.Rows[i].Cells[7].Value) + ");";
                try
                {
                    //SqlConnection conn = new SqlConnection();
                    

                    using (SqlCommand comm = new SqlCommand(StrQuery, con2))
                    {
                        if (con2.State == ConnectionState.Closed)
                            con2.Open();
                        comm.ExecuteNonQuery();
                        
                    }

                    using (SqlCommand comm2 = new SqlCommand("update items set item_price=" + dataGridView1.Rows[i].Cells[3].Value + ", last_updt='" + DateTime.Now.ToString("yyyyMMdd", new CultureInfo("en-US", false)) + "' where item_no='" + txt_itemno.Text.Trim() + "' and item_barcode='" + dataGridView1.Rows[i].Cells[0].Value + "'", con2))
                    {
                        if (con2.State == ConnectionState.Closed)
                            con2.Open();
                        comm2.ExecuteNonQuery();

                    }
                    
                    panel1.Enabled = false;
                    button2.Enabled = false;
                  //  textBox5.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            con2.Close();

            //////////string StrQuery = "merge items_bc as T"
            //////////                + " using (select '" + label1.Text + "' as item_no,"
            //////////                + " '" + dataTable.Columns[0] + "' as barcode,"
            //////////                + " '1' as pack,"
            //////////                + " '1' as pk_qty,"
            //////////                + " " + dataTable.Columns[1] + " as price from "+ dataTable + ") as S"
            //////////    // + " on T.barcode = S.barcode and"
            //////////                + " on T.barcode = S.barcode"
            //////////    //   + " T.report_date = S.report_date"
            //////////                + " when matched then"
            //////////                + " update set T.price = T.price"
            //////////                + " when not matched then"
            //////////                + " insert (item_no, barcode,pack,pk_qty, price)"
            //////////                + " values(S.item_no, S.barcode,S.pack,S.pk_qty, S.price);";
            //////////try
            //////////{
            //////////    //SqlConnection conn = new SqlConnection();
            //////////    con2.Open();

            //////////    using (SqlCommand comm = new SqlCommand(StrQuery, con2))
            //////////    {
            //////////        comm.ExecuteNonQuery();
            //////////    }
            //////////    con2.Close();

            //////////}
            //////////catch (Exception ex)
            //////////{
            //////////    MessageBox.Show(ex.ToString());
            //////////}
           
        }
        

        private void button3_Click(object sender, EventArgs e)
        {
            //SqlDataAdapter da10 = new SqlDataAdapter("select count(*) from sales_dtl where itemno ='" + txt_itemno.Text + "'", con2);
            //SqlDataAdapter da11 = new SqlDataAdapter("select count(*) from pu_dtl where itemno ='" + txt_itemno.Text + "'", con2);
            //SqlDataAdapter da12 = new SqlDataAdapter("select count(*) from sto_dtl where itemno ='" + txt_itemno.Text + "'", con2);
            //SqlDataAdapter da13 = new SqlDataAdapter("select count(*) from salofr_dtl where itemno ='" + txt_itemno.Text + "'", con2);
            string tdodelet = dataGridView1.CurrentRow.Cells[0].Value.ToString().Trim();
            SqlDataAdapter da10 = new SqlDataAdapter("select count(*) from sales_dtl where barcode ='" + tdodelet + "'", con2);
            SqlDataAdapter da11 = new SqlDataAdapter("select count(*) from pu_dtl where barcode ='" + tdodelet + "'", con2);
            SqlDataAdapter da12 = new SqlDataAdapter("select count(*) from sto_dtl where barcode ='" + tdodelet + "'", con2);
            SqlDataAdapter da13 = new SqlDataAdapter("select count(*) from salofr_dtl where barcode ='" + tdodelet + "'", con2);
            SqlDataAdapter da14 = new SqlDataAdapter("select count(*) from pos_dtl where barcode ='" + tdodelet + "'", con2);
            DataTable dt10 = new DataTable();
            DataTable dt11 = new DataTable();
            DataTable dt12 = new DataTable();
            DataTable dt13 = new DataTable();
            DataTable dt14 = new DataTable();
            da10.Fill(dt10);
            da11.Fill(dt11);
            da12.Fill(dt12);
            da13.Fill(dt13);
            da14.Fill(dt14);
            // if (dt10.Rows.Count != 0)
            if (Convert.ToInt32(dt10.Rows[0][0]) != 0 || Convert.ToInt32(dt11.Rows[0][0]) != 0 || Convert.ToInt32(dt12.Rows[0][0]) != 0 || Convert.ToInt32(dt13.Rows[0][0]) != 0 || Convert.ToInt32(dt14.Rows[0][0]) != 0)
            {
                MessageBox.Show(BL.CLS_Session.lang.Equals("E") ? "Can't delete moved" : "لا يمكن حذف باركود صنف تمت عليه حركة", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (Convert.ToInt32(dataGridView1.CurrentRow.HeaderCell.Value) == 1)
            {
                MessageBox.Show("لايمكن حذف الباركود الاساسي", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult result = MessageBox.Show(BL.CLS_Session.lang.Equals("E") ? "do you want to delete?" : "هل تريد حذف " + dataGridView1.CurrentRow.Cells[0].Value.ToString().Trim() + " فعلا", "Confirmation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
             if (result == DialogResult.Yes)
             {
                 //sqlDataAdapter 
                 //string std = dataGridView1.CurrentRow.Cells[0].Value.ToString().Trim();
                 //  MessageBox.Show(std);
                 string StrQuery = "delete from items_bc where barcode='" + tdodelet + "' and sl_no='" + BL.CLS_Session.sl_prices + "' and br_no='" + BL.CLS_Session.brno + "' and pkorder <> 1 ";
                 try
                 {
                     // dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
                     if (con2.State == ConnectionState.Closed)
                         con2.Open();

                     using (SqlCommand comm = new SqlCommand(StrQuery, con2))
                     {
                         comm.ExecuteNonQuery();
                         dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
                     }
                     con2.Close();
                     // dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
                     int rowNumber = 1;
                     foreach (DataGridViewRow r in dataGridView1.Rows)
                     {
                         // dataGridView1.Rows.Add(r);
                         // dataGridView1.Rows[e.RowIndex].HeaderCell.Value = (e.RowIndex + 1).ToString();
                         if (r.IsNewRow) continue;
                         //r.HeaderCell.Value = (e.RowIndex + 1).ToString();
                         r.HeaderCell.Value = rowNumber.ToString();
                         rowNumber = rowNumber + 1;
                     }


                 }
                 catch (Exception ex)
                 {
                     MessageBox.Show(ex.ToString());
                 }
             }
             else if (result == DialogResult.No)
             {
                 //...
             }
             else
             {
                 //...
             } 
        }
        private void Load_Data()
        {
            try
            {
                sqlConnection = BL.DAML.con;// new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
                // sqlConnection.Open();
                sqlDataAdapter = new SqlDataAdapter("select a.barcode as barcode,b.unit_name as unit,a.pk_qty as shdqty,a.price as price,a.pkorder as serial,a.pack as u_i,round((a.pk_qty * " + cst + "),4) bccst,a.min_price from items_bc a,units b where a.pack=b.unit_id and a.sl_no='" + BL.CLS_Session.sl_prices + "' and item_no='" + txt_itemno.Text.Trim() + "' order by pkorder", sqlConnection);
                sqlCommandBuilder = new SqlCommandBuilder(sqlDataAdapter);

                dataTable = new DataTable();



                sqlDataAdapter.Fill(dataTable);


                bindingSource = new BindingSource();

                bindingSource.DataSource = dataTable;


                dataGridView1.DataSource = bindingSource;

                dataGridView1.Columns[0].ReadOnly = true;
                dataGridView1.Columns[1].ReadOnly = true;
                if (BL.CLS_Session.multi_bc)
                    dataGridView1.Columns[2].ReadOnly = false;
                else
                    dataGridView1.Columns[2].ReadOnly = true;
                dataGridView1.Columns[4].ReadOnly = true;
                dataGridView1.Columns[6].ReadOnly = true;

                int rowNumber = 1;
                foreach (DataGridViewRow r in dataGridView1.Rows)
                {
                    // dataGridView1.Rows.Add(r);
                    // dataGridView1.Rows[e.RowIndex].HeaderCell.Value = (e.RowIndex + 1).ToString();
                    if (r.IsNewRow) continue;
                    //r.HeaderCell.Value = (e.RowIndex + 1).ToString();
                    r.HeaderCell.Value = rowNumber.ToString();
                    rowNumber = rowNumber + 1;
                }
            }
            catch { }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (ptype.Equals("1") || ptype.Equals("2"))
            {
                CrystalDecisions.CrystalReports.Engine.ReportDocument report = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
               
                    // ReportDocument report = new ReportDocument();
                DataTable dtpb = daml.SELECT_QUIRY_only_retrn_dt("select i.item_no , i.item_name,i.item_cost,b.price as item_price,b.barcode as  item_barcode,i.item_unit,i.item_obalance,i.item_cbalance,i.item_group,i.item_image,i.item_req from items i join items_bc b on b.item_no=i.item_no and b.sl_no='" + BL.CLS_Session.sl_prices + "' and b.barcode='" + dataGridView1.CurrentRow.Cells[0].Value.ToString().Trim() + "'");



                    string filePath = Directory.GetCurrentDirectory() + @"\reports\BarcodeReports" + ptype + ".rpt";
                    report.Load(filePath);

                    report.SetDataSource(dtpb);
                    report.SetParameterValue("cmp_name", POS.BL.CLS_Session.cmp_name);
                    // report.ReportDefinition.Areas.

                    // crystalReportViewer1.ReportSource = report;

                    // crystalReportViewer1.Refresh();

                    report.PrintOptions.PrinterName = bprinter;
                    int xxx = string.IsNullOrEmpty(textBox5.Text) ? 1 : Convert.ToInt32(textBox5.Text);



                    //int xxx = Convert.ToInt32(textBox8.Text);
                    //for (int i = 1; i <= xxx; i++)
                    //{
                    // report.PrintToPrinter(1, true, 1, 1);
                    // report.PrintToPrinter(1, false, 0, 0);
                    report.PrintToPrinter(xxx, true, 1, 1);

                    // }
                
                report.Close();
            }
            else
            {
                if (ptype.Equals("3"))
                {
                    
                        // ReportDocument report = new ReportDocument();
                    DataTable dtpb = daml.SELECT_QUIRY_only_retrn_dt("select i.item_no , i.item_name,i.item_cost,b.price as item_price,b.barcode as  item_barcode,i.item_unit,i.item_obalance,i.item_cbalance,i.item_group,i.item_image,i.item_req from items i join items_bc b on b.item_no=i.item_no and b.sl_no='" + BL.CLS_Session.sl_prices + "' and b.barcode='" + dataGridView1.CurrentRow.Cells[0].Value.ToString().Trim() + "'");

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
                    
                }
                else
                {
                    
                        // ReportDocument report = new ReportDocument();
                    DataTable dtpb = daml.SELECT_QUIRY_only_retrn_dt("select i.item_no , i.item_name,i.item_cost,b.price as item_price,b.barcode as  item_barcode,i.item_unit,i.item_obalance,i.item_cbalance,i.item_group,i.item_image,i.item_req from items i join items_bc b on b.item_no=i.item_no and b.sl_no='" + BL.CLS_Session.sl_prices + "' and b.barcode='" + dataGridView1.CurrentRow.Cells[0].Value.ToString().Trim() + "'");

                        FastReport.Report rpt = new FastReport.Report();

                        rpt.Load(Environment.CurrentDirectory + @"\reports\Barcode" + (string.IsNullOrEmpty(prpt) ? "1" : prpt) + ".frx");
                        rpt.SetParameterValue("br_name", BL.CLS_Session.brname);
                        rpt.RegisterData(dtpb, "items");

                        rpt.PrintSettings.ShowDialog = false;
                        rpt.PrintSettings.Printer = bprinter;
                        // rpt.PrintSettings.Copies = Convert.ToInt32(txt_count.Text);
                        rpt.PrintSettings.Copies = string.IsNullOrEmpty(textBox5.Text) ? 1 : Convert.ToInt32(textBox5.Text);

                        rpt.Print();
                   
                }

            }

            /*
           if(dataGridView1.CurrentRow.Cells[0].Value !=null)
           {
            
                if (textBox5.Text == "")
                {
                    textBox5.Text = "1";
                    SqlDataAdapter da2 = new SqlDataAdapter("select i.item_no , i.item_name,i.item_cost,b.price as item_price,b.barcode as  item_barcode,i.item_unit,i.item_obalance,i.item_cbalance,i.item_group,i.item_image,i.item_req from items i join items_bc b on b.item_no=i.item_no and b.barcode='" + dataGridView1.CurrentRow.Cells[0].Value + "'", con2);
                    //"item_no like '%" + textBox1.Text + "%' or item_name like '%"+textBox1.Text+"%'"
                    // SqlDataAdapter dacr1 = new SqlDataAdapter("select * from hdr where ref=(select max(ref) from hdr)", con);
                    // DataSet1 ds = new DataSet1();
                    DataTable dt2 = new DataTable();
                    da2.Fill(dt2);
                    // dacr.Fill(ds, "dtl");
                    // dacr1.Fill(ds, "hdr");


                    Pos.rpt.BarcodeReport report = new Pos.rpt.BarcodeReport();
                    report.SetDataSource(dt2);
                    report.SetParameterValue("cmp_name", POS.BL.CLS_Session.cmp_name);
                    // report.ReportDefinition.Areas.

                    // crystalReportViewer1.ReportSource = report;

                    // crystalReportViewer1.Refresh();

                    report.PrintOptions.PrinterName = bprinter;




                    //int xxx = Convert.ToInt32(textBox8.Text);
                    //for (int i = 1; i <= xxx; i++)
                    //{
                   ///////////////////////// report.PrintToPrinter(1, true, 1, 1);
                    report.PrintToPrinter(1, false, 0, 0);
                    report.Close();
                    // report.PrintToPrinter(1, false, 1, 1);
                   // txt_prtbar.Text = "";
                    // report.PrintToPrinter(1, false, 1, 1);
                    textBox5.Text = "";
                    //}

                }
                else
                {

                    //SqlDataAdapter da2 = new SqlDataAdapter("select * from items where item_no ='" + textBox1.Text + "'", con2);
                    SqlDataAdapter da2 = new SqlDataAdapter("select i.item_no , i.item_name,i.item_cost,b.price as item_price,b.barcode as  item_barcode,i.item_unit,i.item_obalance,i.item_cbalance,i.item_group,i.item_image,i.item_req from items i join items_bc b on b.item_no=i.item_no and b.barcode='" + dataGridView1.CurrentRow.Cells[0].Value + "'", con2);
                    
                    DataTable dt2 = new DataTable();
                    da2.Fill(dt2);
                    // dacr.Fill(ds, "dtl");
                    // dacr1.Fill(ds, "hdr");


                    Pos.rpt.BarcodeReport report = new Pos.rpt.BarcodeReport();
                    report.SetDataSource(dt2);
                    // report.ReportDefinition.Areas.

                    // crystalReportViewer1.ReportSource = report;

                    // crystalReportViewer1.Refresh();

                    //////report.PrintOptions.PrinterName = bprinter;
                    //////int xxx = Convert.ToInt32(textBox5.Text);
                    //////for (int i = 1; i <= xxx; i++)
                    //////{
                    //////    report.PrintToPrinter(1, true, 1, 1);
                    //////    // report.PrintToPrinter(1, false, 1, 1);

                    //////}
                    report.PrintOptions.PrinterName = bprinter;
                    int xxx = Convert.ToInt32(textBox5.Text);
                    // for (int i = 1; i <= xxx; i++)
                    // {
                    // report.PrintToPrinter(1, true, 1, 1);
                    report.PrintToPrinter(xxx, true, 1, 1);
                    // 
                    // report.PrintToPrinter(1, false, 1, 1);

                    // }
                    report.Close();
                    textBox5.Text = "";
                }
            }
            else
            {
                MessageBox.Show("يرجى اختار صنف", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            */
        }

        private void cmb_unit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_shdqty.Focus();
            }
        }

        private void txt_price_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_mprice.Focus();
            }
        }

        private void txt_barcode_Enter(object sender, EventArgs e)
        {
            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new System.Globalization.CultureInfo("EN"));
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                SqlDataAdapter da10 = new SqlDataAdapter("select count(*) from sales_dtl where itemno ='" + txt_itemno.Text + "'", con2);
                SqlDataAdapter da11 = new SqlDataAdapter("select count(*) from pu_dtl where itemno ='" + txt_itemno.Text + "'", con2);
                SqlDataAdapter da12 = new SqlDataAdapter("select count(*) from sto_dtl where itemno ='" + txt_itemno.Text + "'", con2);
                SqlDataAdapter da13 = new SqlDataAdapter("select count(*) from salofr_dtl where itemno ='" + txt_itemno.Text + "'", con2);
                DataTable dt10 = new DataTable();
                DataTable dt11 = new DataTable();
                DataTable dt12 = new DataTable();
                DataTable dt13 = new DataTable();
                da10.Fill(dt10);
                da11.Fill(dt11);
                da12.Fill(dt12);
                da13.Fill(dt13);
                // if (dt10.Rows.Count != 0)
                if ((dataGridView1.CurrentRow.Index != 0 && Convert.ToInt32(dt10.Rows[0][0]) + Convert.ToInt32(dt11.Rows[0][0]) +  Convert.ToInt32(dt12.Rows[0][0]) + Convert.ToInt32(dt13.Rows[0][0]) ==0) || (dataGridView1.CurrentRow.Index != 0 && BL.CLS_Session.multi_bc))
                {
                    // if (dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[2] && dataGridView1.CurrentRow.Index != 0 && BL.CLS_Session.multi_bc)
                    // if (dataGridView1.CurrentCell == dataGridView1.CurrentRow.Cells[2] && dataGridView1.CurrentRow.Index != 0 && BL.CLS_Session.multi_bc)
                    dataGridView1.CurrentRow.Cells[2].ReadOnly = false;
                    //  else
                    //     dataGridView1.CurrentRow.Cells[2].ReadOnly = true;
                }
                else
                    dataGridView1.CurrentRow.Cells[2].ReadOnly = true;
            }
            catch { }
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txt_barcode_Leave(object sender, EventArgs e)
        {
            SqlDataAdapter da = new SqlDataAdapter("select barcode from items_bc where barcode='" + txt_barcode.Text.Trim() + "' and sl_no='" + BL.CLS_Session.sl_prices + "'", con2);
            DataTable dtb = new DataTable();

            da.Fill(dtb);
            if (dtb.Rows.Count > 0 && !string.IsNullOrEmpty(txt_barcode.Text.Trim()))
            {

                MessageBox.Show("الصنف موجود من سابق", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_barcode.Focus();
            }
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

            int rowNumber = 1;
            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                // dataGridView1.Rows.Add(r);
                // dataGridView1.Rows[e.RowIndex].HeaderCell.Value = (e.RowIndex + 1).ToString();
                if (r.IsNewRow) continue;
                //r.HeaderCell.Value = (e.RowIndex + 1).ToString();
                r.HeaderCell.Value = rowNumber.ToString();
                rowNumber = rowNumber + 1;
            }
        }

        private void txt_mprice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_add.Focus();
            }
        }
    }
}
