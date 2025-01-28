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
using Microsoft.Reporting.WinForms;
using System.Globalization;

namespace POS.Sal
{
    public partial class Sal_Ofr_Report : BaseForm
    {
        BL.DAML daml = new BL.DAML();
        BL.Date_Validate datval = new BL.Date_Validate();
        DataTable dt, dtusers;
      //  DataTable dtt;
        public static int qq;
       // string typeno = "";
        SqlConnection con3 =BL.DAML.con;// new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        public Sal_Ofr_Report()
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
                /*
                if (typeno == "")
                {
                    string xxx;
                    xxx = maskedTextBox1.Text.ToString();

                    string zzz;
                    zzz = maskedTextBox2.Text.ToString();
                    SqlDataAdapter da3 = new SqlDataAdapter("select [ref] ,[contr] ,[type],[date] ,[total] ,[count] ,[payed],[total_cost],[saleman] ,[req_no] ,[cust_no],[discount],[net_total] from sales_hdr where date between'" + xxx + "' and '" + zzz + "'", con3);
                    SqlDataAdapter da4 = new SqlDataAdapter("select sum(total),sum(total_cost),sum(discount),sum(net_total) from sales_hdr where date between'" + xxx + "' and '" + zzz + "'", con3);
                    DataSet ds3 = new DataSet();
                    da3.Fill(ds3, "0");
                    da4.Fill(ds3, "1");
                    dataGridView1.DataSource = ds3.Tables["0"];
                    txt_total.Text = ds3.Tables[1].Rows[0][0].ToString();
                    txt_cost.Text = ds3.Tables[1].Rows[0][1].ToString();


                    textBox1.Text = ds3.Tables[1].Rows[0][2].ToString();
                    textBox2.Text = ds3.Tables[1].Rows[0][3].ToString();

                    dataGridView1.Columns[9].Visible = false;
                    dataGridView1.Columns[10].Visible = false;
                    dtt = ds3.Tables["0"];
                }
                else
                {// and type = " + typeno + "
                    string xxx;
                    xxx = maskedTextBox1.Text.ToString();

                    string zzz;
                    zzz = maskedTextBox2.Text.ToString();
                    SqlDataAdapter da3 = new SqlDataAdapter("select [ref] ,[contr] ,[type],[date] ,[total] ,[count] ,[payed],[total_cost],[saleman] ,[req_no] ,[cust_no],[discount],[net_total] from sales_hdr where date between'" + xxx + "' and '" + zzz + "' and type = " + typeno + "", con3);
                    SqlDataAdapter da4 = new SqlDataAdapter("select sum(total),sum(total_cost),sum(discount),sum(net_total) from sales_hdr where date between'" + xxx + "' and '" + zzz + "' and type = " + typeno + "", con3);
                    DataSet ds3 = new DataSet();
                    da3.Fill(ds3, "0");
                    da4.Fill(ds3, "1");
                    dataGridView1.DataSource = ds3.Tables["0"];
                    txt_total.Text = ds3.Tables[1].Rows[0][0].ToString();
                    txt_cost.Text = ds3.Tables[1].Rows[0][1].ToString();

                    textBox1.Text = ds3.Tables[1].Rows[0][2].ToString();
                    textBox2.Text = ds3.Tables[1].Rows[0][3].ToString();

                    dataGridView1.Columns[9].Visible = false;
                    dataGridView1.Columns[10].Visible = false;
                    dtt = ds3.Tables["0"];

                }
                */
                string usrname = cmb_user.SelectedIndex != -1 ? " and usrid = '" + cmb_user.SelectedValue.ToString() + "' " : " ";
                string usrid = BL.CLS_Session.up_edit_othr ? " " : " and usrid = '" + BL.CLS_Session.UserName + "' ";
                string custno = string.IsNullOrEmpty(txt_code.Text) ? " "  : " and custno='"+txt_code.Text+"' ";
                string cond = checkBox1.Checked? "invhdate" : "invmdate" ;
                string condp = rad_posted.Checked ? " and posted=1 " :(rad_notpostd.Checked? " and posted=0 " :" ");

                string condf = txt_fno.Text != "" ? txt_fno.Text : "1";
                string condt = txt_tno.Text != "" ? txt_tno.Text : "999999999";
                string condft = " and ref between " + condf + " and " + condt + " ";

                string conslctr = cmb_salctr.SelectedIndex != -1 ? " and slcenter = '" + cmb_salctr.SelectedValue.ToString() + "' " : " ";
                string condtp = cmb_type.SelectedIndex != -1 ? " and invtype='" + cmb_type.SelectedValue + "' " : " ";
               // string qstr = "select slcenter sl_no,invtype a_type,ref a_ref,CONVERT(VARCHAR(10), CAST(invmdate as date), 105) a_mdate,CONVERT(VARCHAR(10), CAST(invhdate as date), 105) a_hdate,text a_text,invttl invttl,invdsvl invdsvl,tax_amt_rcvd tax,nettotal nettotal,posted a_p,invtype a_t from sales_hdr where branch='" + BL.CLS_Session.brno + "' " + condp + usrname + condft + conslctr + " and " + cond + " between '" + txt_fdate.Text.Replace("-", "") + "' and '" + txt_tdate.Text.Replace("-", "") + "'";
                string qstr = "select slcenter sl_no,invtype a_type,ref a_ref,CONVERT(VARCHAR(10), CAST(invmdate as date), 105) a_mdate,(substring(invhdate,7,2) + '-' + substring(invhdate,5,2)+'-'+substring(invhdate,1,4)) a_hdate,text a_text,invttl invttl,invdsvl invdsvl,tax_amt_rcvd tax,nettotal nettotal,posted a_p,invtype a_t from salofr_hdr where invtype in ('24','37') " + condtp + " and branch='" + BL.CLS_Session.brno + "' " + condp + usrname + usrid + custno + condft + conslctr + " and " + cond + " between '" + datval.convert_to_yyyyDDdd(txt_fdate.Text) + "' and '" + datval.convert_to_yyyyDDdd(txt_tdate.Text) + "'";
                
                if (!string.IsNullOrEmpty(txt_desc.Text))
                {
                    qstr = qstr + " and text like '%" + txt_desc.Text + "%'";
                }

                if (!string.IsNullOrEmpty(txt_equl.Text))
                {
                  //  MessageBox.Show(comboBox2.SelectedValue.ToString());
                    qstr = qstr + " and invttl " + comboBox2.Text.Substring(0,1) + txt_equl.Text;
                }
                
                dt = daml.SELECT_QUIRY_only_retrn_dt(qstr + " and invtype like '%" + cmb_type.SelectedValue + "%' order by invmdate");

                DataRow dr = dt.NewRow();
                double sumopnd = 0, sumopnm = 0, sumtrd = 0, sumtrm = 0, sumttld = 0, sumttlm = 0;
                foreach (DataRow dtr in dt.Rows)
                {
                    sumopnd = sumopnd + Convert.ToDouble(dtr[6]);
                    sumopnm = sumopnm + Convert.ToDouble(dtr[7]);
                    sumtrd = sumtrd + Convert.ToDouble(dtr[8]);
                    sumtrm = sumtrm + Convert.ToDouble(dtr[9]);
                   // sumttld = sumttld + Convert.ToDouble(dtr[6]);
                  //  sumttlm = sumttlm + Convert.ToDouble(dtr[7]);
                }
              //  dr[0] = "";
              //  dr[1] = "";
               // dr[2] = "";
               // dr[3] = "";
              //  dr[4] = "";
                dr[5] =BL.CLS_Session.lang.Equals("ع")? "الاجمالي" : "TOTAL";
                dr[6] = Math.Round(sumopnd,2);
                dr[7] = Math.Round(sumopnm,2);
                dr[8] = Math.Round(sumtrd,2);
                dr[9] = Math.Round(sumtrm,2);
                dr[10] = true;
               // dr[11] = "";

                dt.Rows.Add(dr);

                dataGridView1.DataSource = dt;

                dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.LightSteelBlue;
                //   dataGridView1.ClearSelection();
                dataGridView1.ClearSelection();

                
                foreach (DataGridViewRow rw in dataGridView1.Rows)
                {
                    if (Convert.ToBoolean(rw.Cells[10].Value) == false)
                       // rw.DefaultCellStyle.BackColor = Color.Thistle;
                     //   rw.DefaultCellStyle.ForeColor = Color.Red;
                   // rw.Cells[0].Value = rw.Cells[0].Value.ToString().Equals("01") ? "ق عام" : rw.Cells[0].Value.ToString().Equals("02")? "س قبض" : "س صرف";
                    if(!string.IsNullOrEmpty(rw.Cells[1].Value.ToString()))
                    rw.Cells[1].Value = datval.validate_trtypes(rw.Cells[1].Value.ToString());
                }

                
                
             
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SalesReport_Load(object sender, EventArgs e)
        {
            txt_fdate.Text = BL.CLS_Session.start_dt;
            txt_tdate.Text = BL.CLS_Session.end_dt;
            /*
            comboBox1.DisplayMember = "Text";
            comboBox1.ValueMember = "Value";

            var items = new[] { new { Text = "قيد عام", Value = "01" }, new { Text = "سند قبض", Value = "02" }, new { Text = "سند صرف", Value = "03" } };
            comboBox1.DataSource = items;
            comboBox1.SelectedIndex = -1;
            */
            string tr = BL.CLS_Session.trkey.Replace(" ", "','").Remove(0, 2) + "'";
         //   dttrtyps = daml.SELECT_QUIRY_only_retrn_dt("select * from trtypes where tr_no in(" + tr + ") and tr_no in('04','05','14','15')");
            cmb_type.DataSource = daml.SELECT_QUIRY_only_retrn_dt("select * from trtypes where tr_no in(" + tr + ") and tr_no in('24','37')");
            cmb_type.DisplayMember =BL.CLS_Session.lang.Equals("ع")? "tr_name" : "tr_ename";
            cmb_type.ValueMember = "tr_no";
            cmb_type.SelectedIndex = -1;

            string sl = BL.CLS_Session.slkey.Replace(" ", "','").Remove(0, 2) + "'";
            // MessageBox.Show(sl);
            
            cmb_salctr.DataSource = daml.SELECT_QUIRY_only_retrn_dt("select * from slcenters where sl_brno + sl_no in(" + sl + ") and sl_brno='" + BL.CLS_Session.brno + "'");
            cmb_salctr.DisplayMember = "sl_name";
            cmb_salctr.ValueMember = "sl_no";
            cmb_salctr.SelectedIndex = -1;

            dtusers = daml.SELECT_QUIRY_only_retrn_dt("select user_name,full_name from users");

            cmb_user.DataSource = dtusers;
            cmb_user.DisplayMember = "full_name";
            cmb_user.ValueMember = "user_name";
            cmb_user.SelectedIndex = -1;

            /*
            comboBox2.DisplayMember = "Text";
            comboBox2.ValueMember = "Value";
            var items2 = new[] { new { Text = ">", Value = ">" }, new { Text = "=", Value = "=" }, new { Text = "<", Value = "<" } };
            comboBox2.DataSource = items2;
            comboBox2.SelectedIndex = -1;
            */
            /*
            maskedTextBox1.Text = DateTime.Now.ToString("yyyy-MM-01") + "05:00:00";

            DateTime dt = new DateTime();

            dt=DateTime.Now.AddDays(1);

           // maskedTextBox2.Text = DateTime.Now.ToString("yyyy-MM-dd") + "05:00:00";
            maskedTextBox2.Text = dt.ToString("yyyy-MM-dd") + "05:00:00";
             * */
          //  string temy1 =
          //  txt_fdate.Text = DateTime.Now.ToString("yyyy-01-01", new CultureInfo("en-US"));

           // string temy2=
         //   txt_tdate.Text = DateTime.Now.ToString("yyyy-12-31", new CultureInfo("en-US"));
        }

        private void dataGridView1_KeyUp(object sender, KeyEventArgs e)
        {
            /*
            if (e.KeyCode == Keys.F5)
            {
                // int selectedIndex = dataGridView1.CurrentCell.RowIndex;
                DilySalesReport.cc = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                // if (selectedIndex > -1)
                // {
                //dataGridView1.Rows.RemoveAt(selectedIndex);
                //dataGridView1.Refresh(); // if needed


                // }
                SalesDtlReport sdtr = new SalesDtlReport();
                //MAIN mn = new MAIN();
                //sdtr.MdiParent = mn;

                sdtr.ShowDialog();


            }
             * */
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
                SalesDtlReport sdtr = new SalesDtlReport();
                //MAIN mn = new MAIN();
                //sdtr.MdiParent = mn;

                sdtr.ShowDialog();
            */

            
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
       // using Excel = Microsoft.Office.Interop.Excel;
        private void copyAlltoClipboard()
        {
            //to remove the first blank column from datagridview
            //dataGridView1.RowHeadersVisible = false;
            //dataGridView1.SelectAll();
            //DataObject dataObj = dataGridView1.GetClipboardContent();
            //if (dataObj != null)
            //    Clipboard.SetDataObject(dataObj);
            /*
            dataGridView1.SelectAll();
            DataObject dataObj = dataGridView1.GetClipboardContent();
            if (dataObj != null)
                Clipboard.SetDataObject(dataObj);
             * */
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                Microsoft.Office.Interop.Excel._Application XcelApp = new Microsoft.Office.Interop.Excel.Application();
                XcelApp.Application.Workbooks.Add(Type.Missing);



                for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
                {
                    // XcelApp.Cells[5, i] = dataGridView1.Columns[i - 1].Visible == true ? dataGridView1.Columns[i - 1].HeaderText : "";
                    if (dataGridView1.Columns[i - 1].Visible == true)
                        XcelApp.Cells[5, i] ="'"+ dataGridView1.Columns[i - 1].HeaderText;
                    else
                        XcelApp.Cells[5, i] = null;
                }

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        //  XcelApp.Cells[i + 6, j + 1] = dataGridView1.Columns[j].Visible == true ? dataGridView1.Rows[i].Cells[j].Value.ToString() : "";
                        if (dataGridView1.Columns[j].Visible == true)
                            XcelApp.Cells[i + 6, j + 1] ="'"+ dataGridView1.Rows[i].Cells[j].Value.ToString();
                        else
                            XcelApp.Cells[i + 6, j + 1] = null;
                    }
                }
                // XcelApp.Cells[0, 0] = "123";

                Microsoft.Office.Interop.Excel._Worksheet workSheet = XcelApp.ActiveSheet as Microsoft.Office.Interop.Excel.Worksheet;

                // workSheet.Rows.Insert();
                /*
                workSheet.Cells[1, "H"] = "ID Number";
                workSheet.Cells[1, "J"] = "Current Balance";

                var row = 1;
                foreach (var acct in bankAccounts)
                {
                    row++;
                    workSheet.Cells[row, "H"] = acct.ID;
                    workSheet.Cells[row, "J"] = acct.Balance;
                }
                */
                workSheet.Cells[2, "E"] = BL.CLS_Session.cmp_name;
                workSheet.Cells[3, "E"] = BL.CLS_Session.brname;

                workSheet.Range["C2", "F2"].AutoFormat(Microsoft.Office.Interop.Excel.XlRangeAutoFormat.xlRangeAutoFormat3DEffects2);
                workSheet.Range["C3", "F3"].AutoFormat(Microsoft.Office.Interop.Excel.XlRangeAutoFormat.xlRangeAutoFormat3DEffects2);

                workSheet.Range["F" + (dataGridView1.Rows.Count + 5).ToString(), "J" + (dataGridView1.Rows.Count + 5).ToString()].AutoFormat(Microsoft.Office.Interop.Excel.XlRangeAutoFormat.xlRangeAutoFormat3DEffects2);

                workSheet.Range["A5", "J5"].AutoFormat(Microsoft.Office.Interop.Excel.XlRangeAutoFormat.xlRangeAutoFormat3DEffects2);

                // workSheet.Cells[2, "D"]

                XcelApp.Columns.AutoFit();
                XcelApp.Visible = true;
            }
            ////copyAlltoClipboard();
            ////Microsoft.Office.Interop.Excel.Application xlexcel;
            ////Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
            ////Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
            ////object misValue = System.Reflection.Missing.Value;
            ////xlexcel = new Microsoft.Office.Interop.Excel.Application();
            ////xlexcel.Visible = true;
            ////xlWorkBook = xlexcel.Workbooks.Add(misValue);
            ////xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            ////Microsoft.Office.Interop.Excel.Range CR = (Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[1, 1];
            ////CR.Select();
            ////xlWorkSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);  
            /*
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
             */
           
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
           // dataGridView1_CellContentDoubleClick((object)sender, (DataGridViewCellEventArgs)e);
            /*
            DilySalesReport.cc = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            // if (selectedIndex > -1)
            // {
            //dataGridView1.Rows.RemoveAt(selectedIndex);
            //dataGridView1.Refresh(); // if needed


            // }
            SalesDtlReport sdtr = new SalesDtlReport();
            //MAIN mn = new MAIN();
            //sdtr.MdiParent = mn;

            sdtr.ShowDialog();
             */
          //  dataGridView1_KeyDown((object)sender,(EventArgs) e);
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*
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
             */
        }

        private void button3_Click(object sender, EventArgs e)
        {
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

                /*
                DataSet ds1 = new DataSet();

                */
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

                if(BL.CLS_Session.lang.Equals("ع"))
                    rf.reportViewer1.LocalReport.ReportEmbeddedResource = "POS.Sal.rpt.Sal_Report_rpt.rdlc";
                else
                    rf.reportViewer1.LocalReport.ReportEmbeddedResource = "POS.Sal.rpt.Sal_Report_en_rpt.rdlc";



                // ReportParameter[] parameters = new ReportParameter[2];
                // parameters[0] = new ReportParameter("comp_name", BL.CLS_Session.cmp_name);
                // parameters[1] = new ReportParameter("mdate", dthdr.Rows[0][3].ToString().Substring(6, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(4, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(0, 4));

                // rf.reportViewer1.LocalReport.SetParameters(parameters);
                ReportParameter[] parameters = new ReportParameter[4];
                parameters[0] = new ReportParameter("comp_name", BL.CLS_Session.cmp_name);
                //  parameters[1] = new ReportParameter("mdate", dthdr.Rows[0][3].ToString().Substring(6, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(4, 2) + "- " + dthdr.Rows[0][3].ToString().Substring(0, 4));
                parameters[1] = new ReportParameter("fmdate", txt_fdate.Text);
                parameters[2] = new ReportParameter("tmdate", txt_tdate.Text);
                parameters[3] = new ReportParameter("br_name", BL.CLS_Session.brname);

                rf.reportViewer1.LocalReport.SetParameters(parameters);

                rf.reportViewer1.RefreshReport();
                rf.MdiParent = ParentForm;
                rf.Show();

                //rf.reportViewer1.RefreshReport();
                //rf.MdiParent = ParentForm;
                //rf.Show();
            }
            catch { }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (!BL.CLS_Session.sysno.Equals("1"))
            {


                if (e.KeyCode == Keys.F9)
                {
                    if (BL.CLS_Session.cmp_type.StartsWith("a"))
                    {
                        Sal.Sal_Ofr_D_TF f4 = new Sal.Sal_Ofr_D_TF(dataGridView1.CurrentRow.Cells[0].Value.ToString(), dataGridView1.CurrentRow.Cells["a_t"].Value.ToString(), dataGridView1.CurrentRow.Cells[2].Value.ToString());
                        f4.MdiParent = ParentForm;
                        f4.Show();
                    }
                    else
                    {
                        if (dataGridView1.CurrentRow.Cells["a_t"].Value.ToString().Equals("24"))
                        {
                            Sal.Sal_Ofr_Tran_D f4 = new Sal.Sal_Ofr_Tran_D(dataGridView1.CurrentRow.Cells[0].Value.ToString(), dataGridView1.CurrentRow.Cells["a_t"].Value.ToString(), dataGridView1.CurrentRow.Cells[2].Value.ToString());
                            f4.MdiParent = ParentForm;
                            f4.Show();
                        }
                        else
                        {
                            Sal.Sal_Ord_Tran_D f4 = new Sal.Sal_Ord_Tran_D(dataGridView1.CurrentRow.Cells[0].Value.ToString(), dataGridView1.CurrentRow.Cells["a_t"].Value.ToString(), dataGridView1.CurrentRow.Cells[2].Value.ToString());
                            f4.MdiParent = ParentForm;
                            f4.Show();
                        }
                    }
                }
            }
            if (BL.CLS_Session.sysno == "1")
            {
                if (e.KeyCode == Keys.F9)
                {
                    Sal.Sal_Ofr_Tran_E2 f4 = new Sal.Sal_Ofr_Tran_E2(dataGridView1.CurrentRow.Cells[0].Value.ToString(), dataGridView1.CurrentRow.Cells["a_t"].Value.ToString(), dataGridView1.CurrentRow.Cells[2].Value.ToString());
                    f4.MdiParent = ParentForm;
                    f4.Show();
                }
            }
        }

        private void dataGridView1_CellContentDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            /*
            Acc_Tran f4 = new Acc_Tran(dataGridView1.CurrentRow.Cells["a_t"].Value.ToString(), dataGridView1.CurrentRow.Cells[1].Value.ToString());
            f4.MdiParent = ParentForm;
            // ActiveMdiChild.WindowState = FormWindowState.Normal;
            f4.Show();
             */
            SendKeys.Send("{F9}");
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                cmb_type.SelectedIndex = -1;
            }

             
        }

        private void كشفحسابToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*
            Acc_Tran f4 = new Acc_Tran(dataGridView1.CurrentRow.Cells["a_t"].Value.ToString(), dataGridView1.CurrentRow.Cells[1].Value.ToString());
            f4.MdiParent = ParentForm;
            // ActiveMdiChild.WindowState = FormWindowState.Normal;
            f4.Show();
             */
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    // Add this
                    dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    // Can leave these here - doesn't hurt
                    // dataGridView1.Rows[e.RowIndex].Selected = true;
                    dataGridView1.Focus();
                }
            }
            catch { }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
           // string temy1 =
            if (checkBox1.Checked)
            {
                txt_fdate.Text = DateTime.Now.ToString("01-01-yyyy", new CultureInfo("ar-SA", false));
                //  string temy2 =
                txt_tdate.Text = DateTime.Now.ToString("dd-MM-yyyy", new CultureInfo("ar-SA", false));
            }
            else
            {
                txt_fdate.Text = DateTime.Now.ToString("01-01-yyyy", new CultureInfo("en-US", false));
                //  string temy2 =
                txt_tdate.Text = DateTime.Now.ToString("dd-MM-yyyy", new CultureInfo("en-US", false));
            }
        }

        private void cmb_user_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                cmb_user.SelectedIndex = -1;
            }
        }

        private void cmb_salctr_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                cmb_salctr.SelectedIndex = -1;
            }
        }

        private void txt_fdate_Leave(object sender, EventArgs e)
        {
            try
            {
                string datestr = txt_fdate.Text.ToString().Replace("-", "").Trim();
                if (datestr.Length == 0)
                {
                    txt_fdate.Text = DateTime.Now.ToString("dd", new CultureInfo("en-US", false)) + DateTime.Now.ToString("MM", new CultureInfo("en-US", false)) + DateTime.Now.ToString("yyyy", new CultureInfo("en-US", false));
                    // txt_hdate.Text = convert_to_hdate(txt_hdate.Text.ToString());
                }
                else
                {
                    if (datestr.Length == 2)
                    {
                        txt_fdate.Text = datestr + DateTime.Now.ToString("MM", new CultureInfo("en-US", false)) + DateTime.Now.ToString("yyyy", new CultureInfo("en-US", false));
                        // txt_hdate.Text = convert_to_hdate(txt_mdate.Text.ToString());
                    }
                    else
                    {
                        if (datestr.Length == 4)
                        {
                            txt_fdate.Text = datestr + DateTime.Now.ToString("yyyy", new CultureInfo("en-US", false));
                            // txt_hdate.Text = convert_to_hdate(txt_mdate.Text.ToString());
                        }
                        else
                        {
                            // txt_hdate.Text = convert_to_hdate(txt_mdate.Text.ToString());
                        }
                    }
                }
            }
            catch { }
        }

        private void txt_tdate_Leave(object sender, EventArgs e)
        {
            try
            {
                string datestr = txt_tdate.Text.ToString().Replace("-", "").Trim();
                if (datestr.Length == 0)
                {
                    txt_tdate.Text = DateTime.Now.ToString("dd", new CultureInfo("en-US", false)) + DateTime.Now.ToString("MM", new CultureInfo("en-US", false)) + DateTime.Now.ToString("yyyy", new CultureInfo("en-US", false));
                    // txt_hdate.Text = convert_to_hdate(txt_hdate.Text.ToString());
                }
                else
                {
                    if (datestr.Length == 2)
                    {
                        txt_tdate.Text = datestr + DateTime.Now.ToString("MM", new CultureInfo("en-US", false)) + DateTime.Now.ToString("yyyy", new CultureInfo("en-US", false));
                        // txt_hdate.Text = convert_to_hdate(txt_mdate.Text.ToString());
                    }
                    else
                    {
                        if (datestr.Length == 4)
                        {
                            txt_tdate.Text = datestr + DateTime.Now.ToString("yyyy", new CultureInfo("en-US", false));
                            // txt_hdate.Text = convert_to_hdate(txt_mdate.Text.ToString());
                        }
                        else
                        {
                            // txt_hdate.Text = convert_to_hdate(txt_mdate.Text.ToString());
                        }
                    }
                }
            }
            catch { }
        }

        private void txt_fdate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_tdate.Focus();
                // SendKeys.Send("{Home}");
                txt_tdate.SelectAll();
            }
        }

        private void txt_tdate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.Focus();
            }
        }

        private void txt_code_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F7)
            {
                // txt_name.Text = "";
                try
                {
                    Cus.Acc_Statment_Exp f6 = new Cus.Acc_Statment_Exp(txt_code.Text);
                    f6.MdiParent = ParentForm;
                    f6.Show();
                }
                catch { }

            }
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

                DataTable dt2 = daml.SELECT_QUIRY_only_retrn_dt("select cu_no ,cu_name ,cl_acc,cu_crlmt from customers a join cuclass b on a.cu_class=b.cl_no  where a.inactive=0 and a.cu_brno='" + BL.CLS_Session.brno + "' and cu_no='" + txt_code.Text + "'");

                // dataGridView1.DataSource = daml.SELECT_QUIRY(qstr2);

                if (dt2.Rows.Count > 0)
                {
                    txt_code.Text = dt2.Rows[0][0].ToString();
                    txt_name.Text = dt2.Rows[0][1].ToString();
                  //  txt_crlmt.Text = dt2.Rows[0][3].ToString();
                }
                else
                {
                    txt_code.Text = ""; //dt2.Rows[0][0].ToString();
                    txt_name.Text = "";// dt2.Rows[0][1].ToString();
                }


            }
        }

        private void txt_code_Leave(object sender, EventArgs e)
        {
            DataTable dt2 = daml.SELECT_QUIRY_only_retrn_dt("select cu_no ,cu_name ,cl_acc,cu_crlmt from customers a join cuclass b on a.cu_class=b.cl_no  where a.inactive=0 and a.cu_brno='" + BL.CLS_Session.brno + "' and cu_no='" + txt_code.Text + "'");

            // dataGridView1.DataSource = daml.SELECT_QUIRY(qstr2);

            if (dt2.Rows.Count > 0)
            {
                txt_code.Text = dt2.Rows[0][0].ToString();
                txt_name.Text = dt2.Rows[0][1].ToString();
                //  txt_crlmt.Text = dt2.Rows[0][3].ToString();
            }
            else
            {
                txt_code.Text = ""; //dt2.Rows[0][0].ToString();
                txt_name.Text = "";// dt2.Rows[0][1].ToString();
            }
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int testInt = Convert.ToBoolean(dataGridView1.CurrentRow.Cells["a_p"].Value) ? 1 : 0;
                datval.formate_dgv(this.dataGridView1, testInt);
            }
            catch { }
        }
    }
}
