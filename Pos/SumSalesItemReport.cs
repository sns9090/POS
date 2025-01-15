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

namespace POS.Pos
{
    public partial class SumSalesItemReport : BaseForm
    {
        BL.DAML daml = new BL.DAML();
        BL.Date_Validate datval = new BL.Date_Validate();
        DataTable dtt, dtslctr;
        public static int qq;
        string typeno = "";
        SqlConnection con3 = BL.DAML.con; //new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        public SumSalesItemReport()
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
                string cndtyp = comboBox2.SelectedIndex == 1 ? " and b.type=1 " : comboBox2.SelectedIndex == 2 ? " and b.type=0 " : " ";
                string conds = cmb_salctr.SelectedIndex != -1 ? " and b.slno='" + cmb_salctr.SelectedValue + "' " : " ";
                string condt = string.IsNullOrEmpty(txt_contr.Text) ? " " : " and a.contr_id=" + txt_contr.Text + " ";
                string condam = string.IsNullOrEmpty(txt_salman.Text) ? " " : " and a.saleman=" + txt_salman.Text + " ";
                string condcncl = checkBox1.Checked ? " d_pos_hdr " : " pos_hdr ";
                string xxx= maskedTextBox1.Text.ToString();

               string zzz= maskedTextBox2.Text.ToString();
               SqlDataAdapter da3;
                    //SqlDataAdapter da3 = new SqlDataAdapter("select a_type a,a_ref b,a_mdate c,a_hdate,a_text d,a_amt e  where date between '20180101' and '20190101'", con3);
                    //SqlDataAdapter da4 = new SqlDataAdapter("select sum(total),sum(total_cost),sum(discount),sum(net_total) from pos_hdr where date between'" + xxx + "' and '" + zzz + "'", con3);
                  //  SqlDataAdapter da3 = new SqlDataAdapter("select contr ,case [type] when 1 then 'نقدي' when 0 then 'مرتجع' end as [type],ref,(CONVERT(varchar, date, 25)) [date] ,[total] ,[count] ,[payed],[total_cost],[saleman] ,[req_no] ,[cust_no],[discount],[net_total] from "+condcncl+" where date between'" + xxx + "' and '" + zzz + "' " + conds + condt + condam + "", con3);
               if (rd_contrs.Checked)
               {
                 //  da3 = new SqlDataAdapter("select cast(a.contr_id as varchar) a1,a.contr_name a2,round(isnull(sum(case b.type when 1 then b.total when 0 then 0 when 2 then 0 end),0),2) a3,round(isnull(sum(case b.type when 1 then b.discount when 0 then 0 when 2 then 0 end ),0),2) a4,round(isnull(sum(case b.type when 0 then b.total when 1 then 0 when 2 then 0 end ),0),2) a5,round(isnull(sum(case b.type when 0 then b.discount when 0 then 0 when 2 then 0 end ),0),2) a6,round(isnull(sum(case b.type when 1 then b.tax_amt when 0 then -b.tax_amt when 2 then 0 end),0),2) a7,0.00 a8 from contrs a join pos_hdr b on a.c_brno=b.brno and a.c_slno=b.slno and a.contr_id=b.contr where a.c_brno='" + BL.CLS_Session.brno + "' and b.date between'" + datval.convert_to_yyyy_MMddwith_dash(xxx) + " " + maskedTextBox3.Text + "' and '" + datval.convert_to_yyyy_MMddwith_dash(zzz) + " " + maskedTextBox4.Text + "' " + conds + condt + condam + cndtyp + " group by a.contr_id,a.contr_name", con3);
                   da3 = new SqlDataAdapter("select cast(a.contr_id as varchar) a1,a.contr_name a2,round(isnull(sum(case b.type when 0 then 0 else b.total end),0),2) a3,round(isnull(sum(case b.type when 0 then 0 else b.discount end ),0),2) a4,round(isnull(sum(case b.type when 0 then b.total else 0 end ),0),2) a5,round(isnull(sum(case b.type when 0 then b.discount else 0 end ),0),2) a6,round(isnull(sum(case b.type when 0 then -b.tax_amt else b.tax_amt  end),0),2) a7,0.00 a8 from contrs a join pos_hdr b on a.c_brno=b.brno and a.c_slno=b.slno and a.contr_id=b.contr where a.c_brno='" + BL.CLS_Session.brno + "' and b.date between'" + datval.convert_to_yyyy_MMddwith_dash(xxx) + " " + maskedTextBox3.Text + "' and '" + datval.convert_to_yyyy_MMddwith_dash(zzz) + " " + maskedTextBox4.Text + "' " + conds + condt + condam + cndtyp + " group by a.contr_id,a.contr_name", con3);
                   
                   //  SqlDataAdapter da3 = new SqlDataAdapter("select * from hdr where date like'%" + xxx + "%'", con3);
                   // SqlDataAdapter da4 = new SqlDataAdapter("select sum(total) from hdr where date between'" + xxx + "' and '" + yyy + "'", con3);
                  // da4 = new SqlDataAdapter("select sum(case type when 1 then total  when 0 then -total end),sum(case type when 1 then total_cost  when 0 then -total_cost end),sum(case type when 1 then discount  when 0 then -discount end),sum(case type when 1 then net_total  when 0 then -net_total end ) from " + condcncl + " where date between'" + datval.convert_to_yyyy_MMddwith_dash(xxx) + "' and '" + datval.convert_to_yyyy_MMddwith_dash(zzz) + "' " + conds + condt + condam + "", con3);
               }
               else
               {
                   //da3 = new SqlDataAdapter("select contr ,case [type] when 1 then 'بيع نقدي' when 0 then 'مرتجع نقدي' when 2 then 'مرتجع اجل' when 3 then 'بيع اجل' end as [type],ref,(CONVERT(varchar, date, 25)) [date] ,(case type when 1 then total  when 0 then -total end)[total] ,[count] ,[payed],(case type when 1 then total_cost  when 0 then -total_cost end)[total_cost],[saleman] ,[req_no] ,[cust_no],(case type when 1 then discount  when 0 then -discount end)[discount],(case type when 1 then net_total  when 0 then -net_total end)[net_total] from " + condcncl + " where date between'" + datval.convert_to_yyyy_MMddwith_dash(xxx) + " 05:00:00.000' and '" + datval.convert_to_yyyy_MMddwith_dash(zzz) + " 05:00:00.000' " + conds + condt + condam + cndtyp + "", con3);
                  // da3 = new SqlDataAdapter("select cast(a.sl_id as varchar) a1,a.sl_name a2,round(isnull(sum(case b.type when 1 then b.total when 0 then 0 when 2 then 0 end),0),2) a3,round(isnull(sum(case b.type when 1 then b.discount when 0 then 0 when 2 then 0 end ),0),2) a4,round(isnull(sum(case b.type when 0 then b.total when 1 then 0 when 2 then 0 end ),0),2) a5,round(isnull(sum(case b.type when 0 then b.discount when 0 then 0 when 2 then 0 end ),0),2) a6,round(isnull(sum(case b.type when 1 then b.tax_amt when 0 then -b.tax_amt when 2 then 0 end),0),2) a7,0.00 a8 from pos_salmen a join pos_hdr b on a.sl_brno=b.brno and cast(a.sl_id as nvarchar(50))=b.saleman where a.sl_brno='" + BL.CLS_Session.brno + "' and b.date between'" + datval.convert_to_yyyy_MMddwith_dash(xxx) + " " + maskedTextBox3.Text + "' and '" + datval.convert_to_yyyy_MMddwith_dash(zzz) + " " + maskedTextBox4.Text + "' " + conds + condt + condam + cndtyp + " group by a.sl_id,a.sl_name", con3);
                   da3 = new SqlDataAdapter("select cast(a.sl_id as varchar) a1,a.sl_name a2,round(isnull(sum(case b.type when 0 then 0 else b.total end),0),2) a3,round(isnull(sum(case b.type when 0 then 0 else b.discount end ),0),2) a4,round(isnull(sum(case b.type when 0 then b.total else 0 end ),0),2) a5,round(isnull(sum(case b.type when 0 then b.discount else 0 end ),0),2) a6,round(isnull(sum(case b.type when 0 then -b.tax_amt else b.tax_amt  end),0),2) a7,0.00 a8 from pos_salmen a join pos_hdr b on a.sl_brno=b.brno and cast(a.sl_id as nvarchar(50))=b.saleman where a.sl_brno='" + BL.CLS_Session.brno + "' and b.date between'" + datval.convert_to_yyyy_MMddwith_dash(xxx) + " " + maskedTextBox3.Text + "' and '" + datval.convert_to_yyyy_MMddwith_dash(zzz) + " " + maskedTextBox4.Text + "' " + conds + condt + condam + cndtyp + " group by a.sl_id,a.sl_name", con3);

                   

                   //  SqlDataAdapter da3 = new SqlDataAdapter("select * from hdr where date like'%" + xxx + "%'", con3);
                   // SqlDataAdapter da4 = new SqlDataAdapter("select sum(total) from hdr where date between'" + xxx + "' and '" + yyy + "'", con3);
                //   da4 = new SqlDataAdapter("select sum(case type when 1 then total  when 0 then -total end),sum(case type when 1 then total_cost  when 0 then -total_cost end),sum(case type when 1 then discount  when 0 then -discount end),sum(case type when 1 then net_total  when 0 then -net_total end ) from " + condcncl + " where date between'" + datval.convert_to_yyyy_MMddwith_dash(xxx) + "' and '" + datval.convert_to_yyyy_MMddwith_dash(zzz) + "' " + conds + condt + condam + "", con3);
              
               }
                    DataSet ds3 = new DataSet();
                    da3.Fill(ds3, "0");

                    DataRow dr = ds3.Tables["0"].NewRow();
                    double sumopnd = 0, sumopnm = 0, sumtrd = 0, sumtrm = 0, sumttld = 0, sumttlm = 0;
                    foreach (DataRow dtr in ds3.Tables["0"].Rows)
                    {
                        sumopnd = sumopnd + Convert.ToDouble(dtr[2]);
                        sumopnm = sumopnm + Convert.ToDouble(dtr[3]);
                        sumtrd = sumtrd + Convert.ToDouble(dtr[4]);
                        sumtrm = sumtrm + Convert.ToDouble(dtr[5]);
                        sumttld = sumttld + Convert.ToDouble(dtr[6]);
                        sumttlm = sumttlm + Convert.ToDouble(dtr[7]);
                    }
                    dr[0] = "";
                    dr[1] = "الاجمالي";
                    dr[2] = sumopnd;
                    dr[3] = sumopnm;
                    dr[4] = sumtrd;
                    dr[5] = sumtrm;
                    dr[6] = sumttld;
                    dr[7] = sumttlm;

                    ds3.Tables["0"].Rows.Add(dr);
                  //  da4.Fill(ds3, "1");
                  //  dataGridView1.Rows.Clear();
                    dataGridView1.DataSource = ds3.Tables["0"];

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        row.Cells[7].Value = Convert.ToDouble(row.Cells[2].Value) - Convert.ToDouble(row.Cells[3].Value) - Convert.ToDouble(row.Cells[4].Value) + Convert.ToDouble(row.Cells[5].Value);
                    }

                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.LightSteelBlue;
                    dataGridView1.ClearSelection();
                    //txt_total.Text = ds3.Tables[1].Rows[0][0].ToString();
                    //txt_cost.Text = ds3.Tables[1].Rows[0][1].ToString();


                    //textBox1.Text = ds3.Tables[1].Rows[0][2].ToString();
                    //textBox2.Text = ds3.Tables[1].Rows[0][3].ToString();

                    //dataGridView1.Columns[9].Visible = false;
                    //dataGridView1.Columns[10].Visible = false;
                    //dtt = ds3.Tables["0"];

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
          //  maskedTextBox1.Text = DateTime.Now.ToString("01-MM-yyyy 05:00:00.000", new CultureInfo("en-US"));
           // maskedTextBox2.Text = DateTime.Now.AddDays(1).ToString("dd-MM-yyyy 05:00:00.000", new CultureInfo("en-US"));
            maskedTextBox1.Text = DateTime.Now.ToString("01-MM-yyyy", new CultureInfo("en-US"));
            maskedTextBox2.Text = DateTime.Now.AddDays(1).ToString("dd-MM-yyyy", new CultureInfo("en-US"));
          //  maskedTextBox3.Text = "05:00:00.000";
          //  maskedTextBox4.Text = "04:59:59.999";
            maskedTextBox3.Text = (BL.CLS_Session.posatrtday.ToString().Length == 1 ? "0" + BL.CLS_Session.posatrtday.ToString() : BL.CLS_Session.posatrtday.ToString()) + ":00:00.000";
           // maskedTextBox4.Text = ((BL.CLS_Session.posatrtday - 1).ToString().Length == 1 ? "0" + (BL.CLS_Session.posatrtday - 1).ToString() : (BL.CLS_Session.posatrtday - 1).ToString()) + ":59:59.999";
            if (BL.CLS_Session.posatrtday != 0)
                maskedTextBox4.Text = ((BL.CLS_Session.posatrtday - 1).ToString().Length == 1 ? "0" + (BL.CLS_Session.posatrtday - 1).ToString() : (BL.CLS_Session.posatrtday - 1).ToString()) + ":59:59.999";
            else
                maskedTextBox4.Text = "23:59:59.999";

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
            //Reports.Report_Form rrf = new Reports.Report_Form("DR", dtt);
            //rrf.MdiParent = ParentForm;
            //rrf.Show();
            if (dataGridView1.Rows.Count == 0)
                return;
            /*
            Reports.Report_Form rrf = new Reports.Report_Form("DR", dtt);
            rrf.MdiParent = ParentForm;
            rrf.Show();
             */
            try
            {
                Acc.rpt.Acc_frmPrint rf = new Acc.rpt.Acc_frmPrint();


                // DataSet ds1 = new DataSet();
                System.Data.DataTable dt = new System.Data.DataTable();
                int cn = 1;
                foreach (DataGridViewColumn col in dataGridView1.Columns)
                {
                    /*
                    dt.Columns.Add(col.Name);
                    // dt.Columns.Add(col.HeaderText);
                     * */
                    dt.Columns.Add("c" + cn.ToString());

                    // MessageBox.Show("c" + cn.ToString());
                    cn++;
                }

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    DataRow dRow = dt.NewRow();
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        dRow[cell.ColumnIndex] = cell.Value;
                        //  MessageBox.Show(cell.Value.ToString());
                    }
                    dt.Rows.Add(dRow);
                }




                rf.reportViewer1.LocalReport.DataSources.Clear();
                rf.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));
                rf.reportViewer1.LocalReport.ReportEmbeddedResource = "POS.Pos.rpt.CS_Report_DM_rpt.rdlc";



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
                parameters[4] = new ReportParameter("cors", rd_contrs.Checked? "اسم الكاشير":"اسم البائع");

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

                    SqlDataAdapter dacr = new SqlDataAdapter("select [brno],[slno],[ref],[contr],[type],[barcode],[name],[unit],([price]-([price]*[discpc]/100)) price,[qty],[cost],[is_req],[itemno],[srno],[pkqty] from pos_dtl where ref=" + toprt + " and contr=" + toprtc + "", con3);
                    SqlDataAdapter dacr1 = new SqlDataAdapter("select * from pos_hdr where ref=" + toprt + " and contr=" + toprtc + "", con3);
                    SqlDataAdapter chk = new SqlDataAdapter("select sum(isnull(is_req,0)) from pos_dtl where ref=" + toprt + " and contr=" + toprtc + "", con3);

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

                    if (ds.Tables["sum"].Rows.Count > 0 && Convert.ToInt32(ds.Tables["sum"].Rows[0][0]) > 0)
                    {
                        // rpt.SalesReport_req report = new rpt.SalesReport_req();
                        rpt.SalesReport111 report = new rpt.SalesReport111();
                        //  CrystalReport1 report = new CrystalReport1();
                        report.SetDataSource(ds);
                        report.SetParameterValue("Comp_Name", BL.CLS_Session.cmp_name);
                        report.SetParameterValue("br_name", BL.CLS_Session.brname);
                        report.SetParameterValue("msg1", BL.CLS_Session.msg1.ToString());
                        report.SetParameterValue("msg2", BL.CLS_Session.msg2.ToString());
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
                        report.SetParameterValue("msg1", BL.CLS_Session.msg1.ToString());
                        report.SetParameterValue("msg2", BL.CLS_Session.msg2.ToString());

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

        private void rd_salmen_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView1.Columns[1].HeaderText = "اسم البائع";
        }

        private void rd_contrs_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView1.Columns[1].HeaderText = "اسم الكاشير";
        }
    }
}
